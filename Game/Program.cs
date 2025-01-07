using System;
using ChessGame.Board;
using ChessGame.Board.Exceptions;
using ChessGame.Chess;
using ChessGame.Chess.Exceptions;

namespace ChessGame
{
    class Program
    {
        static void Main (string[] args)
        {
           
            
            
            //Consoling welcome Note,rules and taking Player names
            Console.Write("\t\t\t***********WelCome to the Chess Game***********\n");
            Console.Write("Rules are same as Normal Chess Game!\n");
            Console.Write("First Player will be White and Second will be DarkGray in color\n");
            Console.Write("VERTICAL NUMBERS AT LEFT OF CHESS BOARD ARE COLUMNS FOR INDEX\n");
            Console.Write("HORIZONTAL ALPHABETS AT THE BOTTOM OF CHESS BOARD ARE ROWS FOR INDEX\n");
            Console.Write("Message will be displayed when player takes a wrong move\n\n");
            //Taking Player One Name
            Console.Write("Enter Player One Name : ");
            Players.PlayerOneName = Console.ReadLine();

            //Taking Player Two Name
            Console.Write("Enter Player Two Name : ");
            Players.PlayerTwoName = Console.ReadLine();


            //Iniating count for dislaying the Player's turn with Name
            Players.Count = 1;


            //Initiating the Match
            Console.Write("\nLet's Start the Game!\n\n");
            ChessMatch MatchForChess = new ChessMatch();

            while (!MatchForChess.Finishes)
            {
                //Viewing chess board
                View.ConsoleChessGameBoard(MatchForChess.ChessGameBoard);

                //Viewing status of Match
                View.ConsoleMatchCurrentStatus(MatchForChess);

                try
                {
                    //Taking index of the piece to move
                    Console.Write("Which piece you want to move, enter index (ColumnRow) : ");
                    var FromPosition = View.ReadchessIndex();
                    
                    MatchForChess.CheckOriginPosition(FromPosition);
                    
                    // Print the chess board with the piece's possible movements
                    View.ConsoleChessGameBoard(MatchForChess.ChessGameBoard, FromPosition);
                    View.ConsoleMatchCurrentStatus(MatchForChess);
                    Console.WriteLine("Peice you want to move: {0}{1}", char.ToUpper(FromPosition.Column), FromPosition.Row);

                    //Taking index where player want to move the piece
                    Console.Write("Where you want to move the piece, enter index (ColumnRow): ");
                    var ToPosition = View.ReadchessIndex();

                    MatchForChess.ChessBoardTargetIndex(ToPosition);

                    MatchForChess.ExecuteTheTurn(FromPosition, ToPosition);
                }
                catch (ChessBoardException e)
                {
                    View.ConsoleExceptionOccured(e);
                }
                catch (ChessMatchException e)
                {
                    View.ConsoleExceptionOccured(e);
                }
                Console.Write("\n\t\t\t***********Next Player Turn***********\n\n");
            }
            

            //At the end of Match, Viewing the Winner name and Details
            View.ConsoleMatchEnd(MatchForChess);
        }
    }
}
