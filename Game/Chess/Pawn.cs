using System;
using System.Collections.Generic;

using ChessGame.Board;
using ChessGame.Board.Exceptions;

namespace ChessGame.Chess
{
    sealed class Pawn : Piece   
    {
        /// <summary>
        /// Constructor function for this class
        /// </summary>
        /// <param name="ChessPlayBoard"></param>
        /// <param name="color"></param>
        public Pawn (ChessGameBoard ChessPlayBoard, Color color)
            : base(ChessPlayBoard, color)
        {
        }

        /// <summary>
        /// This function has all the logic for the possible movements by Pawn piece
        /// </summary>
        /// <returns></returns>
        public override bool[,] AllPossibleMovements ()
        {
            bool[,] movements = new bool[ChessGameBoard.Lines, ChessGameBoard.Columns];
            Position[] Indexes = new Position[4];

            if(Color.Equals(Color.White))
            {
                Indexes[0] = (new Position(Position.Row - 1, Position.Column));
                Indexes[1] = Movements.Equals(0) ? (new Position(Position.Row - 2, Position.Column)) : null;
                Indexes[2] = (new Position(Position.Row - 1, Position.Column + 1));
                Indexes[3] = (new Position(Position.Row - 1, Position.Column - 1));
            }
            else
            {
                Indexes[0] = (new Position(Position.Row + 1, Position.Column));
                Indexes[1] = Movements.Equals(0) ? (new Position(Position.Row + 2, Position.Column)) : null;
                Indexes[2] = (new Position(Position.Row + 1, Position.Column + 1));
                Indexes[3] = (new Position(Position.Row + 1, Position.Column - 1));
            }

            for (var c = 0; c < Indexes.Length; c++ )
            {
                var currentIndex = Indexes[c];

                if (currentIndex != null && ChessGameBoard.IsValidPosition(currentIndex))
                {
                    if (c < 2)
                    {
                        if (CanMove(currentIndex))
                            movements[currentIndex.Row, currentIndex.Column] = true;
                    }
                    else
                    {
                        if (ChessGameBoard.HasEnemy(currentIndex, ChessMatch.Adversary(Color)))
                            movements[currentIndex.Row, currentIndex.Column] = true;
                    }
                }   
            }

            return movements;
        }

        protected override bool CanMove (Position position)
        {
            return !ChessGameBoard.pieceExist(position);
        }

        public override string ToString ()
        {
            return "P";
        }
    }
}
