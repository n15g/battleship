using System.Linq;

namespace N15G.Battleship
{
    public class BaseShip : IShip
    {
        private readonly uint _length;
        private readonly bool[] _damage;

        public uint Length => _length;
        public bool[] Damage => _damage;

        public bool IsSunk => _damage.All(x => x);

        public BaseShip(uint length)
        {
            _length = length;
            _damage = new bool[_length];
        }

        public void ApplyDamage(int index)
        {
            _damage[index] = true;
        }
    }

    public class Destroyer : BaseShip
    {
        public Destroyer() : base(2)
        {
        }
    }

    public class Cruiser : BaseShip
    {
        public Cruiser() : base(3)
        {
        }
    }

    public class Submarine : BaseShip
    {
        public Submarine() : base(3)
        {
        }
    }

    public class Battleship : BaseShip
    {
        public Battleship() : base(4)
        {
        }
    }

    public class Carrier : BaseShip
    {
        public Carrier() : base(5)
        {
        }
    }
}