using System;
using System.Collections.Generic;
using System.Text;

namespace WordSearch
{
    class Board
    {
        private List<Letter>[,] boardArray; //an array of lists, each list containing one or many game objects

        public Board (GameFile gameFileToLoad)
        {
            boardArray = new List<Letter>[gameFileToLoad.BoardDimensions.Row, gameFileToLoad.BoardDimensions.Collum];
        }
    }

}
