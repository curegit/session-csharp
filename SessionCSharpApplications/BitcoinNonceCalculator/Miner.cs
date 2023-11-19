using System;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography;

namespace BitcoinNonceCalculator
{
    using static BitConverter;

    public class Miner(Block block, uint start, uint step)
    {
        private uint current = start;

        private readonly uint increment = step;

        private readonly BigInteger target = block.CalculateTarget();

        private readonly byte[] header = block.GetHeader().ToArray();

        private readonly SHA256 sha256 = SHA256.Create();

        public BigInteger ComputeHashWith(uint nonce)
        {
            var nonceBytes = IsLittleEndian ? GetBytes(nonce) : GetBytes(nonce).Reverse();
            var doubleHash = sha256.ComputeHash(sha256.ComputeHash([.. header, .. nonceBytes]));
            return new BigInteger(doubleHash, isUnsigned: true, isBigEndian: false);
        }

        public bool TestNonce(uint nonce)
        {
            return ComputeHashWith(nonce) <= target;
        }

        public bool TestNextNonce(out uint nonce)
        {
            return TestNonce(nonce = unchecked(current += increment));
        }
    }
}
