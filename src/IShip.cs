namespace N15G.Battleship
{
    public interface IShip
    {
        /// <summary>
        /// The length of the ship in grid squares.
        /// </summary>
        public int Length { get; }

        /// <summary>
        /// Record of damage to hull segments.
        /// </summary>
        public bool[] Damage { get; }

        /// <summary>
        /// True if every hull segment of the ship is damaged and therefore the ship is sunk.
        /// </summary>
        public bool IsSunk { get; }

        /// <summary>
        /// Damage the hull at the given index.
        /// </summary>
        /// <param name="index">The point (relative to the ship origin) at which to damage this ship.</param>
        public void ApplyDamage(int index);
    }
}