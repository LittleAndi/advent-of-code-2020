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

            var trees = GetTreesEncounterd(lines, 3, 1);

            System.Console.WriteLine($"Part one: {trees} trees encounterd");

            var encounterdTreesProduct = 1;
            encounterdTreesProduct *= GetTreesEncounterd(lines, 1, 1);
            encounterdTreesProduct *= GetTreesEncounterd(lines, 3, 1);
            encounterdTreesProduct *= GetTreesEncounterd(lines, 5, 1);
            encounterdTreesProduct *= GetTreesEncounterd(lines, 7, 1);
            encounterdTreesProduct *= GetTreesEncounterd(lines, 1, 2);

            System.Console.WriteLine($"Part two: Product of trees encounterd {encounterdTreesProduct}");
        }

        static int GetTreesEncounterd(string[] lines, int movX, int movY)
        {
            var lineLength = lines.First().Length;
            var x = movX;
            var y = movY;
            var trees = 0;

            do
            {
                if (lines[y][x].Equals('#')) trees++;

                x += movX;
                y += movY;

                if (x > lineLength - 1) x -= lineLength;
            } while (y < lines.Count());

            return trees;
        }
    }
}
