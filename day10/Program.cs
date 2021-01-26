using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace day10
{
    class Program
    {
        static void Main(string[] args)
        {
            SortedSet<int> adaptersOutputJoltage = new SortedSet<int>();
            File.ReadAllLines("input.txt")
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Select(l => int.Parse(l))
                .ToList()
                .ForEach(outputJoltage => adaptersOutputJoltage.Add(outputJoltage));

            // Add charging outlet
            adaptersOutputJoltage.Add(0);

            // Add your device
            adaptersOutputJoltage.Add(adaptersOutputJoltage.Last() + 3);

            PartOne(adaptersOutputJoltage);
            PartTwo(adaptersOutputJoltage);
        }

        private static void PartTwo(SortedSet<int> adaptersOutputJoltage)
        {
            // Strategey: Analyze sequence of 1-jolt differences
            // 2 ones in a row (3 numbers involved) => 2 combinations
            // 3 ones in a row (4 numbers involved) => 4 combinations
            // 4 ones in a row (5 numbers involved) => 7 combinations
            List<int> diffs = new List<int>();
            for (int i = 1; i < adaptersOutputJoltage.Count; i++)
            {
                diffs.Add(adaptersOutputJoltage.ElementAt(i) - adaptersOutputJoltage.ElementAt(i - 1));
            }

            // Print the diffs
            System.Console.WriteLine($"Part two: diffs {string.Join(',', diffs)}");
        }

        private static void PartOne(SortedSet<int> adaptersOutputJoltage)
        {
            var oneJoltDifferences = 0;
            var threeJoltDifferences = 0;

            for (int i = 1; i < adaptersOutputJoltage.Count; i++)
            {
                switch (adaptersOutputJoltage.ElementAt(i) - adaptersOutputJoltage.ElementAt(i - 1))
                {
                    case 1:
                        oneJoltDifferences++;
                        break;
                    case 3:
                        threeJoltDifferences++;
                        break;
                }
            }

            System.Console.WriteLine($"Part one: 1-jolt diffs {oneJoltDifferences} * 3-jolt diffs {threeJoltDifferences} = {oneJoltDifferences * threeJoltDifferences}");
        }
    }
}
