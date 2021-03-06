using System.Collections.Generic;

namespace N15G.Battleship
{
    public interface IGameBoard
    {
        /// <summary>
        /// Game grid size along the x-axis.
        /// </summary>
        public int SizeX { get; }

        /// <summary>
        /// Game grid size along the y-axis.
        /// </summary>
        public int SizeY { get; }

        /// <summary>
        /// Current state of the game grid.
        /// </summary>
        public bool[,] Grid { get; }

        /// <summary>
        /// Returns whether the game is over (all remaining ships have sunk).
        /// </summary>
        public bool GameOver { get; }

        /// <summary>
        /// The players 'fleet'.
        /// A list of all the ships added to the game board, their location and heading. 
        /// </summary>
        public IReadOnlyList<IFleetEntry> Fleet { get; }


        /// <summary>
        /// Place a <see cref="IShip">Ship</see> on the game board.
        /// </summary>
        /// <param name="ship">The ship to place.</param>
        /// <param name="x">Ship origin x coordinate.</param>
        /// <param name="y">Ship origin y coordinate.</param>
        /// <param name="facing">Whether the ship is facing horizontally or vertically.</param>
        /// <returns>this</returns>
        public BattleshipGameBoard PlaceShip(IShip ship, int x, int y, Facing facing);

        /// <summary>
        /// Register an attack at the given coordinates.
        /// </summary>
        /// <param name="x">X coord.</param>
        /// <param name="y">Y coord.</param>
        /// <returns>Result of the attack. <see cref="AttackResult"/></returns>
        public AttackResult Attack(int x, int y);

        /// <summary>
        /// Represents the placement of a <see cref="IShip">Ship</see> on the game board, adding it to the
        /// players 'fleet'.
        /// </summary>
        public interface IFleetEntry
        {
            public IShip Ship { get; }
            public int X { get; }
            public int Y { get; }
            public Facing Facing { get; }
        }

        /// <summary>
        /// The result of an attack on the game board.
        /// Contains whether the attack resulted in a hit or miss, and whether the game is currently over. 
        /// </summary>
        public readonly struct AttackResult
        {
            public bool Hit { get; }
            public bool GameOver { get; }

            public AttackResult(bool hit, bool gameOver)
            {
                Hit = hit;
                GameOver = gameOver;
            }
        }
    }
}