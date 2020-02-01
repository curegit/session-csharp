using System;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;

namespace BitcoinNonceCalculator
{
	using static BitConverter;

	public class Miner
	{
		private readonly BigInteger target;

		private readonly byte[] header;

		private readonly Random random = new Random();

		private readonly SHA256 sha256 = new SHA256Managed();

		public Miner(Block block)
		{
			target = block.Target;
			header = block.GetHeader().ToArray();
		}

		public BigInteger ComputeHashWith(uint nonce)
		{
			var nonceBytes = IsLittleEndian ? GetBytes(nonce) : GetBytes(nonce).Reverse();
			var doubleHash = sha256.ComputeHash(sha256.ComputeHash(header.Concat(nonceBytes).ToArray()));
			return new BigInteger(doubleHash, isUnsigned: true, isBigEndian: false);
		}

		public bool TestNonce(uint nonce)
		{
			return ComputeHashWith(nonce) <= target;
		}

		public bool TestRandomNonce(out uint nonce)
		{
			var buffer = new byte[4];
			random.NextBytes(buffer);
			nonce = ToUInt32(buffer);
			return TestNonce(nonce);
		}
	}
}
