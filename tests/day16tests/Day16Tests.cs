using System;
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
            ticketValidator.IsTicketValid(ticketInfo).ShouldBe(result);
        }
    }
}
