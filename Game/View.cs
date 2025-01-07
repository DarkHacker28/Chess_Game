using System;

using ChessGame.Board;
using ChessGame.Board.Exceptions;
using ChessGame.Chess;

namespace ChessGame
{
    static class View
    {
        
        private static readonly ConsoleColor _byDefaultConsoleForegroundClr = ConsoleColor.Gray;
        private static readonly ConsoleColor _byDefaultConsoleBackgroundClr = ConsoleColor.Black;

        /// <summary>
        /// This function will console the chess board as initial, without the movement of piece
        /// </summary>
        /// <param name="ChessPlayBoard">It takes the chessboard to console</param>
        public static void ConsoleChessGameBoard (ChessGameBoard ChessPlayBoard)
        {
            for (int l = 0; l < ChessGameBoard.Lines; l++)
            {
                ConsoleBoardColumnLabel(l);

                for (int c = 0; c < ChessGameBoard.Columns; c++)
                    ConsoleBoardPiece(ChessPlayBoard.GetBoardPiece(new Position(l, c)), false);

                Console.WriteLine();
            }

            ConsoleBoardRowLabel();

            Console.Write("\n\n");
        }

        /// <summary>
        /// This function will console the chess board with the movement by player
        /// </summary>
        /// <param name="ChessPlayBoard">The chess board</param>
        /// <param name="origin">Index or origin with the movement</param>
        public static void ConsoleChessGameBoard (ChessGameBoard ChessPlayBoard, ChessPosition origin)
        {
            Console.Write("\nAfter Selection\n\n");
            for (int l = 0; l < ChessGameBoard.Lines; l++)
            {
                ConsoleBoardColumnLabel(l);

                for (int c = 0; c < ChessGameBoard.Columns; c++)
                {
                    var currentIndex = new Position(l, c);
                    bool[,] possibleMovements = ChessPlayBoard.GetBoardPiece(origin.ToPosition()).AllPossibleMovements();

                    if (possibleMovements[l, c])
                        ConsoleBoardPiece(ChessPlayBoard.GetBoardPiece(currentIndex), true);
                    else
                        ConsoleBoardPiece(ChessPlayBoard.GetBoardPiece(currentIndex));
                }
                Console.WriteLine();
            }

            ConsoleBoardRowLabel();

            Console.Write("\n\n");
        }

        /// <summary>
        /// This function will console the Match status
        /// </summary>
        /// <param name="chessMatch">chess Match object</param>
        public static void ConsoleMatchCurrentStatus (ChessMatch chessMatch)
        {
            Console.WriteLine("GameTurn: {0}", chessMatch.GameTurn);
            Console.WriteLine("Current Player's Color : {0} piece's", chessMatch.currentPlayer);
            Console.WriteLine("Current Player's Name : {0} ", chessMatch.PlayerName);

            ConsoleOutGamePiecesByClr(chessMatch, Color.White);
            ConsoleOutGamePiecesByClr(chessMatch, Color.DarkGray);

            if (chessMatch.Check)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("CHECK!!!");
                Console.ForegroundColor = _byDefaultConsoleForegroundClr;
            }

            Console.Write("\n\n");
        }

        /// <summary>
        /// this function will console at the end of Match showing the results and winner name
        /// </summary>
        /// <param name="chessMatch">chess Match object</param>
        public static void ConsoleMatchEnd (ChessMatch chessMatch)
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("CHECKMATE!!!\n");
            Console.ForegroundColor = _byDefaultConsoleForegroundClr;

            Console.WriteLine("Winner: {0} piece's", chessMatch.currentPlayer);
            Console.WriteLine("Winner Player Name {0}", chessMatch.currentPlayer);
            Console.WriteLine("GameTurn: {0}", chessMatch.GameTurn);
            Console.WriteLine("Started at: {0}", chessMatch.GameStartAt);
            Console.WriteLine("Finishes at: {0}\n", chessMatch.GameFinishAt);
            Console.WriteLine("Thanks For Playing!");
            ConsoleBoardWhenMatchEnd(chessMatch.ChessGameBoard);

            Console.ReadLine();
        }

        /// <summary>
        /// This function will read the position of piece you want to move, also check for exception
        /// </summary>
        /// <returns></returns>
        public static ChessPosition ReadchessIndex ()
        {
            var s = Console.ReadLine();

            if(s.Length.Equals(2))
            {
                if (char.IsLetter(s[0]) && char.IsNumber(s[1]))
                    return new ChessPosition(char.ToLower(s[0]), int.Parse(s[1].ToString()));
                else
                    throw new ChessBoardException("This is Invalid position!");
            }
            else
            {
                throw new ChessBoardException("This is Invalid position!");
            }
        }

        /// <summary>
        /// This function will be called when exception occurs, it consoles the error
        /// </summary>
        /// <param name="e"></param>
        public static void ConsoleExceptionOccured (Exception e)
        {
            Console.Clear();

            Console.ForegroundColor = (ConsoleColor)Color.Red;
            
            Console.WriteLine("[Error]: {0}", e.Message);
            Console.WriteLine("\nPress Enter to Continue");
            Console.ReadLine();

            Console.ForegroundColor = _byDefaultConsoleForegroundClr;
            Console.Clear();
        }

        /// <summary>
        /// This function will basically console the inner portion of chess board i.e Pieces and blank spaces
        /// </summary>
        /// <param name="piece"></param>
        private static void ConsoleBoardPiece (Piece piece)
        {
            if (piece != null)
            {
                Console.ForegroundColor = (ConsoleColor)piece.Color;
                Console.Write(piece + " ");
                Console.ForegroundColor = _byDefaultConsoleForegroundClr;
            }
            else
            {
                Console.Write("- ");
            }
        }

        /// <summary>
        /// This will does the same as above function but will show the possible movements of the selected peice
        /// </summary>
        /// <param name="piece">the piece which is selected</param>
        /// <param name="AllPossibleMovements">possible movements for that piece</param>
        private static void ConsoleBoardPiece (Piece piece, bool AllPossibleMovements)
        {
            if (AllPossibleMovements)
                Console.BackgroundColor = (ConsoleColor)Color.DarkYellow;

            if (piece != null)
            {
                Console.ForegroundColor = (ConsoleColor)piece.Color;
                Console.Write(piece + " ");
                Console.ForegroundColor = _byDefaultConsoleForegroundClr;
            }
            else
            {
                Console.Write("- ");
            }

            Console.BackgroundColor = _byDefaultConsoleBackgroundClr;
        }


        /// <summary>
        /// Console the Vertical labels i.e 1,2,3... the index for columns
        /// </summary>
        /// <param name="line"></param>
        private static void ConsoleBoardColumnLabel (int line)
        {
            Console.Write(ChessGameBoard.Lines - line + " ");
        }

        /// <summary>
        /// console the horizontal labels i.e A,B,C... the index for rows
        /// </summary>
        private static void ConsoleBoardRowLabel ()
        {
            string label = "  ";

            for (int c = 0; c < ChessGameBoard.Columns; c++)
                label += (char)('a' + c) + " ";

            Console.Write(label.ToUpper());
        }
        
        /// <summary>
        /// This function will console the killed pieces by the opponnet player 
        /// </summary>
        /// <param name="chessMatch"></param>
        /// <param name="color"></param>
        private static void ConsoleOutGamePiecesByClr (ChessMatch chessMatch, Color color)
        {
            Console.Write("Pieces Killed: ");
            Console.ForegroundColor = (ConsoleColor)color;
            string value = "[";

            foreach (var piece in chessMatch.GetOutOfGamePieces(color))
                value += (piece + " ");

            value += "]";

            Console.WriteLine(value);
            Console.ForegroundColor = _byDefaultConsoleForegroundClr;
        }

        /// <summary>
        /// Console the chess board at the end of Match
        /// </summary>
        /// <param name="ChessPlayBoard"></param>
        private static void ConsoleBoardWhenMatchEnd (ChessGameBoard ChessPlayBoard)
        {
            for (int l = 0; l < ChessGameBoard.Lines; l++)
            {
                ConsoleBoardColumnLabel(l);

                for (int c = 0; c < ChessGameBoard.Columns; c++)
                    ConsoleBoardPiece(ChessPlayBoard.GetBoardPiece(new Position(l, c)), false);

                Console.WriteLine();
            }

            ConsoleBoardRowLabel();

            Console.Write("\n\n");
        }
    }
}
