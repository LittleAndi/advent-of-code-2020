using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace day12
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt")
                .Where(l => !string.IsNullOrWhiteSpace(l))
                .ToList();

            PartOne(lines);
            PartTwo(lines);
        }

        private static void PartTwo(List<string> lines)
        {
            Regex regex = new Regex(@"(\w)(\d+)");
            var ship = new Ship();

            foreach (var line in lines)
            {
                var matches = regex.Match(line);
                var command = Enum.Parse<Ship.Command>(matches.Groups[1].Value);
                var value = int.Parse(matches.Groups[2].Value);

                switch (command)
                {
                    case Ship.Command.L:
                        ship.RotateWaypoint(Ship.TurnDirection.L, value);
                        break;
                    case Ship.Command.R:
                        ship.RotateWaypoint(Ship.TurnDirection.R, value);
                        break;
                    case Ship.Command.N:
                        ship.MoveWaypoint(Ship.Direction.N, value);
                        break;
                    case Ship.Command.S:
                        ship.MoveWaypoint(Ship.Direction.S, value);
                        break;
                    case Ship.Command.W:
                        ship.MoveWaypoint(Ship.Direction.W, value);
                        break;
                    case Ship.Command.E:
                        ship.MoveWaypoint(Ship.Direction.E, value);
                        break;
                    case Ship.Command.F:
                        ship.MoveTowardsWaypoint(value);
                        break;
                    default:
                        System.Console.WriteLine("Unknown command");
                        break;
                }
            }
            System.Console.WriteLine($"Part two: Manhattan distance of {ship.CurrentPosition} is {Math.Abs(ship.CurrentPosition.Item1) + Math.Abs(ship.CurrentPosition.Item2)}");
        }
        private static void PartOne(List<string> lines)
        {
            Regex regex = new Regex(@"(\w)(\d+)");
            var ship = new Ship();
            foreach (var line in lines)
            {
                var matches = regex.Match(line);
                var command = Enum.Parse<Ship.Command>(matches.Groups[1].Value);
                var value = int.Parse(matches.Groups[2].Value);

                switch (command)
                {
                    case Ship.Command.L:
                        ship.Turn(Ship.TurnDirection.L, value);
                        break;
                    case Ship.Command.R:
                        ship.Turn(Ship.TurnDirection.R, value);
                        break;
                    case Ship.Command.N:
                        ship.Move(Ship.Direction.N, value);
                        break;
                    case Ship.Command.S:
                        ship.Move(Ship.Direction.S, value);
                        break;
                    case Ship.Command.W:
                        ship.Move(Ship.Direction.W, value);
                        break;
                    case Ship.Command.E:
                        ship.Move(Ship.Direction.E, value);
                        break;
                    case Ship.Command.F:
                        ship.MoveForward(value);
                        break;
                    default:
                        System.Console.WriteLine("Unknown command");
                        break;
                }
            }

            System.Console.WriteLine($"Part one: Manhattan distance of {ship.CurrentPosition} is {Math.Abs(ship.CurrentPosition.Item1) + Math.Abs(ship.CurrentPosition.Item2)}");
        }
    }

    public class Ship
    {
        public enum Direction : byte { E, S, W, N }
        public enum TurnDirection : byte { L, R }
        public enum Command : byte { N, S, E, W, L, R, F }
        int shipPosEast = 0;
        int shipPosNorth = 0;
        int waypointRelativePosEast = 10;
        int waypointRelativePosNorth = 1;
        Direction currentDirection = Direction.E;
        public (int, int) CurrentPosition => (this.shipPosEast, this.shipPosNorth);
        public (int, int) CurrentRelativeWaypointPosition => (this.waypointRelativePosEast, this.waypointRelativePosNorth);
        public (int, int) CurrentWaypointPosition => (this.shipPosEast + this.waypointRelativePosEast, this.shipPosNorth + this.waypointRelativePosNorth);
        public Direction CurrentDirection => this.currentDirection;

        public void Turn(TurnDirection turnDirection, int degrees)
        {
            if (turnDirection is TurnDirection.L) degrees = -degrees;
            byte steps = (byte)((((degrees / 90) % 4) + 4) % 4);
            for (byte i = 0; i < steps; i++)
            {
                this.currentDirection = this.currentDirection.Next();
            }
        }

        public void Move(Direction direction, int value)
        {
            switch (direction)
            {
                case Direction.N:
                    this.shipPosNorth += value;
                    break;
                case Direction.S:
                    this.shipPosNorth -= value;
                    break;
                case Direction.E:
                    this.shipPosEast += value;
                    break;
                case Direction.W:
                    this.shipPosEast -= value;
                    break;
            }
        }

        public void MoveForward(int value)
        {
            Move(this.currentDirection, value);
        }
        public void MoveWaypoint(Direction direction, int value)
        {
            switch (direction)
            {
                case Direction.N:
                    this.waypointRelativePosNorth += value;
                    break;
                case Direction.S:
                    this.waypointRelativePosNorth -= value;
                    break;
                case Direction.E:
                    this.waypointRelativePosEast += value;
                    break;
                case Direction.W:
                    this.waypointRelativePosEast -= value;
                    break;
            }
        }
        public void MoveTowardsWaypoint(int value)
        {
            this.shipPosEast += this.waypointRelativePosEast * value;
            this.shipPosNorth += this.waypointRelativePosNorth * value;
        }

        public void RotateWaypoint(TurnDirection turnDirection, int degrees)
        {
            if (turnDirection is TurnDirection.R) degrees = -degrees;
            var angle = degrees * Math.PI / 180;
            var east = (int)Math.Round(Math.Cos(angle) * this.waypointRelativePosEast - Math.Sin(angle) * this.waypointRelativePosNorth, 0);
            var north = (int)Math.Round(Math.Sin(angle) * this.waypointRelativePosEast + Math.Cos(angle) * this.waypointRelativePosNorth, 0);
            this.waypointRelativePosEast = east;
            this.waypointRelativePosNorth = north;
        }
    }

    public static class Extensions
    {

        public static T Next<T>(this T src) where T : struct
        {
            if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argument {0} is not an Enum", typeof(T).FullName));

            T[] Arr = (T[])Enum.GetValues(src.GetType());
            int j = Array.IndexOf<T>(Arr, src) + 1;
            return (Arr.Length == j) ? Arr[0] : Arr[j];
        }
    }
}
