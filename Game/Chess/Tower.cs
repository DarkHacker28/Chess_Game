using System;
using System.Collections.Generic;

using ChessGame.Board;
using ChessGame.Board.Exceptions;

namespace ChessGame.Chess
{
    sealed class Tower : Piece
    {
        /// <summary>
        /// Constructor function for this class
        /// </summary>
        /// <param name="ChessPlayBoard"></param>
        /// <param name="color"></param>
        public Tower (ChessGameBoard ChessPlayBoard, Color color)
            : base(ChessPlayBoard, color)
        {
        }

        /// <summary>
        /// This function has all the logic possible for Tower piece
        /// </summary>
        /// <returns></returns>
        public override bool[,] AllPossibleMovements ()
        {
            List<Position> Indexes = new List<Position>();
            bool[,] movements = new bool[ChessGameBoard.Columns, ChessGameBoard.Lines];

            // Left movement
            for (int c = Position.Column - 1; c >= 0;  c--)
            {
                var position = new Position(Position.Row, c);
                CheckForTheMovevment(position, Indexes);

                if (ChessGameBoard.pieceExist(position))
                    break;
            }
            // Right movement
            for (int c = Position.Column + 1; c < 8; c++)
            {
                var position = new Position(Position.Row, c);
                CheckForTheMovevment(position, Indexes);

                if (ChessGameBoard.pieceExist(position))
                    break;
            }
            // Up movement
            for (int l = Position.Row - 1; l >= 0; l--)
            {
                var position = new Position(l, Position.Column);
                CheckForTheMovevment(position, Indexes);

                if (ChessGameBoard.pieceExist(position))
                    break;
            }
            // Down movement
            for (int l = Position.Row + 1; l < 8; l++)
            {
                var position = new Position(l, Position.Column);
                CheckForTheMovevment(position, Indexes);

                if (ChessGameBoard.pieceExist(position))
                    break;
            }

            foreach (var currentIndex in Indexes)
                movements[currentIndex.Row, currentIndex.Column] = true;

            return movements;
        }

        private void CheckForTheMovevment (Position position, List<Position> Indexes)
        {
            if (CanMove(position))
                Indexes.Add(position);
        }

        public override string ToString ()
        {
            return "T";
        }
    }
}
