using System;
using System.IO;
using System.Linq;

namespace day01
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt")
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Select(l => int.Parse(l))
                .ToArray<int>();

            PartOne(lines);
            PartTwo(lines);
        }
        private static void PartOne(int[] lines)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = i + 1; j < lines.Length; j++)
                {
                    if (lines[i] + lines[j] == 2020)
                    {
                        Console.WriteLine($"{lines[i]} + {lines[j]} = {lines[i] + lines[j]}");
                        Console.WriteLine($"{lines[i]} * {lines[j]} = {lines[i] * lines[j]}");
                        return;
                    }
                }
            }
        }
        private static void PartTwo(int[] lines)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                for (int j = i + 1; j < lines.Length; j++)
                {
                    for (int k = j + 1; k < lines.Length; k++)
                    {
                        if (lines[i] + lines[j] + lines[k] == 2020)
                        {
                            Console.WriteLine($"{lines[i]} + {lines[j]} + {lines[k]} = {lines[i] + lines[j] + lines[k]}");
                            Console.WriteLine($"{lines[i]} * {lines[j]} * {lines[k]} = {lines[i] * lines[j] * lines[k]}");
                            return;
                        }
                        
                    }
                }
            }
        }
    }
}
