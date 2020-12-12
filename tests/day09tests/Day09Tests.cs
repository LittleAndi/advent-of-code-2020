using System;
using Xunit;
using day09;
using Shouldly;

namespace day09tests
{
    public class Day09Tests
    {
        private long[] sampleInput = new long[] {
            35, 20, 15, 25, 47, 40, 62, 55, 65, 95, 102, 117, 150, 182, 127, 219, 299, 277, 309, 576
         };

        [Fact]
        public void TestFirstWeaknessNumber()
        {
            var xmasCipher = new XmasCipher(sampleInput, 5);
            xmasCipher.GetFirstWeaknessNumber().ShouldBe(127);
        }

        [Fact]
        public void TestContiguousSet()
        {
            var xmasCipher = new XmasCipher(sampleInput, 5);
            var firstWeeknessNumber = xmasCipher.GetFirstWeaknessNumber();
            firstWeeknessNumber.ShouldBe(127);
            xmasCipher.GetSumOfContiguousSetNumbers(firstWeeknessNumber).ShouldBe(62);
        }
    }
}
