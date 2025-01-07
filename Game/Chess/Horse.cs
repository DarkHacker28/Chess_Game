using System;
using System.Collections.Generic;

using ChessGame.Board;
using ChessGame.Board.Exceptions;

namespace ChessGame.Chess
{
    sealed class Horse : Piece
    {
        /// <summary>
        /// Constructor function for this class
        /// </summary>
        /// <param name="ChessPlayBoard"></param>
        /// <param name="color"></param>
        public Horse (ChessGameBoard ChessPlayBoard, Color color)
            : base(ChessPlayBoard, color)
        {
        }

        /// <summary>
        /// This function has all the logic for Horse piece movement
        /// </summary>
        /// <returns>true or false</returns>
        public override bool[,] AllPossibleMovements ()
        {
            bool[,] movements = new bool[ChessGameBoard.Lines, ChessGameBoard.Columns];
            Position[] Indexes = new Position[]
            {
                new Position(Position.Row - 2, Position.Column - 1),
                new Position(Position.Row - 2, Position.Column + 1),
                new Position(Position.Row + 2, Position.Column - 1),
                new Position(Position.Row + 2, Position.Column + 1),
                new Position(Position.Row - 1, Position.Column + 2),
                new Position(Position.Row + 1, Position.Column + 2),
                new Position(Position.Row - 1, Position.Column - 2),
                new Position(Position.Row + 1, Position.Column - 2)
            };

            foreach (var currentIndex in Indexes)
                if (ChessGameBoard.IsValidPosition(currentIndex) && CanMove(currentIndex))
                    movements[currentIndex.Row, currentIndex.Column] = true;

            return movements;

        }

        public override string ToString ()
        {
            return "H";
        }
    }   
}
