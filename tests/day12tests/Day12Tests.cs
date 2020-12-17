using System;
using Xunit;
using day12;
using Shouldly;

namespace day12tests
{
    public class Day12Tests
    {
        [Theory]
        [InlineData(Ship.TurnDirection.R, 0, Ship.Direction.E)]
        [InlineData(Ship.TurnDirection.R, 90, Ship.Direction.S)]
        [InlineData(Ship.TurnDirection.R, 180, Ship.Direction.W)]
        [InlineData(Ship.TurnDirection.R, 270, Ship.Direction.N)]
        [InlineData(Ship.TurnDirection.R, 360, Ship.Direction.E)]
        [InlineData(Ship.TurnDirection.L, 90, Ship.Direction.N)]
        [InlineData(Ship.TurnDirection.L, 180, Ship.Direction.W)]
        [InlineData(Ship.TurnDirection.L, 270, Ship.Direction.S)]
        [InlineData(Ship.TurnDirection.L, 360, Ship.Direction.E)]
        public void TestTurning(Ship.TurnDirection turnDirection, int degrees, Ship.Direction direction)
        {
            var ship = new Ship();
            ship.Turn(turnDirection, degrees);
            ship.CurrentDirection.ShouldBe(direction);
        }

        [Theory]
        [InlineData(Ship.Command.N, 10, 0, 10)]
        [InlineData(Ship.Command.S, 10, 0, -10)]
        [InlineData(Ship.Command.E, 10, 10, 0)]
        [InlineData(Ship.Command.W, 10, -10, 0)]
        [InlineData(Ship.Command.F, 10, 10, 0)]
        public void TestMoving(Ship.Command command, int value, int expectedEast, int expectedNorth)
        {
            var ship = new Ship();
            switch (command)
            {
                case Ship.Command.N:
                    ship.Move(Ship.Direction.N, value);
                    break;
                case Ship.Command.S:
                    ship.Move(Ship.Direction.S, value);
                    break;
                case Ship.Command.E:
                    ship.Move(Ship.Direction.E, value);
                    break;
                case Ship.Command.W:
                    ship.Move(Ship.Direction.W, value);
                    break;
                case Ship.Command.F:
                    ship.MoveForward(value);
                    break;
            }

            ship.CurrentPosition.ShouldBe((expectedEast, expectedNorth));
        }

        [Fact]
        public void TestSample()
        {
            var ship = new Ship();
            ship.CurrentDirection.ShouldBe(Ship.Direction.E);
            ship.CurrentPosition.ShouldBe((0, 0));
            ship.MoveForward(10);
            ship.CurrentDirection.ShouldBe(Ship.Direction.E);
            ship.CurrentPosition.ShouldBe((10, 0));
            ship.Move(Ship.Direction.N, 3);
            ship.CurrentDirection.ShouldBe(Ship.Direction.E);
            ship.CurrentPosition.ShouldBe((10, 3));
            ship.MoveForward(7);
            ship.CurrentDirection.ShouldBe(Ship.Direction.E);
            ship.CurrentPosition.ShouldBe((17, 3));
            ship.Turn(Ship.TurnDirection.R, 90);
            ship.CurrentDirection.ShouldBe(Ship.Direction.S);
            ship.CurrentPosition.ShouldBe((17, 3));
            ship.MoveForward(11);
            ship.CurrentDirection.ShouldBe(Ship.Direction.S);
            ship.CurrentPosition.ShouldBe((17, -8));
        }

        [Fact]
        public void TestSquareMovementCCW()
        {
            var ship = new Ship();
            ship.MoveForward(10);
            ship.CurrentPosition.ShouldBe((10, 0));
            ship.Turn(Ship.TurnDirection.L, 90);
            ship.MoveForward(10);
            ship.CurrentPosition.ShouldBe((10, 10));
            ship.Turn(Ship.TurnDirection.L, 90);
            ship.MoveForward(10);
            ship.CurrentPosition.ShouldBe((0, 10));
            ship.Turn(Ship.TurnDirection.L, 90);
            ship.MoveForward(10);
            ship.CurrentPosition.ShouldBe((0, 0));
        }
        [Fact]
        public void TestSquareMovementCW()
        {
            var ship = new Ship();
            ship.MoveForward(10);
            ship.Turn(Ship.TurnDirection.R, 90);
            ship.MoveForward(10);
            ship.Turn(Ship.TurnDirection.R, 90);
            ship.MoveForward(10);
            ship.Turn(Ship.TurnDirection.R, 90);
            ship.MoveForward(10);
            ship.CurrentPosition.ShouldBe((0, 0));
        }

        [Fact]
        public void TestMovingWithWaypoint()
        {
            var ship = new Ship();
            ship.CurrentPosition.ShouldBe((0, 0));
            ship.CurrentRelativeWaypointPosition.ShouldBe((10, 1));
            ship.MoveTowardsWaypoint(10);
            ship.CurrentPosition.ShouldBe((100, 10));
            ship.CurrentRelativeWaypointPosition.ShouldBe((10, 1));
            ship.MoveWaypoint(Ship.Direction.N, 3);
            ship.CurrentPosition.ShouldBe((100, 10));
            ship.CurrentRelativeWaypointPosition.ShouldBe((10, 4));
            ship.MoveTowardsWaypoint(7);
            ship.CurrentPosition.ShouldBe((170, 38));
            ship.CurrentRelativeWaypointPosition.ShouldBe((10, 4));
            ship.RotateWaypoint(Ship.TurnDirection.R, 90);
            ship.CurrentPosition.ShouldBe((170, 38));
            ship.CurrentRelativeWaypointPosition.ShouldBe((4, -10));
            ship.MoveTowardsWaypoint(11);
            ship.CurrentPosition.ShouldBe((214, -72));
            ship.CurrentRelativeWaypointPosition.ShouldBe((4, -10));
        }
    }
}
