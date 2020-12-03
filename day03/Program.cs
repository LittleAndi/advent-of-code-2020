using System;
using System.IO;
using System.Linq;

namespace day03
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt")
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .ToArray();

            var lineLength = lines.First().Length;
            var x = 0 + 3;
            var y = 0 + 1;
            var trees = 0;

            do
            {
                if (lines[y][x].Equals('#')) trees++;

                x += 3;
                y += 1;

                if (x > lineLength - 1) x -= lineLength;
            } while (y < lines.Count());

            System.Console.WriteLine($"{trees} trees encounterd");
        }
    }
}
