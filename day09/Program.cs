using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day09
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt")
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .Select(l => long.Parse(l))
            .ToArray<long>();

            var xmasCipher = new XmasCipher(lines, 25);
            var firstWeeknessNumber = xmasCipher.GetFirstWeaknessNumber();

            System.Console.WriteLine($"Part one: first weekness number is {firstWeeknessNumber}");

            var sumOfContiguousSetNumbes = xmasCipher.GetSumOfContiguousSetNumbers(firstWeeknessNumber);

            System.Console.WriteLine($"Part two: sum of contiguous set numbers is {sumOfContiguousSetNumbes}");
        }
    }

    public class XmasCipher
    {
        private readonly long[] input;
        private readonly int preamble;

        public XmasCipher(long[] input, int preamble)
        {
            this.input = input;
            this.preamble = preamble;
        }

        public long GetFirstWeaknessNumber()
        {
            for (int i = preamble; i < this.input.Length; i++)
            {
                if (!TestPosition(i)) return this.input[i];
            }

            return -1;
        }

        private bool TestPosition(int pos)
        {
            for (int i = pos - this.preamble; i < pos - 1; i++)
            {
                for (int j = i + 1; j < pos; j++)
                {
                    if (this.input[i] + this.input[j] == this.input[pos]) return true;
                }
            }
            return false;
        }

        public long GetSumOfContiguousSetNumbers(long value)
        {
            for (int i = 0; i < this.input.Length - 1; i++)
            {
                SortedSet<long> sortedNumbers = new SortedSet<long>();
                long sum = this.input[i];
                sortedNumbers.Add(this.input[i]);
                for (int j = i + 1; j < this.input.Length; j++)
                {
                    sortedNumbers.Add(this.input[j]);
                    sum += this.input[j];
                    if (sum > value) j = this.input.Length;
                    if (sum == value) return sortedNumbers.First() + sortedNumbers.Last();
                }
            }
            return -1;
        }
    }
}
