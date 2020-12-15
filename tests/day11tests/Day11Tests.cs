using System;
using Xunit;
using System.IO;
using Shouldly;
using System.Linq;
using day11;
namespace day11tests
{
    public class Day11Tests
    {
        [Fact]
        public void TestInitWaitingArea()
        {
            var area = LoadInput("input_1_state_0.txt");

            var waitingArea = new WaitingArea(area);
            waitingArea.SizeX.ShouldBe(10);
            waitingArea.SizeY.ShouldBe(10);
            waitingArea.GetInfo(0, 0).ShouldBe('L');
            waitingArea.GetInfo(9, 9).ShouldBe('L');
            waitingArea.GetInfo(0, 9).ShouldBe('L');
            waitingArea.GetInfo(9, 0).ShouldBe('L');
            waitingArea.GetInfo(9, 6).ShouldBe('.');
        }

        [Fact]
        public void TestApplySeatingPartOne()
        {
            var state0 = LoadInput("input_1_state_0.txt");
            var state1 = LoadInput("input_1_state_1.txt");
            var state2 = LoadInput("input_1_state_2.txt");
            var state3 = LoadInput("input_1_state_3.txt");
            var state4 = LoadInput("input_1_state_4.txt");
            var state5 = LoadInput("input_1_state_5.txt");
            var waitingArea = new WaitingArea(state0);
            waitingArea.State.ShouldBe(state0);
            waitingArea.ApplySeatingPartOne(); // Round 1
            waitingArea.State.ShouldBe(state1);
            waitingArea.ApplySeatingPartOne(); // Round 2
            waitingArea.State.ShouldBe(state2);
            waitingArea.ApplySeatingPartOne(); // Round 3
            waitingArea.State.ShouldBe(state3);
            waitingArea.ApplySeatingPartOne(); // Round 4
            waitingArea.State.ShouldBe(state4);
            waitingArea.ApplySeatingPartOne(); // Round 5
            waitingArea.State.ShouldBe(state5);
            waitingArea.SeatsTaken.ShouldBe(37);
        }
        [Fact]
        public void TestApplySeatingPartTwo()
        {
            var state0 = LoadInput("input_2_state_0.txt");
            var state1 = LoadInput("input_2_state_1.txt");
            var state2 = LoadInput("input_2_state_2.txt");
            var state3 = LoadInput("input_2_state_3.txt");
            var state4 = LoadInput("input_2_state_4.txt");
            var state5 = LoadInput("input_2_state_5.txt");
            var state6 = LoadInput("input_2_state_6.txt");
            var waitingArea = new WaitingArea(state0);
            waitingArea.State.ShouldBe(state0);
            waitingArea.ApplySeatingPartTwo(); // Round 1
            waitingArea.State.ShouldBe(state1);
            waitingArea.ApplySeatingPartTwo(); // Round 2
            waitingArea.State.ShouldBe(state2);
            waitingArea.ApplySeatingPartTwo(); // Round 3
            waitingArea.State.ShouldBe(state3);
            waitingArea.ApplySeatingPartTwo(); // Round 4
            waitingArea.State.ShouldBe(state4);
            waitingArea.ApplySeatingPartTwo(); // Round 5
            waitingArea.State.ShouldBe(state5);
            waitingArea.ApplySeatingPartTwo(); // Round 6
            waitingArea.State.ShouldBe(state6);
            waitingArea.ApplySeatingPartTwo(); // Round 7 (should be no change)
            waitingArea.State.ShouldBe(state6);
            waitingArea.SeatsTaken.ShouldBe(26);
        }

        [Theory]
        [InlineData("input_1_state_0.txt", 0, 0, false)]
        [InlineData("input_1_state_2.txt", 0, 0, true)]
        [InlineData("input_1_state_2.txt", 6, 7, false)]
        public void TestAnyOccupiedSeatsAround(string filename, int x, int y, bool result)
        {
            var area = LoadInput(filename);
            var waitingArea = new WaitingArea(area);
            waitingArea.AnyOccupiedSeatsAround(x, y).ShouldBe(result);
        }

        [Theory]
        [InlineData("input_2_state_t1.txt", 3, 4, true)]
        [InlineData("input_2_state_t2.txt", 3, 3, false)]
        [InlineData("input_2_state_t3.txt", 1, 1, false)]
        [InlineData("input_2_state_t3.txt", 3, 1, true)]
        public void TestAnyOccupiedSeatsInSight(string filename, int x, int y, bool result)
        {
            var area = LoadInput(filename);
            var waitingArea = new WaitingArea(area);
            waitingArea.AnyOccupiedSeatsInSight(x, y).ShouldBe(result);
        }

        [Theory]
        [InlineData("input_1_state_0.txt", 0, 0, false)]
        [InlineData("input_1_state_1.txt", 0, 0, false)]
        [InlineData("input_1_state_1.txt", 1, 2, true)]
        [InlineData("input_1_state_1.txt", 9, 2, true)]
        [InlineData("input_1_state_5.txt", 7, 4, false)]
        [InlineData("input_1_state_5.txt", 5, 4, true)]
        public void TestFourOrMoreSeatsOccupiedAround(string filename, int x, int y, bool result)
        {
            var area = LoadInput(filename);
            var waitingArea = new WaitingArea(area);
            waitingArea.FourOrMoreSeatsOccupiedAround(x, y).ShouldBe(result);
        }
        [Theory]
        [InlineData("input_2_state_t1.txt", 3, 4, true)]
        [InlineData("input_2_state_t2.txt", 3, 3, false)]
        [InlineData("input_2_state_t2.txt", 0, 3, false)]
        [InlineData("input_2_state_t2.txt", 1, 5, true)]
        public void TestFiveOrMourSeatsInSight(string filename, int x, int y, bool result)
        {
            var area = LoadInput(filename);
            var waitingArea = new WaitingArea(area);
            waitingArea.FiveOrMoreSeatsInSight(x, y).ShouldBe(result);
        }
        [Theory]
        [InlineData(0, 0, 'L')]
        [InlineData(100, 100, '.')]
        [InlineData(-1, -1, '.')]
        [InlineData(-100, 100, '.')]
        public void TestGetInfo(int x, int y, char result)
        {
            var area = LoadInput("input_1_state_0.txt");

            var waitingArea = new WaitingArea(area);
            waitingArea.GetInfo(x, y).ShouldBe(result);
        }

        [Theory]
        [InlineData("input_1_state_0.txt", 0)]
        [InlineData("input_1_state_5.txt", 37)]
        [InlineData("input_2_state_6.txt", 26)]
        public void TestSeatsTaken(string filename, int seatsTaken)
        {
            var area = LoadInput(filename);
            var waitingArea = new WaitingArea(area);
            waitingArea.SeatsTaken.ShouldBe(seatsTaken);
        }
        private char[,] LoadInput(string filename)
        {
            var lines = File.ReadAllLines(filename)
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .Select(l => l.ToCharArray())
            .ToList();

            var area = new char[lines.First().Length, lines.Count];
            var y = 0;
            lines.ForEach(l =>
                {
                    for (int x = 0; x < l.Length; x++)
                    {
                        area.SetValue(l[x], x, y);
                    }
                    y++;
                }
            );

            return area;
        }
    }
}
