using System;
using System.IO;
using System.Linq;

namespace day11
{
    class Program
    {
        static void Main(string[] args)
        {
            var area1 = LoadInput("input.txt");
            var waitingArea1 = new WaitingArea(area1);
            PartOne(waitingArea1);

            var area2 = LoadInput("input.txt");
            var waitingArea2 = new WaitingArea(area2);
            PartTwo(waitingArea2);
        }

        private static void PartTwo(WaitingArea waitingArea)
        {
            var seatingsChanged = false;
            var i = 0;
            do
            {
                seatingsChanged = waitingArea.ApplySeatingPartTwo();
                i++;
            } while (seatingsChanged);

            System.Console.WriteLine($"Part two: {i} iterations, {waitingArea.SeatsTaken} seats taken");
        }

        private static void PartOne(WaitingArea waitingArea)
        {
            var seatingsChanged = false;
            var i = 0;
            do
            {
                seatingsChanged = waitingArea.ApplySeatingPartOne();
                i++;
            } while (seatingsChanged);

            System.Console.WriteLine($"Part one: {i} iterations, {waitingArea.SeatsTaken} seats taken");
        }

        static char[,] LoadInput(string filename)
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

    public class WaitingArea
    {
        private readonly char[,] state;

        public WaitingArea(char[,] state)
        {
            this.state = state;
        }

        public int SizeX => this.state.GetLength(0);
        public int SizeY => this.state.GetLength(1);

        public char[,] State { get => this.state; }
        public int SeatsTaken
        {
            get
            {
                var s = "";
                foreach (var item in this.state)
                {
                    s += item;
                }
                return s.Count(s => s == '#');
            }
        }

        public char GetInfo(int x, int y)
        {
            if (x < 0 || x >= SizeX) return '.';
            if (y < 0 || y >= SizeY) return '.';
            return this.state[x, y];
        }
        public bool ApplySeatingPartOne()
        {
            var newState = new char[SizeX, SizeY];
            var changed = false;

            Array.Copy(this.state, newState, SizeX * SizeY);

            for (int y = 0; y < SizeY; y++)
            {
                for (int x = 0; x < SizeX; x++)
                {
                    switch (newState[x, y])
                    {
                        case 'L':
                            if (!AnyOccupiedSeatsAround(x, y))
                            {
                                newState[x, y] = '#';
                                changed = true;
                            }
                            break;
                        case '#':
                            if (FourOrMoreSeatsOccupiedAround(x, y))
                            {
                                newState[x, y] = 'L';
                                changed = true;
                            }
                            break;
                        case '.':
                            break;
                    }
                }
            }

            Array.Copy(newState, this.state, SizeX * SizeY);

            return changed;
        }

        public bool ApplySeatingPartTwo()
        {
            var newState = new char[SizeX, SizeY];
            var changed = false;

            Array.Copy(this.state, newState, SizeX * SizeY);

            for (int y = 0; y < SizeY; y++)
            {
                for (int x = 0; x < SizeX; x++)
                {
                    switch (newState[x, y])
                    {
                        case 'L':
                            if (!AnyOccupiedSeatsInSight(x, y))
                            {
                                newState[x, y] = '#';
                                changed = true;
                            }
                            break;
                        case '#':
                            if (FiveOrMoreSeatsInSight(x, y))
                            {
                                newState[x, y] = 'L';
                                changed = true;
                            }
                            break;
                        case '.':
                            break;
                    }
                }
            }

            Array.Copy(newState, this.state, SizeX * SizeY);

            return changed;
        }

        public bool FourOrMoreSeatsOccupiedAround(int x, int y)
        {
            // Test everything around (not (x,y))
            var around = GatherInfoFromNearbySquares(x, y);
            return around.Count(p => p == '#') >= 4;
        }
        public bool FiveOrMoreSeatsInSight(int x, int y)
        {
            // Test everything in sight (not (x,y))
            var inSight = GatherInfoFromWhatYouSee(x, y);
            return inSight.Count(p => p == '#') >= 5;
        }
        public bool AnyOccupiedSeatsAround(int x, int y)
        {
            // Test everything around (not (x,y))
            var around = GatherInfoFromNearbySquares(x, y);
            return around.Contains('#');
        }

        public bool AnyOccupiedSeatsInSight(int x, int y)
        {
            // Test everything in sight (not (x,y))
            var inSight = GatherInfoFromWhatYouSee(x, y);
            return inSight.Contains('#');
        }

        private string GatherInfoFromNearbySquares(int x, int y)
        {
            var around = "";

            // Top
            around += GetInfo(x - 1, y - 1);
            around += GetInfo(x, y - 1);
            around += GetInfo(x + 1, y - 1);

            // Mid
            around += GetInfo(x - 1, y);
            around += GetInfo(x + 1, y);

            // Bottom
            around += GetInfo(x - 1, y + 1);
            around += GetInfo(x, y + 1);
            around += GetInfo(x + 1, y + 1);

            return around;
        }

        private string GatherInfoFromWhatYouSee(int x, int y)
        {
            var around = "";
            // W
            {
                var xt = x - 1;
                var yt = y;
                while (xt >= 0)
                {
                    var pt = GetInfo(xt, yt);
                    if (pt != '.')
                    {
                        around += pt;
                        break;
                    }
                    xt--;
                }
            }

            // NW
            {
                var xt = x - 1;
                var yt = y - 1;
                while (xt >= 0 && yt >= 0)
                {
                    var pt = GetInfo(xt, yt);
                    if (pt != '.')
                    {
                        around += pt;
                        break;
                    }
                    xt--;
                    yt--;
                }
            }

            // N
            {
                var xt = x;
                var yt = y - 1;
                while (yt >= 0)
                {
                    var pt = GetInfo(xt, yt);
                    if (pt != '.')
                    {
                        around += pt;
                        break;
                    }
                    yt--;
                }
            }

            // NE
            {
                var xt = x + 1;
                var yt = y - 1;
                while (xt < SizeX && yt >= 0)
                {
                    var pt = GetInfo(xt, yt);
                    if (pt != '.')
                    {
                        around += pt;
                        break;
                    }
                    xt++;
                    yt--;
                }
            }

            // E
            {
                var xt = x + 1;
                var yt = y;
                while (xt < SizeX)
                {
                    var pt = GetInfo(xt, yt);
                    if (pt != '.')
                    {
                        around += pt;
                        break;
                    }
                    xt++;
                }
            }

            // SE
            {
                var xt = x + 1;
                var yt = y + 1;
                while (xt < SizeX && yt < SizeY)
                {
                    var pt = GetInfo(xt, yt);
                    if (pt != '.')
                    {
                        around += pt;
                        break;
                    }
                    xt++;
                    yt++;
                }
            }

            // S
            {
                var xt = x;
                var yt = y + 1;
                while (yt < SizeY)
                {
                    var pt = GetInfo(xt, yt);
                    if (pt != '.')
                    {
                        around += pt;
                        break;
                    }
                    yt++;
                }
            }

            // SW
            {
                var xt = x - 1;
                var yt = y + 1;
                while (xt >= 0 && yt < SizeY)
                {
                    var pt = GetInfo(xt, yt);
                    if (pt != '.')
                    {
                        around += pt;
                        break;
                    }
                    xt--;
                    yt++;
                }
            }

            return around;
        }
    }
}
