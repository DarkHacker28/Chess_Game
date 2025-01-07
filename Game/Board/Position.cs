using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessGame.Board
{
    class Position
    {
        //decaliring variables for this class
        public int Row { get; private set; }
        public int Column { get; private set; }
    
        /// <summary>
        /// Constructor function for this class
        /// </summary>
        /// <param name="line"></param>
        /// <param name="column"></param>
        public Position (int line, int column)
        {
            Row = line;
            Column = column;
        }


        public override string ToString ()
        {
            return Row + ", " + Column;
        }
    }
}
