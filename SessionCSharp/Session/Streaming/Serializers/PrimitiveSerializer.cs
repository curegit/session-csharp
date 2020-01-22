using System;
using System.IO;
using System.Threading.Tasks;

namespace Session.Streaming.Serializers
{
	/*
	public sealed class PrimitiveSerializer : ISpecialSerializer
	{
		private readonly bool useLittleEndian;

		public PrimitiveSerializer(bool useLittleEndian = true)
		{
			this.useLittleEndian = useLittleEndian;
		}

		public void Serialize<T>(Stream stream, T value)
		{
			if (stream is null) throw new ArgumentNullException(nameof(stream));

			
		}

		public async Task SerializeAsync<T>(Stream stream, T value)
		{

		}

		public T Deserialize<T>(Stream stream)
		{

		}

		public async Task<T> DeserializeAsync<T>(Stream stream)
		{
			if (stream is null)
			{
				throw new ArgumentNullException(nameof(stream));
			}
			if (typeof(T) == typeof(string))
			{
				var buffer = new byte[4];
				await stream.ReadAsync(buffer, 0, 4);
				
					int length = BitConverter.ToInt32(buffer, 0);
				if (length < 0)
				{
					string str = null;
					return (T)(object?)str;
				}else
				{
					var data = new byte[length];
					await stream.ReadAsync(data, 0, length);
					return (T)(object)BitConverter.ToString(data);
				}
			}
			else
			{
				switch
			}
		}

		private int GetLength<T>(Stream stream)
		{

		}

		private async ValueTask GetLengthAsync<T>(Stream stream)
		{

		}

		private byte[] ToBytes<T>(T value)
		{
			if (typeof(T) is string)
			{

			}
			else 
			{
				if (useLittleEndian ^ BitConverter.IsLittleEndian)
				{
					Array.Reverse(bytes);
				}
			}

			BitConverter.
		}

		private T FromBytes<T>()
		{

		}


		
		 * bool	System.Boolean
			byte	System.Byte
			sbyte	System.SByte
			char	System.Char
			decimal	System.Decimal
			double	System.Double
			float	System.Single
			int	System.Int32
			uint	System.UInt32
			long	System.Int64
			ulong	System.UInt64
			object	System.Object
			short	System.Int16
			ushort	System.UInt16
			string	System.String
	}
	*/
}
