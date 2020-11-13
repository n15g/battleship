using AssertNet;
using NUnit.Framework;

namespace N15G.Battleship
{
    public class AbstractShipTests
    {
        [Test]
        public void TestConstructDefaults()
        {
            var ship = new BaseShip(10);
            Assertions.AssertThat(ship.Length).IsEqualTo(10);
            Assertions.AssertThat(ship.Damage)
                .HasSize(10)
                .AllSatisfy(x => x == false);
            Assertions.AssertThat(ship.IsSunk).IsFalse();
        }

        [Test]
        public void TestHullLengths()
        {
            Assertions.AssertThat(new Carrier().Length).IsEqualTo(5);
            Assertions.AssertThat(new Battleship().Length).IsEqualTo(4);
            Assertions.AssertThat(new Cruiser().Length).IsEqualTo(3);
            Assertions.AssertThat(new Submarine().Length).IsEqualTo(3);
            Assertions.AssertThat(new Destroyer().Length).IsEqualTo(2);
        }

        [Test]
        public void TestApplyDamage()
        {
            var ship = new BaseShip(3);

            Assertions.AssertThat(ship.Damage).ContainsExactly(false, false, false);

            ship.ApplyDamage(0);
            Assertions.AssertThat(ship.Damage).ContainsExactly(true, false, false);
            Assertions.AssertThat(ship.IsSunk).IsFalse();

            ship.ApplyDamage(2);
            Assertions.AssertThat(ship.Damage).ContainsExactly(true, false, true);
            Assertions.AssertThat(ship.IsSunk).IsFalse();

            ship.ApplyDamage(1);
            Assertions.AssertThat(ship.Damage).ContainsExactly(true, true, true);
            Assertions.AssertThat(ship.IsSunk).IsTrue();
        }
    }
}