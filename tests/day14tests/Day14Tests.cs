using System;
using Xunit;
using day14;
using Shouldly;
using System.Collections.Generic;
using System.Linq;

namespace day14tests
{
    public class Day14Tests
    {
        [Theory]
        [InlineData("XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X", 11, 73)]
        [InlineData("XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X", 101, 101)]
        [InlineData("XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X", 0, 64)]
        // [InlineData("X1X0", 0, 64)]
        public void TestMask(string mask, int input, long output)
        {
            var computer = new Computer();
            computer.SetMask(mask);
            computer.ApplyMask(input).ShouldBe(output);
        }

        [Theory]
        [MemberData(nameof(MemoryAddressDecoderInput))]
        public void TestMemoryAddressDecoder(string mask, long address, IEnumerable<long> addresses)
        {
            var computer = new Computer();
            var decodeAddresses = computer.DecodeMemoryAddress(mask.ToCharArray(), address);
            decodeAddresses.OrderBy(l => l).ShouldBe(addresses);
        }

        public static IEnumerable<object[]> MemoryAddressDecoderInput()
        {
            yield return new object[] { "000000000000000000000000000000X1001X", 42, new List<long> { 26, 27, 58, 59 } };
            yield return new object[] { "00000000000000000000000000000000X0XX", 26, new List<long> { 16, 17, 18, 19, 24, 25, 26, 27 } };
        }

    }
}
