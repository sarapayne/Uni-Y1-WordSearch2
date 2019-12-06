using System;
using System.Collections.Generic;
using System.Text;

namespace WordSearch
{
    class Board
    {
        private List<Letter>[,] boardArray; //an array of lists, each list containing one or many game objects

        /// <summary>
        /// Takes the supplied GameFile object, then uses the parameters to generate a new boardArray
        /// Adds the letter objects for each word object inside the gameFile to the lists which are inside each cell of the boardArray. 
        /// Fills the lists inside each of the cell arrays with a randomly generated letter object. 
        /// Finally it displays the contents of the new board. 
        /// </summary>
        /// <param name="gameFileToLoad">gameFile object passed into the method. </param>
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

        /// <summary>
        /// Loops through each cell of the boardArray contained within the parameters start and end index. 
        /// Then conditionally it will update the console colour property of the letter objects found.
        /// Objects shown in green (already found) will not be changed. 
        /// letter objects will all be contained in a list (each cell has a list of letter objects).
        /// </summary>
        /// <param name="startIndex">Row/Collum vector Position of the first letter</param>
        /// <param name="endIndex">Row/Collum vector Position of the last letter</param>
        /// <param name="color">console colour parameter to apply to the letter objects.</param>
        public void UpdateBoardArrayCellRange(Vector startIndex, Vector endIndex, ConsoleColor color)
        {
            int rowChange;
            int colChange;
            int numOfHops;
            if (endIndex.Row > startIndex.Row) rowChange = 1;
            else if (endIndex.Row < startIndex.Row) rowChange = -1;
            else rowChange = 0;
            if (endIndex.Collum > startIndex.Collum) colChange = 1;
            else if (endIndex.Collum < startIndex.Collum) colChange = -1;
            else colChange = 0;
            if (rowChange != 0)
            {
                numOfHops = endIndex.Row - startIndex.Row;
            }
            else
            {
                numOfHops = endIndex.Collum - startIndex.Collum;
            }
            if (numOfHops < 0 )
            {
                numOfHops = numOfHops * -1;
            }
            int row = startIndex.Row;
            int col = startIndex.Collum;
            for (int hops = 0; hops <= numOfHops; ++ hops)
            {
                List<Letter> cellContents = boardArray[row, col];
                foreach (Letter letter in cellContents)
                {
                    if (letter.Color != ConsoleColor.Green)
                    {

                        letter.Color = color;
                    }
                }
                row += rowChange;
                col += colChange;
            }
        }

        /// <summary>
        /// Retrieves the list of letter objects found in the specified cell of the boardArray. 
        /// Checks each one untill it finds a letter object with a Word property which matches the supplied word
        /// Using this letter object the diection of the specified word is retrieved. 
        /// Each letter object in the word is then looped through updating its colour based on the found/clear bools
        /// </summary>
        /// <param name="startRowIndex">first letter row index in the boardArray</param>
        /// <param name="startColIndex">first letter col index in the boardArray</param>
        /// <param name="word">string of the word being checked so the method can retrieve the direction of the word form the letter obeject</param>
        /// <param name="found">bool showing if this letter is part of a found word</param>
        /// <param name="clear">bool showing if this letter should be displayed in red or white</param>
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

        /// <summary>
        /// Takes a supplied direction string and uses this to asertain the hop values so the calling method can loop through the other cells
        /// which contain the currently processing word. Then returns the row and collum index changes with each loop itteration. 
        /// </summary>
        /// <param name="direction">direction the word is going</param>
        /// <param name="rowChange">row change for the next letter in the array</param>
        /// <param name="colChange">collum change for the next letter in the array</param>
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

        /// <summary>
        /// Loops through the contents of the supplied list updating the colour property of matching letter obejcts. 
        /// </summary>
        /// <param name="cellContents">list containing the letter objects in one cell of the board array</param>
        /// <param name="word">word we are validating the update against.</param>
        /// <param name="textColour">console colour to change the matching letter objects to. </param>
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

        /// <summary>
        /// Displays the current game board in terminal. 
        /// </summary>
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
                    bool yellowPresent = false;
                    bool redPresent = false;
                    bool greenPresent = false;
                    foreach (Letter letter in cellContents)
                    {
                        if (letter.Color == ConsoleColor.Yellow) yellowPresent = true;
                        if (letter.Color == ConsoleColor.Red) redPresent = true;
                        if (letter.Color == ConsoleColor.Green) greenPresent = true;
                    }
                    if (yellowPresent) charColour = ConsoleColor.Yellow;
                    else if (redPresent) charColour = ConsoleColor.Red;
                    else if (greenPresent) charColour = ConsoleColor.Green;
                    else charColour = ConsoleColor.White;
                    Console.ForegroundColor = charColour;
                    string cellstring = cellContents[0].Character;
                    Console.Write(cellstring.PadRight(4));
                }
                Console.WriteLine();
                Console.WriteLine(" ");
            }
        }

        /// <summary>
        /// Loops through the boardArray filling any empty lists contained with letter objects.
        /// Row and Collum headers are filled appropriately, then random letter objects genrated for the res of the empty lists. 
        /// Add an extra letter object so there is always a min of two per cell and one more than the number required by the number of words crossing the cell. 
        /// </summary>
        private void FillEmptyArrayCells()
        {
            Random random = new Random();
            for (int rowIndex = 0; rowIndex < boardArray.GetLength(0); rowIndex++)
            {
                for (int colIndex = 0; colIndex < boardArray.GetLength(1); colIndex++)
                {
                    Vector position = new Vector(rowIndex, colIndex);
                    if (boardArray[rowIndex, colIndex] == null)
                    {
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
                        {   //ensure we hve at least two letter objects
                            string randomChar = Convert.ToString((char)random.Next('a', 'z'));
                            List<Letter> mainAreaCellList = new List<Letter>();
                            for (int count = 0; count <= 2; count++)
                            {
                                Letter randomLetter = new Letter(randomChar, position);
                                mainAreaCellList.Add(randomLetter);
                            }
                            boardArray[rowIndex, colIndex] = mainAreaCellList;
                        }
                    }
                    else
                    {   //ensure we have two more letter objects than requited by the words crossing the cell
                        string existingChar = boardArray[rowIndex, colIndex][0].Character;
                        for (int extraCount = 0; extraCount <=2; extraCount++)
                        {
                            Letter extraLetter = new Letter(existingChar, position);
                            boardArray[rowIndex, colIndex].Add(extraLetter);
                        }
                        
                    }

                }
            }
        }

        /// <summary>
        /// Takes the supplied gameFile object and loops through each word it contains
        /// Each of those words are in turn looped through, 
        /// Letter objects are added to the board array lists in their appropriate positions. 
        /// </summary>
        /// <param name="gameFile">game file object to process</param>
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
