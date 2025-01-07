using System;

using ChessGame.Board.Exceptions;

namespace ChessGame.Board
{
    class ChessGameBoard
    {
        //Board will be 8*8
        public const int Lines = 8;
        public const int Columns = 8;

        private Piece[,] Pieces;

        /// <summary>
        /// This function will initialize the ChessPlayBoard
        /// </summary>
        public ChessGameBoard ()
        {
            Pieces = new Piece[Lines, Columns];
        }

        /// <summary>
        /// This function will put the selected piece at the desired position
        /// </summary>
        /// <param name="piece">selected piece</param>
        /// <param name="position">position you want to move on</param>
        public void putThePiece (Piece piece, Position position)
        {
            ValidPosition(position);

            if (pieceExist(position))
                throw new ChessBoardException("Already exists a peace in this position!");

            Pieces[position.Row, position.Column] = piece;
            piece.AlterPosition(position);
        }

        /// <summary>
        /// This function will remove the piece from board
        /// </summary>
        /// <param name="position">position from where the pice will be removed</param>
        /// <returns>removed piece or null if piece don't eists</returns>
        public Piece removeThePiece (Position position)
        {
            if (pieceExist(position))
            {
                var piece = GetBoardPiece(position);
                Pieces[position.Row, position.Column] = null;
                piece.AlterPosition(null);

                return piece;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Checks if the piece exists on the desired position
        /// </summary>
        /// <param name="position">desired position</param>
        /// <returns>True or False</returns>
        public bool pieceExist (Position position)
        {
            return GetBoardPiece(position) != null;
        }

        /// <summary>
        /// This function will get the piece
        /// </summary>
        /// <param name="position">desired position</param>
        /// <returns>that piece</returns>
        public Piece GetBoardPiece (Position position)
        {
            ValidPosition(position);
            return Pieces[position.Row, position.Column];    
        }

        /// <summary>
        /// This function will checks for the enemy at that desired position
        /// </summary>
        /// <param name="position">desired position</param>
        /// <param name="color">color of piece</param>
        /// <returns>True or False</returns>
        public bool HasEnemy (Position position, Color color)
        {
            var piece = GetBoardPiece(position);
            return piece != null && piece.Color.Equals(color);
        }

        /// <summary>
        /// This function will check if the position is Valid
        /// </summary>
        /// <param name="position">desired postion</param>
        /// <returns>true or false</returns>
        public bool IsValidPosition (Position position)
        {
            if (position.Row < 0 || position.Row >= Lines || position.Column < 0 || position.Column >= Columns)
                return false;
            return true;
        }
        /// <summary>
        /// This function will check if the p[osition is Invalid then throws exception
        /// </summary>
        /// <param name="position">desired position</param>
        public void ValidPosition (Position position)
        {
            if (!IsValidPosition(position))
                throw new ChessBoardException("Not valid position!");
        }
    }
}
