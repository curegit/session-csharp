using System;
using System.Linq;
using System.Numerics;
using System.Globalization;
using System.Collections.Generic;

namespace BitcoinNonceCalculator
{
    using static BitConverter;

    public readonly struct Block(int version, string previousHash, string markleRoot, DateTimeOffset dateTime, uint bits)
    {
        public readonly int Version = version;

        public readonly string PreviousHash = previousHash;

        public readonly string MarkleRoot = markleRoot;

        public readonly DateTimeOffset DateTime = dateTime;

        public readonly uint Bits = bits;

        public BigInteger CalculateTarget()
        {
            var exponent = (int)(Bits >> 24);
            var significand = Bits << 8 >> 8;
            return significand * BigInteger.Pow(2, 8 * (exponent - 3));
        }

        public IEnumerable<byte> GetHeader()
        {
            var unixTime = (uint)DateTime.ToUnixTimeSeconds();
            var versionBytes = IsLittleEndian ? GetBytes(Version) : GetBytes(Version).Reverse();
            var hash = BigInteger.Parse("0" + PreviousHash, NumberStyles.HexNumber).ToByteArray(isUnsigned: true, isBigEndian: false);
            var hashBytes = hash.Concat(new byte[32 - hash.Length]);
            var markle = BigInteger.Parse("0" + MarkleRoot, NumberStyles.HexNumber).ToByteArray(isUnsigned: true, isBigEndian: false);
            var markleBytes = markle.Concat(new byte[32 - markle.Length]);
            var timeBytes = IsLittleEndian ? GetBytes(unixTime) : GetBytes(unixTime).Reverse();
            var bitsBytes = IsLittleEndian ? GetBytes(Bits) : GetBytes(Bits).Reverse();
            return versionBytes.Concat(hashBytes).Concat(markleBytes).Concat(timeBytes).Concat(bitsBytes);
        }

        public override string ToString()
        {
            var eol = Environment.NewLine;
            return $"Version: 0x{Version:x}{eol}Previous Hash: {PreviousHash}{eol}Markle Root: {MarkleRoot}{eol}Time: {DateTime}{eol}Bits: 0x{Bits:x}";
        }

        public static Block[] GetSampleBlocks()
        {
            return
            [
                new(
                    version: 0x01,
                    previousHash: "00000000ec989ed4909499b92e9d3eb900d75dd1fab455315916e3d45924d456",
                    markleRoot: "382501ac2d50c5944465c2c316dbe2c70f23dd0de73ea86d339ea5f2bca7b648",
                    dateTime: new DateTimeOffset(2009, 1, 10, 23, 57, 02, TimeSpan.Zero),
                    bits: 0x1d00ffffu
                ),
                new(
                    version: 0x01,
                    previousHash: "00000000ad1448f7e133d85eab6662f4a19a7dfa9cc5a0cf9e572e32385541a0",
                    markleRoot: "904eaaddcf0f12d8d7e522c2731fe744c5f1e6ae44f6987a8001f0dc7a35c023",
                    dateTime: new DateTimeOffset(2009, 9, 10, 23, 23, 48, TimeSpan.Zero),
                    bits: 0x1d00ffffu
                ),
                new(
                    version: 0x04,
                    previousHash: "0000000000000000005629ef6b683f8f6301c7e6f8e796e7c58702a079db14e8",
                    markleRoot: "efb8011cb97b5f1599b2e18f200188f1b8207da2884392672f92ac7985534eeb",
                    dateTime: new DateTimeOffset(2016, 1, 30, 13, 23, 09, TimeSpan.Zero),
                    bits: 0x180928f0u
                ),
                new(
                    version: 0x27ffe000,
                    previousHash: "0000000000000000000121536ffebcfba319d6a582c5f9dea2ad298d9b429714",
                    markleRoot: "cbf85b609bdb079b1879b6fea4dcc4a20c93e09472faf735b9383f29269b538c",
                    dateTime: new DateTimeOffset(2020, 1, 31, 1, 51, 20, TimeSpan.Zero),
                    bits: 0x171232ffu
                ),
                new(
                    version: 0x3fffe000,
                    previousHash: "00000000000000000002a549bd53db4d74e646f22478ff3d6794333bbb2b3022",
                    markleRoot: "04815ebd956ad78e3746826b2bc9c4ff2c3414e7461178dcb5bd24e5d79271d0",
                    dateTime: new DateTimeOffset(2020, 1, 31, 0, 8, 20, TimeSpan.Zero),
                    bits: 0x171232ffu
                ),
                new(
                    version: 0x20000000,
                    previousHash: "0000000000000000000316c23af380fb44fc18ac08bb4e682f1d888cacd6ecdb",
                    markleRoot: "efe9c66a254dbee363a2de2b697f19c5ea27d7bffea5a860c9b2e1e4b607d4e4",
                    dateTime: new DateTimeOffset(2020, 1, 31, 8, 48, 1, TimeSpan.Zero),
                    bits: 0x171232ffu
                ),
            ];
        }
    }
}
