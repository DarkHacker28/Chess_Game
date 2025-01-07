using System;

using ChessGame.Board;
using ChessGame.Board.Exceptions;

namespace ChessGame.Chess
{
    sealed class King : Piece
    {
        /// <summary>
        /// constructor function for this class
        /// </summary>
        /// <param name="ChessPlayBoard"></param>
        /// <param name="color"></param>
        public King (ChessGameBoard ChessPlayBoard, Color color) : base(ChessPlayBoard, color)
        {
        }

        /// <summary>
        /// This function has all the logic for possible movements for Kings
        /// </summary>
        /// <returns></returns>
        public override bool[,] AllPossibleMovements ()
        {
            bool[,] movements = new bool[ChessGameBoard.Columns, ChessGameBoard.Lines];
            Position[] testMovements = new Position[]
            {
                new Position(Position.Row + 1, Position.Column),
                new Position(Position.Row + 1, Position.Column + 1),
                new Position(Position.Row, Position.Column + 1),
                new Position(Position.Row - 1, Position.Column + 1),
                new Position(Position.Row - 1, Position.Column),
                new Position(Position.Row - 1, Position.Column - 1),
                new Position(Position.Row, Position.Column - 1),
                new Position(Position.Row + 1, Position.Column - 1)
            };
            
            foreach (var currentMovement in testMovements)
                if (ChessGameBoard.IsValidPosition(currentMovement) && CanMove(currentMovement))
                    movements[currentMovement.Row, currentMovement.Column] = true;

            return movements;
        }

        public override string ToString ()
        {
            return "K";
        }
    }
}
