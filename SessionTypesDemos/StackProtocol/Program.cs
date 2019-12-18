using System;
using System.Collections.Generic;
using SessionTypes;
using SessionTypes.Threading;

namespace StackProtocol
{
	using static ProtocolBuilder;

	public class Program
	{
		public static void Main(string[] args)
		{
			// μβ.&[Push: ?int; μα.&[Push:?int;α;β, Pop:!int;β], End:end]
			var sub0 = AtC(C2S(P<int>, Goto1), End);
			var sub1 = AtC(C2S(P<int>, Call1(Goto0)), S2C(P<int>, Goto0));
			var protocol = SessionList(sub0, sub1);

			var client = protocol.Fork(server =>
			{
				var loop = false;
				var zero = server.Enter();
				var stack = new Stack<int>();
				do
				{
					loop = false;
					zero.Follow(push =>
					{
						var session = push.Receive(out var value);
						stack.Push(value);
						zero = session.Goto().Follow(push =>
						{
							var session = push.Receive(out var value);
							stack.Push(value);
							return session.Call((session, thisFunc) =>
							{
								return session.Follow(push =>
								{
									var session = push.Receive(out var value);
									stack.Push(value);
									return session.Call(thisFunc).Goto().Follow(push =>
									{
										var session = push.Receive(out var value);
										stack.Push(value);
										return thisFunc(session.Goto(), thisFunc);
									},
									end =>
									{
										return end;
									});
								},
								pop =>
								{
									return pop.Send(stack.Pop()).Goto().Follow(push =>
									{
										var session = push.Receive(out var value);
										stack.Push(value);
										return thisFunc(session.Goto(), thisFunc);
									},
									end =>
									{
										return end;
									});
								});
							}).Goto();
						},
						pop => zero = pop.Send(stack.Pop()).Goto());
						loop = true;
					},
					end =>
					{
						end.Close();
					});
				} while (loop);
			});

			var counter = 0;
			var random = new Random();
			var zero = client.Enter();
			while (true)
			{
				if (random.NextDouble() < 0.7)
				{
					var session = zero.SelectLeft().Send(counter).Goto();
					Console.WriteLine($"Push {counter}");
					counter++;
					if (random.NextDouble() < 0.4)
					{
						var s1 = session.SelectLeft().Send(counter);
						Console.WriteLine($"Push {counter}");
						counter++;
						zero = s1.Call((session, thisFunc) =>
						{
							if (random.NextDouble() < 0.4)
							{
								var s2 = session.SelectLeft().Send(counter);
								Console.WriteLine($"Push {counter}");
								counter++;

								var s3 = s2.Call(thisFunc).Goto();
								if (random.NextDouble() < 0.4)
								{
									var s4 = s3.SelectLeft().Send(counter).Goto();
									Console.WriteLine($"Push {counter}");
									counter++;
									return thisFunc(s4, thisFunc);
								}
								else
								{
									return s3.SelectRight();
								}
							}
							else
							{
								var s2 = session.SelectRight().Receive(out var value).Goto();
								Console.WriteLine($"Pop {value}");
								if (random.NextDouble() < 0.4)
								{
									var s3 = s2.SelectLeft().Send(counter).Goto();
									Console.WriteLine($"Push {counter}");
									counter++;
									return thisFunc(s3, thisFunc);
								}
								else
								{
									return s2.SelectRight();
								}
							}
						}).Goto();
					}
					else
					{
						zero = session.SelectRight().Receive(out var value).Goto();
						Console.WriteLine($"Pop {value}");
					}
				}
				else
				{
					zero.SelectRight().Close();
					break;
				}
			};
		}

		/*
		Push 0
		Pop 0
		Push 1
		Push 2
		Pop 2
		Push 3
		Pop 3
		Push 4
		Pop 4
		Push 5
		Push 6
		Push 7
		Push 8
		Push 9
		Push 10
		Push 11
		Pop 11
		Push 12
		Push 13
		Push 14
		Pop 14
		Push 15
		Pop 15
		Push 16
		Pop 16
		Push 17
		Pop 17
		Push 18
		Pop 18
		Push 19
		Pop 19
		Push 20
		Pop 20
		Push 21
		Pop 21
		Push 22
		Push 23
		Pop 23
		Push 24
		Push 25
		Pop 25
		Push 26
		Pop 26
		Push 27
		Push 28
		Push 29
		Pop 29
		Push 30
		Pop 30
		Push 31
		Push 32
		Pop 32
		Push 33
		Pop 33
		Push 34
		Pop 34
		Push 35
		Push 36
		Push 37
		Pop 37
		Push 38
		Pop 38
		Push 39
		Pop 39
		Push 40
		Push 41
		Pop 41
		Push 42
		Pop 42
		Push 43
		Pop 43
		Push 44
		Push 45
		Pop 45
		Push 46
		Pop 46
		Push 47
		Push 48
		Push 49
		Push 50
		Pop 50
		Push 51
		Push 52
		Pop 52
		Push 53
		Pop 53
		*/
	}
}
