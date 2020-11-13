using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace N15G.Battleship
{
    public class BattleshipGameBoard : IGameBoard
    {
        private readonly List<FleetEntry> _fleet = new List<FleetEntry>();

        public int SizeX => Grid.GetLength(0);
        public int SizeY => Grid.GetLength(1);

        public bool[,] Grid { get; }
        public bool GameOver => _fleet.Count != 0 && _fleet.All(ship => ship.Ship.IsSunk);
        public IReadOnlyList<IGameBoard.IFleetEntry> Fleet => _fleet;


        public BattleshipGameBoard(int sizeX = 10, int sizeY = 10)
        {
            Grid = new bool[sizeX, sizeY];
        }

        public BattleshipGameBoard PlaceShip(IShip ship, int x, int y, Facing facing)
        {
            var fleetEntry = new FleetEntry(ship, x, y, facing);

            AssertGridBoundary(fleetEntry);
            AssertNoCollisions(fleetEntry);

            _fleet.Add(fleetEntry);

            return this;
        }

        public IGameBoard.AttackResult Attack(int x, int y)
        {
            if (Grid[x, y]) throw new ArgumentException("This cell has already been targeted");

            Grid[x, y] = true;

            var hitShip = _fleet.Find(ship => ship.GetBoundingBox().Contains(new Point(x, y)));
            if (hitShip != null)
            {
                RegisterHit(hitShip, x, y);
            }

            return new IGameBoard.AttackResult(hitShip != null, GameOver);
        }

        private static void RegisterHit(IGameBoard.IFleetEntry hitShip, int x, int y)
        {
            var damageIndex = hitShip.Facing == Facing.Horizontal
                ? x - hitShip.X
                : y - hitShip.Y;

            hitShip.Ship.ApplyDamage(damageIndex);
        }

        private void AssertGridBoundary(FleetEntry entry)
        {
            var gridBounds = GetGridBoundingBox();
            var shipBounds = entry.GetBoundingBox();

            if (!gridBounds.Contains(shipBounds))
            {
                throw new ArgumentOutOfRangeException(nameof(entry), "Ship placement exceeds game boundaries");
            }
        }

        private void AssertNoCollisions(FleetEntry entry)
        {
            var shipBounds = entry.GetBoundingBox();

            if (_fleet.Any(existingShip => existingShip.GetBoundingBox().IntersectsWith(shipBounds)))
            {
                throw new ArgumentException("Ship placement would collide with an existing ship");
            }
        }

        private Rectangle GetGridBoundingBox()
        {
            return new Rectangle(0, 0, SizeX, SizeY);
        }

        public class FleetEntry : IGameBoard.IFleetEntry
        {
            public IShip Ship { get; }
            public int X { get; }
            public int Y { get; }
            public Facing Facing { get; }

            public FleetEntry(IShip ship, int x, int y, Facing facing)
            {
                Ship = ship;
                X = x;
                Y = y;
                Facing = facing;
            }

            public Rectangle GetBoundingBox()
            {
                return Facing == Facing.Horizontal
                    ? new Rectangle(X, Y, Ship.Length, 1)
                    : new Rectangle(X, Y, 1, Ship.Length);
            }
        }
    }
}