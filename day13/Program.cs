using System;
using System.IO;
using System.Linq;

namespace day13
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Part one:");
            PartOne();

            System.Console.WriteLine("\nPart two:");
            PartTwo();
        }

        private static void PartOne()
        {
            var lines = File.ReadAllLines("input.txt")
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .ToList();

            var earliestDeparture = int.Parse(lines[0]);
            var busses = lines[1].Split(',').Where(b => !b.Equals("x")).Select(b => int.Parse(b)).ToList();

            System.Console.WriteLine(earliestDeparture);
            System.Console.WriteLine();
            System.Console.WriteLine(string.Join('\n', busses));
            System.Console.WriteLine();
            System.Console.WriteLine(string.Join('\n', busses.Select(b => earliestDeparture / b)));
        }

        private static void PartTwo()
        {
            var lines = File.ReadAllLines("input.txt")
                .ToList();

            var busses = lines[1].Split(',').ToArray();

            long increment = long.Parse(busses[0]);

            long timeslot = increment;

            for (int i = 1; i < busses.Length; i++)
            {
                if (busses[i] == "x") continue;

                int busNo = int.Parse(busses[i]);

                bool found = false;

                while (!found)
                {
                    timeslot += increment;
                    if ((timeslot + i) % busNo == 0)
                    {
                        increment *= busNo;
                        found = true;
                        System.Console.WriteLine(timeslot);
                    }
                }
            }
        }
    }
}
