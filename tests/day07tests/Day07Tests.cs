using System;
using day07;
using Shouldly;
using Xunit;

namespace day07tests
{
    public class Day07Tests
    {
        [Theory]
        [InlineData("light red bags contain 1 bright white bag, 2 muted yellow bags.", "light red", "1 bright white bag, 2 muted yellow bags.")]
        [InlineData("dark orange bags contain 3 bright white bags, 4 muted yellow bags.", "dark orange", "3 bright white bags, 4 muted yellow bags.")]
        [InlineData("bright white bags contain 1 shiny gold bag.", "bright white", "1 shiny gold bag.")]
        [InlineData("muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.", "muted yellow", "2 shiny gold bags, 9 faded blue bags.")]
        [InlineData("shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.", "shiny gold", "1 dark olive bag, 2 vibrant plum bags.")]
        [InlineData("dark olive bags contain 3 faded blue bags, 4 dotted black bags.", "dark olive", "3 faded blue bags, 4 dotted black bags.")]
        [InlineData("vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.", "vibrant plum", "5 faded blue bags, 6 dotted black bags.")]
        [InlineData("faded blue bags contain no other bags.", "faded blue", "no other bags.")]
        [InlineData("dotted black bags contain no other bags.", "dotted black", "no other bags.")]
        [InlineData("shiny purple bags contain 2 posh silver bags, 3 striped silver bags, 5 shiny beige bags, 2 plaid chartreuse bags.", "shiny purple", "2 posh silver bags, 3 striped silver bags, 5 shiny beige bags, 2 plaid chartreuse bags.")]

        public void TestInterpret(string input, string bag1name, string rest)
        {
            var bag = new Bag(input);
            bag.Name.ShouldBe(bag1name);
            bag.CanContain.ShouldBe(rest);
        }
    }
}
