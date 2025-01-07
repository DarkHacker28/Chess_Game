using System;

namespace ChessGame.Board
{
    abstract class Piece
    {
        //declairing variables for this class
        public ChessGameBoard ChessGameBoard { get; private set; }
        public Position Position { get; protected set; }
        public Color Color { get; protected set; }
        public int Movements { get; private set; }

        /// <summary>
        /// Constructor function for this class
        /// </summary>
        /// <param name="ChessPlayBoard">chess board object</param>
        /// <param name="color">color object</param>
        public Piece (ChessGameBoard ChessPlayBoard, Color color)
        {
            ChessGameBoard = ChessPlayBoard;
            Color = color;

            Position = null;
            Movements = 0;
        }  
      
        /// <summary>
        /// This function will increment the movement
        /// </summary>
        public void incrementTheMovement ()
        {
            Movements++;
        }

        /// <summary>
        /// This function will decrement the movement
        /// </summary>
        public void decrementTheMovement ()
        {
            Movements--;
        }

        /// <summary>
        /// this function will alter the position
        /// </summary>
        /// <param name="newPosition"></param>
        public void AlterPosition (Position newPosition)
        {
            Position = newPosition;
        }

        /// <summary>
        /// This funcvtion will check if the movement is possible for the desired position
        /// </summary>
        /// <param name="position">desired position</param>
        /// <returns>true or false</returns>
        public bool IsPossibleMovement (Position position)
        {
            return AllPossibleMovements()[position.Row, position.Column];
        }

        /// <summary>
        /// This function will check for the move for the position
        /// </summary>
        /// <param name="position">the position</param>
        /// <returns>true or false</returns>
        protected virtual bool CanMove (Position position)
        {
            return (!ChessGameBoard.pieceExist(position) ||  ChessGameBoard.GetBoardPiece(position).Color != Color);
        }

        public abstract bool[,] AllPossibleMovements ();
    }
}
