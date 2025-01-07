using System;
using System.Collections.Generic;

using ChessGame.Board;
using ChessGame.Board.Exceptions;
using ChessGame.Chess.Exceptions;

namespace ChessGame.Chess
{
    class ChessMatch
    {
        // Declairing the variables for this class
        public ChessGameBoard ChessGameBoard { get; private set; }
        public int GameTurn { get; private set; }
        public Color currentPlayer { get; private set; }
        public bool Finishes { get; private set; }
        public List<Piece> InGamePieces { get; private set; }
        public List<Piece> OutOfGamePieces { get; private set; }
        public DateTime GameStartAt { get; private set; }
        public DateTime GameFinishAt { get; private set; }
        public bool Check { get; private set; }
        public string PlayerName { get; set; }
        
        /// <summary>
        /// Constructor function for this class which initalizes the varaibles
        /// </summary>
        public ChessMatch ()
        {
            
            ChessGameBoard = new ChessGameBoard();
            GameTurn = 1;
            currentPlayer = Color.White;
            PlayerName = Players.PlayerOneName;
            Finishes = false;
            GameStartAt = DateTime.Now;
            InGamePieces = new List<Piece>();
            OutOfGamePieces = new List<Piece>();

            PlacePieces();
        }

        /// <summary>
        /// This function will execute the movement of the piece from origin position to target position
        /// </summary>
        /// <param name="origin">where the piece was</param>
        /// <param name="target">where to move the piece</param>
        public void ExecuteTheTurn (ChessPosition origin, ChessPosition target)
        {
            var originPiece = ChessGameBoard.GetBoardPiece(origin.ToPosition());

            if(originPiece.IsPossibleMovement(target.ToPosition()))
            {
                removeThePiece(origin);
                var currentTargetPiece = ChessGameBoard.GetBoardPiece(target.ToPosition());
                
                if (currentTargetPiece != null)
                    removeThePiece(target, currentTargetPiece);

                insertThePiece(originPiece, target);

                // Verify if the player CHECKED himself or if the player already is on CHECK
                if (IsInCheck(GetKing(currentPlayer)))
                {
                    UndoMovement(origin, target, currentTargetPiece);

                    if(Check)
                        throw new ChessMatchException("You are on CHECK!");
                    else
                        throw new ChessMatchException("You can't CHECK yourself");
                }

                // Verify if the player CHECKED the adversary
                Check = IsInCheck(GetKing(Adversary(currentPlayer)));

                // Verify if the player CHECKMATE the adversary
                if(Check)
                {
                    if (IsInCheckMate(GetKing(Adversary(currentPlayer))))
                    {
                        Finishes = true;
                        GameFinishAt = DateTime.Now;
                    }
                }

                GameNextTurn(originPiece);
            }
            else
            {
                throw new ChessMatchException("This piece can't make this movement");
            }
        }

        /// <summary>
        /// This function will undo the last move from the Chess board
        /// </summary>
        /// <param name="origin">where the piece was</param>
        /// <param name="target">where to move the piece</param>
        /// <param name="removedPiece">that piece</param>
        private void UndoMovement (ChessPosition origin, ChessPosition target, Piece removedPiece)
        {
            var oldOriginPiece = ChessGameBoard.GetBoardPiece(target.ToPosition());

            removeThePiece(target);
            insertThePiece(oldOriginPiece, origin);
            oldOriginPiece.decrementTheMovement();

            if (removedPiece != null)
                insertTheNewPiece(removedPiece, target);
        }

        /// <summary>
        /// This function will check for the position from where you want to move the piece
        /// </summary>
        /// <param name="origin">the position</param>
        public void CheckOriginPosition (ChessPosition origin)
        {
            if (ChessGameBoard.pieceExist(origin.ToPosition()))
            {
                if (!ChessGameBoard.GetBoardPiece(origin.ToPosition()).Color.Equals(currentPlayer))
                    throw new ChessMatchException("You can only movement your pieces");
            }
            else
            {
                throw new ChessBoardException("Not exist a piece in this position");
            }
        }
        /// <summary>
        /// This function will check for the position where you want to move the piece
        /// </summary>
        /// <param name="target">the position</param>
        public void ChessBoardTargetIndex (ChessPosition target)
        {
            ChessGameBoard.ValidPosition(target.ToPosition());
        }

        public List<Piece> GetInGamePieces (Color color)
        {
            return InGamePieces.FindAll(p => p.Color.Equals(color));
        }

        public List<Piece> GetOutOfGamePieces (Color color)
        {
            return OutOfGamePieces.FindAll(p => p.Color.Equals(color));
        }

        /// <summary>
        /// This function will place all the pieces in the chess board
        /// </summary>
        private void PlacePieces ()
        {
            // White's pieces
            insertTheNewPiece(new Tower(ChessGameBoard, Color.White), new ChessPosition('a', 1));
            insertTheNewPiece(new Tower(ChessGameBoard, Color.White), new ChessPosition('h', 1));
            insertTheNewPiece(new King(ChessGameBoard, Color.White), new ChessPosition('d', 1));
            insertTheNewPiece(new Queen(ChessGameBoard, Color.White), new ChessPosition('e', 1));
            insertTheNewPiece(new Bishop(ChessGameBoard, Color.White), new ChessPosition('f', 1));
            insertTheNewPiece(new Bishop(ChessGameBoard, Color.White), new ChessPosition('c', 1));
            insertTheNewPiece(new Horse(ChessGameBoard, Color.White), new ChessPosition('b', 1));
            insertTheNewPiece(new Horse(ChessGameBoard, Color.White), new ChessPosition('g', 1));

            // White pawns
            for (var c = 0; c < ChessGameBoard.Columns; c++)
            {
                char currentColumn = (char)('a' + c);
                insertTheNewPiece(new Pawn(ChessGameBoard, Color.White), new ChessPosition(currentColumn, 2));
            }

            // Black's pieces
            insertTheNewPiece(new Tower(ChessGameBoard, Color.DarkGray), new ChessPosition('a', 8));
            insertTheNewPiece(new Tower(ChessGameBoard, Color.DarkGray), new ChessPosition('h', 8));
            insertTheNewPiece(new King(ChessGameBoard, Color.DarkGray), new ChessPosition('d', 8));
            insertTheNewPiece(new Bishop(ChessGameBoard, Color.DarkGray), new ChessPosition('c', 8));
            insertTheNewPiece(new Queen(ChessGameBoard, Color.DarkGray), new ChessPosition('e', 8));
            insertTheNewPiece(new Bishop(ChessGameBoard, Color.DarkGray), new ChessPosition('f', 8));
            insertTheNewPiece(new Horse(ChessGameBoard, Color.DarkGray), new ChessPosition('b', 8));
            insertTheNewPiece(new Horse(ChessGameBoard, Color.DarkGray), new ChessPosition('g', 8));

            // Black pawns
            for (var c = 0; c < ChessGameBoard.Columns; c++)
            {
                char currentColumn = (char)('a' + c);
                insertTheNewPiece(new Pawn(ChessGameBoard, Color.DarkGray), new ChessPosition(currentColumn, 7));
            }
        }

        /// <summary>
        /// This function will insert the piece
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="chessPosition"></param>
        private void insertThePiece (Piece piece, ChessPosition chessPosition)
        {
            ChessGameBoard.putThePiece(piece, chessPosition.ToPosition());
        }

        /// <summary>
        /// This function will insert the new piece
        /// </summary>
        /// <param name="piece"></param>
        /// <param name="chessPosition"></param>
        private void insertTheNewPiece (Piece piece, ChessPosition chessPosition)
        {
            ChessGameBoard.putThePiece(piece, chessPosition.ToPosition());
            
            OutOfGamePieces.Remove(piece);
            InGamePieces.Add(piece);
        }

        /// <summary>
        /// This function will remove the piece
        /// </summary>
        /// <param name="chessPosition"></param>
        private void removeThePiece (ChessPosition chessPosition)
        {
            ChessGameBoard.removeThePiece(chessPosition.ToPosition());
        }

        /// <summary>
        /// This will be called inside previous function to actually remove the piece
        /// </summary>
        /// <param name="chessPosition"></param>
        /// <param name="piece"></param>
        private void removeThePiece (ChessPosition chessPosition, Piece piece)
        {
            removeThePiece(chessPosition);

            InGamePieces.Remove(piece);
            OutOfGamePieces.Add(piece);
        }
        
        /// <summary>
        /// This function will execute to next Player's turn
        /// </summary>
        /// <param name="originPiece"></param>
        private void GameNextTurn (Piece originPiece)
        {
            originPiece.incrementTheMovement();
            
            if(!Finishes)
            {
                currentPlayer = (currentPlayer.Equals(Color.White)) ? Color.DarkGray : Color.White;
                if(Players.Count%2 ==0)
                {
                    PlayerName = Players.PlayerOneName;
                }
                else
                {
                    PlayerName = Players.PlayerTwoName;
                }
                Players.Count++;
                GameTurn++;
            }
        }
        
        /// <summary>
        /// This function will check for In check
        /// </summary>
        /// <param name="king"></param>
        /// <returns></returns>
        private bool IsInCheck (King king)
        {
            var adversaryPieces = GetInGamePieces(Adversary(king.Color));

            foreach(var currentPiece in adversaryPieces)
                if (currentPiece.IsPossibleMovement(king.Position))
                    return true;

            return false;
        }

        /// <summary>
        /// This function will cherck for Check mate
        /// </summary>
        /// <param name="king"></param>
        /// <returns></returns>
        private bool IsInCheckMate (King king)
        {
            bool[,] kingPossibleMovements = king.AllPossibleMovements();
            var kingPossibleMovementsCount = 0;

            foreach(var currentPiece in GetInGamePieces(Adversary(king.Color)))
            {
                bool[,] adversaryPossibleMovements = currentPiece.AllPossibleMovements();

                for(var l = 0; l < ChessGameBoard.Lines; l++)
                    for(var c = 0; c < ChessGameBoard.Columns; c++)
                        if (kingPossibleMovements[l, c] && adversaryPossibleMovements[l, c])
                            kingPossibleMovements[l, c] = false;
            }

            for (var l = 0; l < ChessGameBoard.Lines; l++)
                for (var c = 0; c < ChessGameBoard.Columns; c++)
                    if (kingPossibleMovements[l, c])
                        kingPossibleMovementsCount++;

            return (kingPossibleMovementsCount.Equals(0));
        }

        private King GetKing (Color color)
        {
            return (King) GetInGamePieces(color).Find(p => p is King);
        }

        public static Color Adversary (Color color)
        {
            return (color.Equals(Color.White)) ? Color.DarkGray : Color.White;
        }
    }
}
