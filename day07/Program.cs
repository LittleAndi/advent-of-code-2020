using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day07
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt")
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .Select(l => new Bag(l))
            .ToList();

            PartOne(lines);
            PartTwo(lines);
        }

        private static void PartTwo(List<Bag> allBags)
        {
            var shinyGoldBag = allBags.First(l => l.Name.Equals("shiny gold"));
            var totalBagsInside = CountInsideBags(shinyGoldBag, allBags);
            System.Console.WriteLine($"Total bags inside {shinyGoldBag.Name}: {totalBagsInside}");
        }

        private static int CountInsideBags(Bag bag, List<Bag> allbags)
        {
            Regex regex = new Regex(@"(\d+) (\w+ \w+)");
            var bagsInsideCount = 0;
            var bagsInside = bag.CanContain.Replace('.', ' ').Split(',').Select(t => t.Trim());
            foreach (var insideBagString in bagsInside)
            {
                var match = regex.Match(insideBagString);
                if (!match.Success) continue;
                var insideBagName = match.Groups[2].Value;
                var insideBagMultiplier = int.Parse(match.Groups[1].Value);
                var insideBag = allbags.First(b => b.Name.Equals(insideBagName));

                var insideBagCount = CountInsideBags(insideBag, allbags);
                bagsInsideCount += insideBagMultiplier * insideBagCount + insideBagMultiplier;
            }
            return bagsInsideCount;
        }

        private static void PartOne(List<Bag> lines)
        {
            var bagsThatCanContainShinyGoldBags = new List<Bag>();

            var newBagsThatCanContainShinyGoldBags = lines.Where(l => l.CanContain.Contains("shiny gold")).ToList();
            bagsThatCanContainShinyGoldBags.AddRange(newBagsThatCanContainShinyGoldBags);

            do
            {
                var repatOverBags = newBagsThatCanContainShinyGoldBags;
                newBagsThatCanContainShinyGoldBags = new List<Bag>();
                foreach (var bag in repatOverBags)
                {
                    var testBags = lines.Where(l => l.CanContain.Contains(bag.Name));
                    foreach (var testBag in testBags)
                    {
                        if (!newBagsThatCanContainShinyGoldBags.Contains(testBag) && !bagsThatCanContainShinyGoldBags.Contains(testBag))
                        {
                            newBagsThatCanContainShinyGoldBags.Add(testBag);
                        }
                    }
                }
                bagsThatCanContainShinyGoldBags.AddRange(newBagsThatCanContainShinyGoldBags);
            } while (newBagsThatCanContainShinyGoldBags.Count() > 0);

            System.Console.WriteLine(bagsThatCanContainShinyGoldBags.Count());
        }
    }

    public class Bag : IEquatable<Bag>
    {
        public readonly string Name;
        public readonly string CanContain;
        Regex regex = new Regex(@"(\w+) (\w+) bags contain (\d+ ((\w+) (\w+) (bags?))(, |.)+|no other bags.)");
        public Bag(string bagInfo)
        {
            var groups = regex.Match(bagInfo).Groups;
            Name = $"{groups[1]} {groups[2]}";
            CanContain = $"{groups[3]}";
        }

        public bool Equals(Bag other)
        {
            return other.Name.Equals(this.Name);
        }
    }
}
