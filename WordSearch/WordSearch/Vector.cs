using System;
using System.Collections.Generic;
using System.Text;

namespace WordSearch
{
    class Vector
    {
        private int row;
        private int collum;

        public Vector()
        {

        }

        public Vector(int inputRow, int inputCollum)
        {
            this.row = inputRow;
            this.collum = inputCollum;
        }

        public int Row
        {
            get { return this.row; }
            set { this.row = value; }
        }

        public int Collum
        {
            get { return this.collum; }
            set { this.collum = value; }
        }
    }
}
