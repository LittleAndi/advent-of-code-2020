using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day14
{
    class Program
    {
        static void Main(string[] args)
        {
            PartOne();
            PartTwo();
        }

        private static void PartOne()
        {
            Regex regexMask = new Regex(@"mask = ([X01]+)");
            Regex regexMem = new Regex(@"mem\[(\d+)\] = (\d+)");
            var computer = new Computer();

            var lines = File.ReadAllLines("input.txt")
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .ToList();

            foreach (var line in lines)
            {
                var maskMatch = regexMask.Match(line);
                var maskMem = regexMem.Match(line);
                if (maskMatch.Success)
                {
                    var mask = maskMatch.Groups[1].Value;
                    computer.SetMask(mask);
                    continue;
                }
                if (maskMem.Success)
                {
                    var memSlot = long.Parse(maskMem.Groups[1].Value);
                    var value = long.Parse(maskMem.Groups[2].Value);
                    computer.SaveValue(memSlot, value);
                }
            }

            System.Console.WriteLine($"Part one: Sum of memory is {computer.SumOfMemory}");
        }

        private static void PartTwo()
        {
            Regex regexMask = new Regex(@"mask = ([X01]+)");
            Regex regexMem = new Regex(@"mem\[(\d+)\] = (\d+)");
            var computer = new Computer();

            var lines = File.ReadAllLines("input.txt")
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .ToList();

            string currentMask = "";

            foreach (var line in lines)
            {
                var maskMatch = regexMask.Match(line);
                var maskMem = regexMem.Match(line);
                if (maskMatch.Success)
                {
                    currentMask = maskMatch.Groups[1].Value;
                    continue;
                }
                if (maskMem.Success)
                {
                    var memSlot = long.Parse(maskMem.Groups[1].Value);
                    var value = long.Parse(maskMem.Groups[2].Value);
                    computer.DecodeAndSaveValue(currentMask, memSlot, value);
                }
            }

            System.Console.WriteLine($"Part two: Sum of memory is {computer.SumOfMemory}");
        }
    }

    public class Computer
    {
        private Dictionary<long, long> memory = new Dictionary<long, long>();
        private long orMask = 0;
        private long andMask = 0;
        public long SumOfMemory => memory.Sum(m => m.Value);
        public void SetMask(string mask)
        {
            // Clear
            orMask = 0;
            andMask = powerN(2, mask.Length) - 1;

            // Set
            for (int i = 0; i < mask.Length; i++)
            {
                switch (mask[i])
                {
                    case 'X':
                        break;
                    case '0':
                        andMask ^= (powerN(2, mask.Length - i - 1));
                        break;
                    case '1':
                        orMask |= (powerN(2, mask.Length - i - 1));
                        break;
                }
            }
        }
        public long ApplyMask(long value)
        {
            value |= orMask;
            value &= andMask;
            return value;
        }
        public void SaveValue(long memSlot, long value)
        {
            value = ApplyMask(value);
            if (memory.ContainsKey(memSlot)) memory[memSlot] = value;
            else memory.Add(memSlot, value);
        }
        public void DecodeAndSaveValue(string mask, long memSlot, long value)
        {
            var decodedAddresses = DecodeMemoryAddress(mask.ToCharArray(), memSlot);
            foreach (var address in decodedAddresses)
            {
                if (memory.ContainsKey(address)) memory[address] = value;
                else memory.Add(address, value);
            }
        }
        long powerN(long number, int power)
        {
            long res = 1;
            long sq = number;
            while (power > 0)
            {
                if (power % 2 == 1)
                {
                    res *= sq;
                }
                sq = sq * sq;
                power /= 2;
            }
            return res;
        }
        /*
         * This is from https://www.reddit.com/r/adventofcode/comments/kcr1ct/2020_day_14_solutions/gfwtkvq
         */
        public IEnumerable<long> DecodeMemoryAddress(char[] mask, long address)
        {
            var tails = mask[^1] switch
            {
                'X' => new[] { 0L, 1 },
                '0' => new[] { address & 1 },
                _ => new[] { 1L }
            };
            return mask.Length == 1
              ? tails
              : tails.SelectMany(tail =>
                  DecodeMemoryAddress(mask[..^1], address >> 1).Select(head => head << 1 | tail)
                );
        }
    }
}
