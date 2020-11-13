namespace N15G.Battleship
{
    public interface IShip
    {
        public uint Length { get; }

        /**
         * Record of damage to hull segments.
         */
        public bool[] Damage { get; }

        public bool IsSunk { get; }

        /**
         * Damage the hull at the given index.
         */
        public void ApplyDamage(int index);
    }
}