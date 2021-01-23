using System;
using System.Collections.Generic;
using System.Linq;
using day16;
using Shouldly;
using Xunit;

namespace day16tests
{
    public class Day16Tests
    {
        [Theory]
        [InlineData(true, 7, 3, 47)]
        [InlineData(false, 40, 4, 50)]
        [InlineData(false, 55, 2, 20)]
        [InlineData(false, 38, 6, 12)]
        public void TestTicketValidation(bool result, params int[] ticketInfo)
        {
            var ticketValidator = new TicketValidator();
            ticketValidator.AddRule("class", 1, 3, 5, 7);
            ticketValidator.AddRule("row", 6, 11, 33, 44);
            ticketValidator.AddRule("seat", 13, 40, 45, 50);
            ticketValidator.IsTicketValid(ticketInfo).ShouldBe(result, string.Join(',', ticketInfo));
        }
        [Fact]
        public void TestTicketScanningErrorRate()
        {
            var ticketValidator = new TicketValidator();
            ticketValidator.AddRule("class", 1, 3, 5, 7);
            ticketValidator.AddRule("row", 6, 11, 33, 44);
            ticketValidator.AddRule("seat", 13, 40, 45, 50);
            ticketValidator.IsTicketValid(new int[] { 7, 3, 47 });
            ticketValidator.IsTicketValid(new int[] { 40, 4, 50 });
            ticketValidator.IsTicketValid(new int[] { 55, 2, 20 });
            ticketValidator.IsTicketValid(new int[] { 38, 6, 12 });
            ticketValidator.TicketScanningErrorRate.ShouldBe(71);
        }
        [Fact]
        public void TestPredicted()
        {
            var predicate = PredicateBuilder.False<int>();
            predicate = predicate.Or(i => i >= 1 && i <= 3);
            predicate = predicate.Or(i => i >= 5 && i <= 6);

            var numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
            var filteredNumbes = numbers.AsQueryable().Where(predicate);
            filteredNumbes.ShouldBe(new List<int> { 1, 2, 3, 5, 6 });
        }
        [Theory]
        [InlineData("class", 0, false)]
        [InlineData("class", 1, true)]
        [InlineData("class", 2, true)]
        [InlineData("row", 0, true)]
        [InlineData("row", 1, true)]
        [InlineData("row", 2, true)]
        [InlineData("seat", 0, false)]
        [InlineData("seat", 1, false)]
        [InlineData("seat", 2, true)]
        public void TestRule(string rule, int pos, bool result)
        {
            var ticketValidator = new TicketValidator();
            ticketValidator.AddRule("class", 0, 1, 4, 19);
            ticketValidator.AddRule("row", 0, 5, 8, 19);
            ticketValidator.AddRule("seat", 0, 13, 16, 19);
            ticketValidator.IsTicketValid(new int[] { 3, 9, 18 });
            ticketValidator.IsTicketValid(new int[] { 15, 1, 5 });
            ticketValidator.IsTicketValid(new int[] { 5, 14, 9 });
            ticketValidator.TestRule(rule, pos).ShouldBe(result, $"{rule} {pos}");
        }
        [Fact]
        public void TestMatrix()
        {
            var ticketValidator = new TicketValidator();
            ticketValidator.AddRule("class", 0, 1, 4, 19);
            ticketValidator.AddRule("row", 0, 5, 8, 19);
            ticketValidator.AddRule("seat", 0, 13, 16, 19);
            ticketValidator.IsTicketValid(new int[] { 3, 9, 18 });
            ticketValidator.IsTicketValid(new int[] { 15, 1, 5 });
            ticketValidator.IsTicketValid(new int[] { 5, 14, 9 });
            ticketValidator.GetFieldMatrix().ShouldBe(
                new Dictionary<string, int[]> {
                    { "class",  new int[] { 0, 1, 1 } },
                    { "row",    new int[] { 1, 1, 1 } },
                    { "seat",   new int[] { 0, 0, 1 } },
                });
        }
        [Fact]
        public void TestResolvedFields()
        {
            var ticketValidator = new TicketValidator();
            ticketValidator.AddRule("class", 0, 1, 4, 19);
            ticketValidator.AddRule("row", 0, 5, 8, 19);
            ticketValidator.AddRule("seat", 0, 13, 16, 19);
            ticketValidator.IsTicketValid(new int[] { 3, 9, 18 });
            ticketValidator.IsTicketValid(new int[] { 15, 1, 5 });
            ticketValidator.IsTicketValid(new int[] { 5, 14, 9 });
            ticketValidator.GetResolvedFields().OrderBy(d => d.Key).ShouldBe(
                new Dictionary<string, int> {
                    { "class", 1 },
                    { "row", 0 },
                    { "seat", 2 },
                }
            );
        }

    }
}
