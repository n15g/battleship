using System;
using AssertNet;
using NUnit.Framework;

namespace N15G.Battleship
{
    public class BattleshipGameBoardTests
    {
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

            for (int x = 0; x < game.SizeX; x++)
            {
                for (int y = 0; y < game.SizeY; y++)
                {
                    var cell = game.Grid[x, y];
                    Assertions.AssertThat(cell).IsFalse();
                }
            }
        }

        [Test]
        public void TestGetBoundingBoxForEastWestHeading()
        {
            var ewBattleShip = new BattleshipGameBoard.FleetEntry(
                new Battleship(),
                3, 4,
                Facing.Horizontal
            ).GetBoundingBox();

            Assertions.AssertThat(ewBattleShip.Left).IsEqualTo(3);
            Assertions.AssertThat(ewBattleShip.Top).IsEqualTo(4);
            Assertions.AssertThat(ewBattleShip.Width).IsEqualTo(4);
            Assertions.AssertThat(ewBattleShip.Height).IsEqualTo(1);
        }

        [Test]
        public void TestGetBoundingBoxForNorthSouthHeading()
        {
            var nsCruiser = new BattleshipGameBoard.FleetEntry(
                new Cruiser(),
                3, 4,
                Facing.Vertical
            ).GetBoundingBox();

            Assertions.AssertThat(nsCruiser.Left).IsEqualTo(3);
            Assertions.AssertThat(nsCruiser.Top).IsEqualTo(4);
            Assertions.AssertThat(nsCruiser.Width).IsEqualTo(1);
            Assertions.AssertThat(nsCruiser.Height).IsEqualTo(3);
        }

        [Test]
        public void TestPlaceShipWithinGameBounds()
        {
            new BattleshipGameBoard()
                .PlaceShip(new Carrier(), 0, 0, Facing.Horizontal);
        }

        [Test]
        public void TestPlaceShipRightOnXBoundary()
        {
            new BattleshipGameBoard()
                .PlaceShip(new Carrier(), 5, 0, Facing.Horizontal);
        }

        [Test]
        public void TestPlaceShipRightOnYBoundary()
        {
            new BattleshipGameBoard()
                .PlaceShip(new Carrier(), 0, 5, Facing.Vertical);
        }

        [Test]
        public void TestPlaceShipExceedingXAxis()
        {
            Assertions.AssertThat(() =>
            {
                new BattleshipGameBoard()
                    .PlaceShip(new Carrier(), 8, 0, Facing.Horizontal);
            }).ThrowsException(typeof(ArgumentOutOfRangeException));
        }

        [Test]
        public void TestPlaceShipEntirelyOverXAxis()
        {
            Assertions.AssertThat(() =>
            {
                new BattleshipGameBoard()
                    .PlaceShip(new Carrier(), 15, 0, Facing.Horizontal);
            }).ThrowsException(typeof(ArgumentOutOfRangeException));
        }

        [Test]
        public void TestPlaceShipExceedingYAxis()
        {
            Assertions.AssertThat(() =>
            {
                new BattleshipGameBoard()
                    .PlaceShip(new Carrier(), 0, 8, Facing.Vertical);
            }).ThrowsException(typeof(ArgumentOutOfRangeException));
        }

        [Test]
        public void TestPlaceShipEntirelyOverYAxis()
        {
            Assertions.AssertThat(() =>
            {
                new BattleshipGameBoard()
                    .PlaceShip(new Carrier(), 0, 15, Facing.Horizontal);
            }).ThrowsException(typeof(ArgumentOutOfRangeException));
        }

        [Test]
        public void TestPlaceShipsNoCollision()
        {
            var game = new BattleshipGameBoard();

            game.PlaceShip(new Carrier(), 0, 0, Facing.Horizontal);

            game.PlaceShip(new Cruiser(), 1, 1, Facing.Horizontal);
        }

        [Test]
        public void TestPlaceShipParallelCollision()
        {
            var game = new BattleshipGameBoard();

            game.PlaceShip(new Carrier(), 0, 0, Facing.Horizontal);

            Assertions.AssertThat(() => { game.PlaceShip(new Cruiser(), 1, 0, Facing.Horizontal); })
                .ThrowsException(typeof(ArgumentException))
                .WithMessageContaining("collide");
        }

        [Test]
        public void TestPlaceShipPerpendicularCollision()
        {
            var game = new BattleshipGameBoard();

            game.PlaceShip(new Carrier(), 0, 1, Facing.Horizontal);

            Assertions.AssertThat(() => { game.PlaceShip(new Cruiser(), 1, 0, Facing.Vertical); })
                .ThrowsException(typeof(ArgumentException))
                .WithMessageContaining("collide");
        }

        [Test]
        public void TestGameOverWithNoFleet()
        {
            var game = new BattleshipGameBoard();

            Assertions.AssertThat(game.GameOver).IsFalse();
        }

        [Test]
        public void TestGameOverWithUndamagedFleet()
        {
            var game = new BattleshipGameBoard()
                .PlaceShip(new Destroyer(), 0, 0, Facing.Horizontal)
                .PlaceShip(new Carrier(), 0, 1, Facing.Vertical);

            Assertions.AssertThat(game.GameOver).IsFalse();
        }

        [Test]
        public void TestGameOverWithSunkFleet()
        {
            var game = new BattleshipGameBoard()
                .PlaceShip(new Destroyer(), 0, 0, Facing.Horizontal)
                .PlaceShip(new Carrier(), 1, 1, Facing.Vertical);

            game.Attack(0, 0);
            game.Attack(1, 0);

            game.Attack(1, 1);
            game.Attack(1, 2);
            game.Attack(1, 3);
            game.Attack(1, 4);
            game.Attack(1, 5);

            Assertions.AssertThat(game.GameOver).IsTrue();
        }

        [Test]
        public void TestAttackTwice()
        {
            var game = new BattleshipGameBoard();

            game.Attack(0, 0);
            Assertions.AssertThat(() => game.Attack(0, 0))
                .ThrowsException(typeof(ArgumentException))
                .WithMessageContaining("already");
        }


        [Test]
        public void TestAttackMiss()
        {
            var game = new BattleshipGameBoard()
                .PlaceShip(new Destroyer(), 0, 0, Facing.Horizontal)
                .PlaceShip(new Carrier(), 0, 1, Facing.Vertical);

            Assertions.AssertThat(game.Attack(3, 3).Hit).IsFalse();
        }

        [Test]
        public void TestAttackHit()
        {
            var game = new BattleshipGameBoard()
                .PlaceShip(new Destroyer(), 0, 0, Facing.Horizontal)
                .PlaceShip(new Carrier(), 0, 1, Facing.Vertical);

            Assertions.AssertThat(game.Attack(0, 0).Hit).IsTrue();
        }

        [Test]
        public void TestAttackGameOverWithSunkFleet()
        {
            var game = new BattleshipGameBoard()
                .PlaceShip(new Destroyer(), 0, 0, Facing.Horizontal)
                .PlaceShip(new Carrier(), 1, 1, Facing.Vertical);

            var attackResult = game.Attack(0, 0);
            game.Attack(1, 0);

            Assertions.AssertThat(attackResult.GameOver).IsFalse();

            game.Attack(1, 1);
            game.Attack(1, 2);
            game.Attack(1, 3);
            game.Attack(1, 4);
            attackResult = game.Attack(1, 5);

            Assertions.AssertThat(attackResult.GameOver).IsTrue();
        }
    }
}