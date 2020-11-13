using System;
using System.Data;
using System.Drawing;
using System.Numerics;
using AssertNet;

namespace N15G.Battleship
{
    public class BattleshipGameBoard
    {
        public readonly uint SizeX;
        public readonly uint SizeY;

        public readonly Cell[,] Grid;
        public readonly FleetEntry[] Fleet = { };

        public BattleshipGameBoard(uint sizeX = 10, uint sizeY = 10)
        {
            SizeX = sizeX;
            SizeY = sizeY;

            Grid = new Cell[SizeX, SizeY];

            for (uint x = 0; x < SizeX; x++)
            {
                for (uint y = 0; y < SizeX; y++)
                {
                    Grid[x, y] = new Cell();
                }
            }
        }

        public BattleshipGameBoard PlaceShip(IShip ship, uint x, uint y, Heading heading)
        {
            var fleetEntry = new FleetEntry(ship, x, y, heading);
            AssertGridBoundary(fleetEntry);

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

        private Rectangle GetGridBoundingBox()
        {
            return new Rectangle(0, 0, (int) SizeX, (int) SizeY);
        }

        public class Cell
        {
            public bool Marked = false;
        }

        public class FleetEntry
        {
            public readonly IShip Ship;
            public readonly uint X;
            public readonly uint Y;
            public readonly Heading Heading;

            public FleetEntry(IShip ship, uint x, uint y, Heading heading)
            {
                Ship = ship;
                this.X = x;
                this.Y = y;
                Heading = heading;
            }

            public Rectangle GetBoundingBox()
            {
                return Heading == Heading.EastWest
                    ? new Rectangle((int) X, (int) Y, (int) Ship.Length, 0)
                    : new Rectangle((int) X, (int) Y, 0, (int) Ship.Length);
            }
        }
    }
}