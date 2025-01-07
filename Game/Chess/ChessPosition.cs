using System;

using ChessGame.Board;

namespace ChessGame.Chess
{
    class ChessPosition
    {
        public char Column { get; private set; }
        public int Row { get; private set; }
        
        public ChessPosition (char column, int line)
        {
            Column = column;
            Row = line;
        }

        public Position ToPosition ()
        {
            return new Position(ChessGameBoard.Lines - Row, Column - 'a');
        }

        public override string ToString ()
        {
            return string.Concat(Column, Row);
        }
    }
}
