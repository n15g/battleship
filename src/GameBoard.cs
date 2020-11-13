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
        public IReadOnlyList<IGameBoard.IFleetEntry> Fleet => _fleet;


        public BattleshipGameBoard(uint sizeX = 10, uint sizeY = 10)
        {
            Grid = new bool[sizeX, sizeY];
        }

        public BattleshipGameBoard PlaceShip(IShip ship, uint x, uint y, Facing facing)
        {
            var fleetEntry = new FleetEntry(ship, x, y, facing);

            AssertGridBoundary(fleetEntry);
            AssertNoCollisions(fleetEntry);

            _fleet.Add(fleetEntry);

            return this;
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
            public uint X { get; }
            public uint Y { get; }
            public Facing Facing { get; }

            public FleetEntry(IShip ship, uint x, uint y, Facing facing)
            {
                Ship = ship;
                X = x;
                Y = y;
                Facing = facing;
            }

            public Rectangle GetBoundingBox()
            {
                return Facing == Facing.Horizontal
                    ? new Rectangle((int) X, (int) Y, (int) Ship.Length, 1)
                    : new Rectangle((int) X, (int) Y, 1, (int) Ship.Length);
            }
        }
    }
}