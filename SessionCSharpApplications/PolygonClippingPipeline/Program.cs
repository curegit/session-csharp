using System;
using System.Collections.Generic;
using Session;
using Session.Threading;

namespace PolygonClippingPipeline
{
	using static ProtocolCombinator;

	/// <summary>
	/// Sutherland–Hodgman の Polygon Clipping Algorithm をセッション型を用いたパイプライン処理によって行う応用例
	/// </summary>
	public class Program
	{
		// エントリポイント
		public static void Main(string[] args)
		{
			// 入力データ：クリップされるポリゴン
			var vertices = new Vector[]
			{
				new Vector(2.0, 2.0),
				new Vector(2.0, 6.0),
				new Vector(4.0, 8.0),
				new Vector(7.0, 8.0),
				new Vector(9.0, 6.0),
				new Vector(9.0, 5.0),
				new Vector(7.0, 5.0),
				new Vector(6.0, 6.0),
				new Vector(5.0, 6.0),
				new Vector(4.0, 5.0),
				new Vector(4.0, 3.0),
				new Vector(5.0, 2.0),
				new Vector(7.0, 2.0),
				new Vector(7.0, 3.0),
				new Vector(6.0, 3.0),
				new Vector(6.0, 4.0),
				new Vector(9.0, 4.0),
				new Vector(9.0, 0.0),
				new Vector(4.0, 0.0),
			};

			// 入力データ：クリップするポリゴン
			var clipper = new Vector[]
			{
				new Vector(1.0, 3.0),
				new Vector(3.0, 6.0),
				new Vector(7.0, 7.0),
				new Vector(8.0, 6.0),
				new Vector(10.0, 1.0),
				new Vector(5.0, 0.0),
			};

			// クリップするポリゴンを辺ごとに分ける
			var edges = new (Vector, Vector)[clipper.Length];
			for (int i = 0; i < edges.Length; i++)
			{
				edges[i] = (clipper[i], clipper[(i + 1) % clipper.Length]);
			}

			// Protocol specification
			var protocol = Select(Send(Val<Vector>, Goto0), End);

			// 本命のパイプライン処理
			var (in_ch, out_ch) = protocol.Pipeline(edges,
				// それぞれのパイプラインスレッドの処理
				(prev1, next1, edge) =>
				{
					Vector? first = null;
					Vector from = default;
					Vector to = default;
					for (var loop = true; loop;)
					{
						prev1.Offer
						(
							left =>
							{
								var prev2 = left.Receive(out var vertex);
								from = to;
								to = vertex;
								if (first == null)
								{
									first = to;
								}
								else
								{
									var clipped = Clip((from, to), edge);
									foreach (var v in clipped)
									{
										var next2 = next1.SelectLeft();
										var next3 = next2.Send(v);
										next1 = next3.Goto();
									}
								}
								prev1 = prev2.Goto();
							},
							right =>
							{
								var clipped = Clip((to, first.Value), edge);
								foreach (var v in clipped)
								{
									var next2 = next1.SelectLeft();
									var next3 = next2.Send(v);
									next1 = next3.Goto();
								}
								next1.SelectRight();
								loop = false;
							});
					}
				}
			);

			// メインスレッドから最初のスレッドへ入力を送信
			foreach (var v in vertices)
			{
				in_ch = in_ch.SelectLeft().Send(v).Goto();
			}
			in_ch.SelectRight().Close();

			// メインスレッドで受信した結果を回収する
			var result = new List<Vector>();
			for (var loop = true; loop;)
			{
				out_ch.Offer
				(
					left =>
					{
						out_ch = left.Receive(out var vertex).Goto();
						result.Add(vertex);
					},
					right =>
					{
						right.Close();
						loop = false;
					}
				);
			}

			// 標準出力する
			for (int i = 0; i < result.Count; i++)
			{
				Console.WriteLine($"{i}: {result[i]}");
			}
		}

		// ある点がポリゴンの一辺の内側に含まれるかどうかを返す
		private static bool IsInside(Vector point, (Vector, Vector) line)
		{
			var (from, to) = line;
			var lineVector = to - from;
			var pointVector = point - from;
			return Vector.Cross(lineVector, pointVector) <= 0;
		}

		// 線分と直線の交点を返す、交わっていなければ null を返す
		private static Vector? IntersectionPoint((Vector, Vector) segment, (Vector, Vector) line)
		{
			var (a, b) = segment;
			var (c, d) = line;
			var cross = Vector.Cross(a, c) + Vector.Cross(c, b) + Vector.Cross(b, d) + Vector.Cross(d, a);
			if (Math.Abs(cross) < double.Epsilon)
			{
				return null;
			}
			else
			{
				var u = (Vector.Cross(a, c) + Vector.Cross(c, d) + Vector.Cross(d, a)) / cross;
				if (u < 0.0 || u > 1.0)
				{
					return null;
				}
				return a + u * (b - a);
			}
		}

		// Sutherland–Hodgman の Polygon Clipping Algorithm のサブルーチン
		private static IEnumerable<Vector> Clip((Vector, Vector) polygonEdge, (Vector, Vector) clipEdge)
		{
			var (from, to) = polygonEdge;
			if (IsInside(to, clipEdge))
			{
				if (!IsInside(from, clipEdge))
				{
					yield return IntersectionPoint(polygonEdge, clipEdge).Value;
				}
				yield return to;
			}
			else if (IsInside(from, clipEdge))
			{
				yield return IntersectionPoint(polygonEdge, clipEdge).Value;
			}
			else
			{
				yield break;
			}
		}

		/// <summary>
		/// 二次元空間のベクトルを表す
		/// </summary>
		private struct Vector
		{
			public double X { get; private set; }
			public double Y { get; private set; }

			public Vector(double x, double y)
			{
				X = x;
				Y = y;
			}

			public override string ToString()
			{
				return $"({X:f3} {Y:f3})";
			}

			public static double Dot(Vector u, Vector v)
			{
				return u.X * v.X + u.Y * v.Y;
			}

			public static double Cross(Vector u, Vector v)
			{
				return u.X * v.Y - u.Y * v.X;
			}

			public static Vector operator +(Vector u, Vector v)
			{
				return new Vector(u.X + v.X, u.Y + v.Y);
			}

			public static Vector operator -(Vector u, Vector v)
			{
				return new Vector(u.X - v.X, u.Y - v.Y);
			}

			public static Vector operator *(double a, Vector u)
			{
				return new Vector(u.X * a, u.Y * a);
			}
		}
	}
}
