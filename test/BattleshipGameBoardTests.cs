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

            Assertions.AssertThat(game.SizeX).IsEqualTo(8);
            Assertions.AssertThat(game.SizeY).IsEqualTo(8);
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
    }
}