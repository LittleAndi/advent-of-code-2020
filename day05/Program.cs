using System;
using System.IO;
using System.Linq;

namespace day05
{
    class Program
    {
        static void Main(string[] args)
        {
            var boardingPasses = File.ReadAllLines("input.txt")
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .Select(l => new BoardingPass(l))
                .ToList();
            
            System.Console.WriteLine($"Part one: The highest Seat ID for these boarding passes is {boardingPasses.Max(bp => bp.SeatId)}");

            var sumOfOtherSeatIds = boardingPasses.Sum(bp => bp.SeatId);
            var totalSum = Enumerable.Range(boardingPasses.Min(bp => bp.SeatId), (boardingPasses.Max(bp => bp.SeatId) - boardingPasses.Min(bp => bp.SeatId) + 1)).Sum();

            System.Console.WriteLine($"Part two: My Seat ID is {totalSum - sumOfOtherSeatIds}");
        }
    }

    public class BoardingPass
    {
        private readonly string boardingPassInfo;
        public BoardingPass(string boardingPassInfo)
        {
            this.boardingPassInfo = boardingPassInfo;

            int rowStartpos = 0;
            int rowEndpos = 127;
            int colStartpos = 0;
            int colEndpos = 7;

            for (int i = 0; i < this.boardingPassInfo.Length; i++)
            {
                switch (this.boardingPassInfo[i])
                {
                    case 'F':
                        rowEndpos -= (rowEndpos - rowStartpos) / 2 + 1;
                        break;
                    case 'B':
                        rowStartpos += (rowEndpos - rowStartpos) / 2 + 1;
                        break;
                    case 'L':
                        colEndpos -= (colEndpos - colStartpos) / 2 + 1;
                        break;
                    case 'R':
                        colStartpos += (colEndpos - colStartpos) / 2 + 1;
                        break;
                }
            }

            Row = rowStartpos;
            Column = colStartpos;
        }

        public int Row { get; set; }
        public int Column { get; set; }
        public int SeatId => Row * 8 + Column;
    }
}
