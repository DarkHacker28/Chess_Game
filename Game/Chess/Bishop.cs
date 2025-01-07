using System;
using System.Collections.Generic;

using ChessGame.Board;
using ChessGame.Board.Exceptions;

namespace ChessGame.Chess
{
    sealed class Bishop : Piece
    {
        /// <summary>
        /// Constructor function for this class
        /// </summary>
        /// <param name="ChessPlayBoard"></param>
        /// <param name="color"></param>
        public Bishop (ChessGameBoard ChessPlayBoard, Color color) 
            : base(ChessPlayBoard, color)
        {
        }

        /// <summary>
        /// This functions has the Complete logic for the moves for Bishop
        /// </summary>
        /// <returns>true or false</returns>
        public override bool[,] AllPossibleMovements ()
        {
            List<Position> Indexes = new List<Position>();
            bool[,] movements = new bool[ChessGameBoard.Lines, ChessGameBoard.Columns];
            var LoopCountVariable = 1;

            for (var c = Position.Column; c < ChessGameBoard.Columns; c++ )
            {
                var position = new Position(Position.Row - LoopCountVariable, Position.Column + LoopCountVariable);
                CheckForTheMovevment(position, Indexes);

                if (ChessGameBoard.IsValidPosition(position) && ChessGameBoard.pieceExist(position))
                    break;

                LoopCountVariable++;
            }

            LoopCountVariable = 1;

            for (var c = Position.Column; c >= 0; c--)
            {
                var position = new Position(Position.Row - LoopCountVariable, Position.Column - LoopCountVariable);
                CheckForTheMovevment(position, Indexes);

                if (ChessGameBoard.IsValidPosition(position) && ChessGameBoard.pieceExist(position))
                    break;

                LoopCountVariable++;
            }

            LoopCountVariable = 1;

            for (var c = Position.Column; c >= 0; c--)
            {
                var position = new Position(Position.Row + LoopCountVariable, Position.Column - LoopCountVariable);
                CheckForTheMovevment(position, Indexes);

                if (ChessGameBoard.IsValidPosition(position) && ChessGameBoard.pieceExist(position))
                    break;

                LoopCountVariable++;
            }

            LoopCountVariable = 1;

            for (var c = Position.Column; c < ChessGameBoard.Columns; c++)
            {
                var position = new Position(Position.Row + LoopCountVariable, Position.Column + LoopCountVariable);
                CheckForTheMovevment(position, Indexes);

                if (ChessGameBoard.IsValidPosition(position) && ChessGameBoard.pieceExist(position))
                    break;

                LoopCountVariable++;
            }

            foreach(var currentMovement in Indexes)
                movements[currentMovement.Row, currentMovement.Column] = true;

            return movements;
        }

        /// <summary>
        /// This function will test the movement
        /// </summary>
        /// <param name="position"></param>
        /// <param name="Indexes"></param>
        private void CheckForTheMovevment (Position position, List<Position> Indexes)
        {
            if (ChessGameBoard.IsValidPosition(position) && CanMove(position))
                Indexes.Add(position);
        }

        public override string ToString ()
        {
            return "B";
        }
    }
}
