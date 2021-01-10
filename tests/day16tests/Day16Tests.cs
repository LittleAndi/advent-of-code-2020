using System;
using day16;
using Shouldly;
using Xunit;

namespace day16tests
{
    public class Day16Tests
    {
        [Fact]
        public void TestTicketValidation()
        {
            var ticketValidator = new TicketValidator();
            ticketValidator.IsTicketValid(new int[] { 7, 3, 47 }).ShouldBe(true);
        }
    }
}
