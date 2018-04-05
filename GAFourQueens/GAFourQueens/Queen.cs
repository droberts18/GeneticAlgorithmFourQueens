using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAFourQueens
{
    class Queen
    {
        private static readonly Random rdm = new Random();
        private int row;
        private int col;

        public Queen()
        {
            giveRandomPosition();
        }

        public void giveRandomPosition()
        {
            row = rdm.Next(1, 5);
            col = rdm.Next(1, 5);
        }

        public int getRow()
        {
            return row;
        }

        public int getCol()
        {
            return col;
        }
    }
}
