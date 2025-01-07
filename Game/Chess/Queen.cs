using System.Collections.Generic;

using ChessGame.Board;
using ChessGame.Board.Exceptions;

namespace ChessGame.Chess
{
    sealed class Queen : Piece
    {
        /// <summary>
        /// Constructor function for this class
        /// </summary>
        /// <param name="ChessPlayBoard"></param>
        /// <param name="color"></param>
        public Queen (ChessGameBoard ChessPlayBoard, Color color) 
            : base(ChessPlayBoard, color)
        {
        }

        /// <summary>
        /// This function has all the logic for Queen Piece
        /// </summary>
        /// <returns></returns>
        public override bool[,] AllPossibleMovements ()
        {
            List<Position> Indexes = new List<Position>();
            bool[,] movements = new bool[ChessGameBoard.Columns, ChessGameBoard.Lines];
            var LoopCountVariable = 1;

            // Left movement
            for (int c = Position.Column - 1; c >= 0; c--)
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

            for (var c = Position.Column; c < ChessGameBoard.Columns; c++)
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

            foreach (var currentMovement in Indexes)
                movements[currentMovement.Row, currentMovement.Column] = true;

            return movements;
        }

        private void CheckForTheMovevment (Position position, List<Position> Indexes)
        {
            if (ChessGameBoard.IsValidPosition(position) && CanMove(position))
                Indexes.Add(position);
        }

        public override string ToString ()
        {
            return "Q";
        }
    }
}
