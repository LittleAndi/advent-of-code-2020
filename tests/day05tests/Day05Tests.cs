using System;
using Xunit;
using day05;
using Shouldly;

namespace day05tests
{
    public class Day05Tests
    {
        [Theory]
        [InlineData("BFFFBBFRRR", 70, 7, 567)]
        [InlineData("FFFBBBFRRR", 14, 7, 119)]
        [InlineData("BBFFBBFRLL", 102, 4, 820)]
        [InlineData("FBFBBFFRLR", 44, 5, 357)]
        public void TestBoardingPassInfo(string input, int row, int column, int seatId)
        {
            var boardingPass = new BoardingPass(input);
            boardingPass.Row.ShouldBe(row);
            boardingPass.Column.ShouldBe(column);
            boardingPass.SeatId.ShouldBe(seatId);
        }
    }
}
