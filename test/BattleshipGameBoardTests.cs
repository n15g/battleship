using System;
using AssertNet;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

namespace N15G.Battleship
{
    public class BattleshipGameBoardTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestDefaults()
        {
            var game = new BattleshipGameBoard();

            Assertions.AssertThat(game.SizeX).IsEqualTo(10);
            Assertions.AssertThat(game.SizeY).IsEqualTo(10);
        }

        [Test]
        public void TestCustomGameSize()
        {
            var game = new BattleshipGameBoard(2, 3);

            Assertions.AssertThat(game.SizeX).IsEqualTo(2);
            Assertions.AssertThat(game.SizeY).IsEqualTo(3);
        }

        [Test]
        public void TestGameGridIsInitialized()
        {
            var game = new BattleshipGameBoard();

            for (uint x = 0; x < game.SizeX; x++)
            {
                for (uint y = 0; y < game.SizeY; y++)
                {
                    var cell = game.Grid[x, y];

                    Assertions.AssertThat(cell).IsNotNull();
                    Assertions.AssertThat(cell.Marked).IsFalse();
                }
            }
        }

        [Test]
        public void TestGetBoundingBoxForEastWestHeading()
        {
            var ewBattleShip = new BattleshipGameBoard.FleetEntry(
                new Battleship(),
                3, 4,
                Heading.EastWest
            ).GetBoundingBox();

            Assertions.AssertThat(ewBattleShip.Left).IsEqualTo(3);
            Assertions.AssertThat(ewBattleShip.Top).IsEqualTo(4);
            Assertions.AssertThat(ewBattleShip.Right).IsEqualTo(7);
            Assertions.AssertThat(ewBattleShip.Bottom).IsEqualTo(4);
        }

        [Test]
        public void TestGetBoundingBoxForNorthSouthHeading()
        {
            var nsCruiser = new BattleshipGameBoard.FleetEntry(
                new Cruiser(),
                3, 4,
                Heading.NorthSouth
            ).GetBoundingBox();

            Assertions.AssertThat(nsCruiser.Left).IsEqualTo(3);
            Assertions.AssertThat(nsCruiser.Top).IsEqualTo(4);
            Assertions.AssertThat(nsCruiser.Right).IsEqualTo(3);
            Assertions.AssertThat(nsCruiser.Bottom).IsEqualTo(7);
        }

        [Test]
        public void TestInsertShipWithinGameBounds()
        {
            new BattleshipGameBoard()
                .PlaceShip(new Carrier(), 0, 0, Heading.EastWest);
        }

        [Test]
        public void TestInsertShipRightOnXBoundary()
        {
            new BattleshipGameBoard()
                .PlaceShip(new Carrier(), 5, 0, Heading.EastWest);
        }

        [Test]
        public void TestInsertShipRightOnYBoundary()
        {
            new BattleshipGameBoard()
                .PlaceShip(new Carrier(), 0, 5, Heading.NorthSouth);
        }

        [Test]
        public void TestInsertShipExceedingXAxis()
        {
            Assertions.AssertThat(() =>
            {
                new BattleshipGameBoard()
                    .PlaceShip(new Carrier(), 8, 0, Heading.EastWest);
            }).ThrowsException(typeof(ArgumentOutOfRangeException));
        }

        [Test]
        public void TestInsertShipEntirelyOverXAxis()
        {
            Assertions.AssertThat(() =>
            {
                new BattleshipGameBoard()
                    .PlaceShip(new Carrier(), 15, 0, Heading.EastWest);
            }).ThrowsException(typeof(ArgumentOutOfRangeException));
        }

        [Test]
        public void TestInsertShipExceedingYAxis()
        {
            Assertions.AssertThat(() =>
            {
                new BattleshipGameBoard()
                    .PlaceShip(new Carrier(), 0, 8, Heading.NorthSouth);
            }).ThrowsException(typeof(ArgumentOutOfRangeException));
        }

        [Test]
        public void TestInsertShipEntirelyOverYAxis()
        {
            Assertions.AssertThat(() =>
            {
                new BattleshipGameBoard()
                    .PlaceShip(new Carrier(), 0, 15, Heading.EastWest);
            }).ThrowsException(typeof(ArgumentOutOfRangeException));
        }
    }
}