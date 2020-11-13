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
        public BattleshipGameBoard PlaceShip(IShip ship, uint x, uint y, Facing facing);


        /// <summary>
        /// Represents the placement of a <see cref="IShip">Ship</see> on the game board, adding it to the
        /// players 'fleet'.
        /// </summary>
        public interface IFleetEntry
        {
            public IShip Ship { get; }
            public uint X { get; }
            public uint Y { get; }
            public Facing Facing { get; }
        }
    }
}