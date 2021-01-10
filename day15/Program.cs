using System;
using System.Linq;
using System.Collections.Generic;

namespace day15
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = new int[] { 16, 11, 15, 0, 1, 7 };
            PartOne(input);
            PartTwo(input);
        }

        private static void PartOne(int[] input)
        {
            int rounds = 2020;
            var lastSpokenNumber = GetLastSpokenNumber(rounds, input);
            System.Console.WriteLine($"Part one: Last spoken number after {rounds} rounds is {lastSpokenNumber}");
        }
        private static void PartTwo(int[] input)
        {
            int rounds = 30000000;
            var lastSpokenNumber = GetLastSpokenNumber(rounds, input);
            System.Console.WriteLine($"Part two: Last spoken number after {rounds} rounds is {lastSpokenNumber}");
        }

        private static int GetLastSpokenNumber(int rounds, int[] startNumbers)
        {
            Dictionary<int, (int, int)> spokenNumbers = new Dictionary<int, (int, int)>();
            for (int i = 0; i < startNumbers.Length; i++)
            {
                spokenNumbers.Add(startNumbers[i], (i + 1, i + 1));
            }

            int turn = spokenNumbers.Count;
            int lastSpokenNumber = spokenNumbers.Last().Key;

            turn++;
            lastSpokenNumber = 0;
            if (spokenNumbers.ContainsKey(lastSpokenNumber)) spokenNumbers[lastSpokenNumber] = (spokenNumbers[lastSpokenNumber].Item1, turn);
            else spokenNumbers.Add(lastSpokenNumber, (turn, turn));

            do
            {
                turn++;

                if (spokenNumbers.ContainsKey(lastSpokenNumber))
                {
                    lastSpokenNumber = (turn - 1) - spokenNumbers[lastSpokenNumber].Item1;
                }
                else
                {
                    lastSpokenNumber = 0;
                }
                if (spokenNumbers.ContainsKey(lastSpokenNumber)) spokenNumbers[lastSpokenNumber] = (spokenNumbers[lastSpokenNumber].Item2, turn);
                else spokenNumbers.Add(lastSpokenNumber, (turn, turn));

            } while (turn < rounds);

            return lastSpokenNumber;
        }
    }
}
