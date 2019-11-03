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
            AddWordsToBoardArray(gameFileToLoad);
            FillEmptyArrayCells();
            DisplayBoard();
        }

        public List<Letter>[,] BoardArray
        {
            get { return this.boardArray; }
            set { this.boardArray = value; }
        }

        public void UpdateBoardArray(int startRowIndex, int startColIndex, string word, bool found, bool clear)
        {
            string direction = "";
            ConsoleColor textColour;
            if (found)
            {
                textColour = ConsoleColor.Green;
            }
            else
            {
                if (clear) textColour = ConsoleColor.White;
                else textColour = ConsoleColor.Red;
            }


            List<Letter> startCellObjects = boardArray[startRowIndex, startColIndex];
            foreach (Letter letter in startCellObjects)
            {
                if (letter.Word == word)
                {
                    direction = letter.Direction;
                }
            }
            RowColChange(direction, out int rowChange, out int colChange);
            int rowIndex = startRowIndex;
            int colIndex = startColIndex;
            colIndex = startColIndex;
            for (int hops = 0; hops < word.Length; hops++)
            {
                List<Letter> cellContents = boardArray[rowIndex, colIndex];
                UpdateAllCellObjectsColour(cellContents, word, textColour);
                rowIndex = rowIndex + rowChange;
                colIndex = colIndex + colChange;
            }
        }

        private void RowColChange(string direction, out int rowChange, out int colChange)
        {
            int rChange;
            int cChange;
            if (direction == "left")
            {
                rChange = -1;
                cChange = 0;
            }
            else if (direction == "right")
            {
                rChange = 1;
                cChange = 0;
            }
            else if (direction == "up")
            {
                rChange = 0;
                cChange = -1;
            }
            else if (direction == "down")
            {
                rChange = 0;
                cChange = 1;
            }
            else if (direction == "leftup")
            {
                rChange = -1;
                cChange = -1;
            }
            else if (direction == "rightup")
            {
                rChange = 1;
                cChange = -1;
            }
            else if (direction == "leftdown")
            {
                rChange = -1;
                cChange = 1;
            }
            else
            {//direction is right down
                rChange = 1;
                cChange = 1;
            }
            rowChange = rChange;
            colChange = cChange;
        }

        private void UpdateAllCellObjectsColour(List<Letter> cellContents, string word, ConsoleColor textColour)
        {
            foreach (Letter letter in cellContents)
            {
                if (letter.Word == word)
                {
                    letter.Color = textColour;
                }
            }
        }

        public void DisplayBoard()
        {
            Console.Clear();
            for (int collumIndex = 0; collumIndex < boardArray.GetLength(1); collumIndex++)
            {   //loop through collums first this time since we want to display row by row in console
                for (int rowIndex = 0; rowIndex < boardArray.GetLength(0); rowIndex++)
                {
                    List<Letter> cellContents = new List<Letter>();
                    cellContents = boardArray[rowIndex, collumIndex];
                    ConsoleColor charColour = ConsoleColor.White;
                    foreach (Letter letter in cellContents)
                    {   //there should only ever be a possibility of white or 1 other colour in each cell. 
                        if (charColour != ConsoleColor.Green)
                        {   //prevent the colour changing from green if green is set
                            charColour = letter.Color;
                        }
                    }
                    Console.ForegroundColor = charColour;
                    string cellstring = cellContents[0].Character;
                    Console.Write(cellstring.PadRight(4));
                }
                Console.WriteLine();
                Console.WriteLine(" ");
            }
        }

        private void FillEmptyArrayCells()
        {
            Random random = new Random();
            for (int rowIndex = 0; rowIndex < boardArray.GetLength(0); rowIndex++)
            {
                for (int colIndex = 0; colIndex < boardArray.GetLength(1); colIndex++)
                {
                    if (boardArray[rowIndex,colIndex] == null)
                    {
                        Vector position = new Vector(rowIndex, colIndex);
                        if (rowIndex == 0 && colIndex == 0)
                        {
                            Letter emptySpace = new Letter();
                            emptySpace.Character = "";
                            emptySpace.Positon = position;
                            emptySpace.Word = "";
                            emptySpace.WordEnd = false;
                            emptySpace.WordStart = false;
                            List<Letter> emptyCellList = new List<Letter>();
                            emptyCellList.Add(emptySpace);
                            boardArray[rowIndex, colIndex] = emptyCellList;
                        }
                        else if (rowIndex == 0)
                        {
                            Letter rowHeadings = new Letter();
                            rowHeadings.Character = Convert.ToString(colIndex - 1);
                            rowHeadings.Word = "";
                            rowHeadings.Positon = position;
                            rowHeadings.Word = "";
                            rowHeadings.WordEnd = false;
                            rowHeadings.WordStart = false;
                            rowHeadings.Color = ConsoleColor.Yellow;
                            List<Letter> rowHeadCellList = new List<Letter>();
                            rowHeadCellList.Add(rowHeadings);
                            boardArray[rowIndex, colIndex] = rowHeadCellList;
                        }
                        else if (colIndex == 0)
                        {
                            Letter colHeadings = new Letter();
                            colHeadings.Character = Convert.ToString(rowIndex - 1);
                            colHeadings.Word = "";
                            colHeadings.Positon = position;
                            colHeadings.WordEnd = false;
                            colHeadings.WordStart = false;
                            colHeadings.Color = ConsoleColor.Yellow;
                            List<Letter> colHeadCellList = new List<Letter>();
                            colHeadCellList.Add(colHeadings);
                            boardArray[rowIndex, colIndex] = colHeadCellList;
                        }
                        else
                        {
                            string randomChar = Convert.ToString((char)random.Next('a', 'z'));
                            Letter randomLetter = new Letter();
                            randomLetter.Character = randomChar;
                            randomLetter.Direction = "";
                            randomLetter.Positon = position;
                            randomLetter.Word = "";
                            randomLetter.WordEnd = false;
                            randomLetter.WordStart = false;
                            List<Letter> mainAreaCellList = new List<Letter>();
                            mainAreaCellList.Add(randomLetter);
                            boardArray[rowIndex, colIndex] = mainAreaCellList;
                        }
                    }
                }
            }
        }

        private void AddWordsToBoardArray(GameFile gameFile)
        {
            List<Letter> cellLetters;
            List<Word> words = gameFile.Words;
            foreach (Word word in words)
            {
                WordSearch.AddWordToGameWordsList(word);
                List<Letter> wordLetters = new List<Letter>();
                wordLetters = word.Letters;
                foreach (Letter letter in wordLetters)
                {
                    int cellRow = letter.Positon.Row;
                    int cellCol = letter.Positon.Collum;
                    if (boardArray[cellRow,cellCol] == null)
                    {
                        cellLetters = new List<Letter>();
                        cellLetters.Add(letter);
                        boardArray[cellRow, cellCol] = cellLetters;
                    }
                    else
                    {
                        boardArray[cellRow, cellCol].Add(letter);
                    }
                }
            }
        }
    }

}
