using System;
using System.Collections.Generic;
using System.Text;

namespace WordSearch
{
    class Validation
    {
        private int collumIndexes;
        private int collumIndex;
        private int rowIndexes;
        private int rowIndex;
        private int numberOfWords;

        private List<Letter> GenerateWordObjects(int firstRowIndex, int firstCollumIndex, string word, string direction)
        {
            List<Letter> wordObjects = new List<Letter>();
            for (int charIndex = 0; charIndex < word.Length; charIndex++)
            {
                Letter gameObject = new Letter();
                gameObject.Direction = direction;
                gameObject.Word = word;
                gameObject.Character = Convert.ToString(word[charIndex]);
                if (charIndex == 0)
                {
                    gameObject.WordStart = true;
                }
                else
                {
                    gameObject.WordStart = false;
                }
                if (charIndex == word.Length - 1)
                {
                    gameObject.WordEnd = true;
                }
                else
                {
                    gameObject.WordEnd = false;
                }
                Vector position = new Vector();
                if (direction == "left")
                {
                    position.Row = firstRowIndex - charIndex;
                    position.Collum = firstCollumIndex;
                }
                else if (direction == "right")
                {
                    position.Row = firstRowIndex + charIndex;
                    position.Collum = firstCollumIndex;
                }
                else if (direction == "up")
                {
                    position.Row = firstRowIndex;
                    position.Collum = firstRowIndex - charIndex;
                }
                else if (direction == "down")
                {
                    position.Row = firstRowIndex;
                    position.Collum = firstCollumIndex + charIndex;
                }
                else if (direction == "leftup")
                {
                    position.Row = firstRowIndex - collumIndex;
                    position.Collum = firstCollumIndex - collumIndex;
                }
                else if (direction == "rightup")
                {
                    position.Row = firstRowIndex + charIndex;
                    position.Collum = firstCollumIndex - charIndex;
                }
                else if (direction == "leftdown")
                {
                    position.Row = firstRowIndex - charIndex;
                    position.Collum = firstCollumIndex + charIndex;
                }
                else if (direction == "rightdown")
                {
                    position.Row = firstRowIndex + charIndex;
                    position.Collum = firstCollumIndex + charIndex;
                }
                gameObject.Positon = position;
                wordObjects.Add(gameObject);
            }//close loop through characters in the word
            return wordObjects;
        }

        private bool AddWordObjectsToBoard(List<Letter> wordObjects)
        {
            bool completedSuccessfully = true;
            while (completedSuccessfully)
            {
                foreach (Letter letter in wordObjects)
                {
                    int rowIndex = letter.Positon.Row;
                    int colIndex = letter.Positon.Collum;
                    if (Program.wordSearch.CellObjects[rowIndex, rowIndex] == null)
                    {
                        List<Letter> cellList = new List<Letter>();
                        cellList.Add(letter);
                        Program.wordSearch.CellObjects[letter.Positon.Row, letter.Positon.Collum] = cellList;
                    }
                    else
                    {
                        List<Letter> cellList = Program.wordSearch.CellObjects[letter.Positon.Row, letter.Positon.Collum];
                        for (int cellListIndex = 0; cellListIndex < cellList.Count; cellListIndex++)
                        {
                            if (cellList[cellListIndex].Character != letter.Character)
                            {
                                completedSuccessfully = false;
                            }
                        }
                        cellList.Add(letter);
                    }
                }
            }
            return completedSuccessfully;
        }

        public string InitialMenu(string input)
        {
            while (input != "1" && input != "2")
            {
                Console.WriteLine("Sorry but the value: " + input + " which you entered is not valid. Your entry must be 1 or 2, please try again");
                input = Console.ReadLine();
            }
            return input;
        }

        public int CheckIntInRange(string input, int lowest, int highest)
        {
            bool isInt = int.TryParse(input, out int number);
            while (!isInt || number < lowest || number > highest)
            {
                Console.WriteLine("Sorry but your entry of: " + input + "was not valid, you must choose a integer number from " + lowest + "to" + highest);
                input = Console.ReadLine();
                isInt = int.TryParse(input, out number);
            }
            return number;
        }

        public void GameFile(string[] fileContents)
        {
            string[] lineSegments;
            bool fileOk = true; //set to true then if it changes to false at any time break the loop
            while (fileOk)
            {
                for (int lineIndex = 0; lineIndex < fileContents.Length; lineIndex++)
                {
                    if (lineIndex == 0)
                    {
                        lineSegments = fileContents[lineIndex].Split(",");
                        if (lineSegments.Length != 3)
                        {
                            fileOk = false; //drop strait out of the while loop
                        }
                        bool isIntCollums = int.TryParse(lineSegments[0], out collumIndexes);
                        bool isIntRows = int.TryParse(lineSegments[1], out rowIndexes);
                        bool isIntWords = int.TryParse(lineSegments[2], out numberOfWords);
                        if (!(isIntCollums && isIntRows && isIntWords && (fileContents.Length == (numberOfWords + 1))))
                        {
                            fileOk = false;
                        }

                        if (fileContents.Length != (numberOfWords + 1))
                        {
                            fileOk = false;
                        }
                        Program.wordSearch.MakeNewArray(rowIndexes + 1, collumIndexes + 1);
                    }//close line inxex is 0
                    else
                    {
                        lineSegments = fileContents[lineIndex].Split(",");
                        bool isIntRow = int.TryParse(lineSegments[1], out rowIndex);
                        bool isIntCol = int.TryParse(lineSegments[2], out collumIndex);

                        bool wordAlreadyExists = CheckIfWordAlreadyExists(lineSegments[0]);
                        if (wordAlreadyExists)
                        {   //duplicate entries for the same word exist, exit 
                            fileOk = false;
                        }
                        else
                        {
                            Word word = new Word(lineSegments[0], false);
                            Program.wordsInCurrentGame.Add(word);
                        }
                        rowIndex++;//add 1 to row and collum indexes to allow for formatting rows and collums
                        collumIndex++; //add 1 to row and collum indexes to allow for formatting rows and collums
                        if (!(lineSegments.Length == 4 && isIntRow && isIntCol))
                        {
                            fileOk = false;
                        }
                        else if (lineSegments[3] == "left")
                        {
                            if (rowIndex - lineSegments[0].Length < 0)
                            {
                                fileOk = false;
                            }
                            else
                            {
                                List<Letter> wordObjects = GenerateWordObjects(rowIndex, collumIndex, lineSegments[0], lineSegments[3]);
                                fileOk = AddWordObjectsToBoard(wordObjects); //adds object AND returns the bool based on results in the method
                            }
                        }
                        else if (lineSegments[3] == "right")
                        {
                            if (rowIndex + lineSegments[0].Length >= rowIndexes)
                            {
                                fileOk = false;
                            }
                            else
                            {
                                List<Letter> wordObjects = GenerateWordObjects(rowIndex, collumIndex, lineSegments[0], lineSegments[3]);
                                fileOk = AddWordObjectsToBoard(wordObjects); //adds object AND returns the bool based on results in the method
                            }
                        }
                        else if (lineSegments[3] == "up")
                        {
                            if (collumIndex - lineSegments[0].Length < 0)
                            {
                                fileOk = false;
                            }
                            else
                            {
                                List<Letter> wordObjects = GenerateWordObjects(rowIndex, collumIndex, lineSegments[0], lineSegments[3]);
                                fileOk = AddWordObjectsToBoard(wordObjects); //adds object AND returns the bool based on results in the method
                            }
                        }
                        else if (lineSegments[3] == "down")
                        {
                            if (collumIndex + lineSegments[0].Length >= collumIndexes)
                            {
                                fileOk = false;
                            }
                            else
                            {
                                List<Letter> wordObjects = GenerateWordObjects(rowIndex, collumIndex, lineSegments[0], lineSegments[3]);
                                fileOk = AddWordObjectsToBoard(wordObjects); //adds object AND returns the bool based on results in the method
                            }
                        }
                        else if (lineSegments[3] == "leftup")
                        {
                            if (rowIndex - lineSegments[0].Length < 0 || collumIndex - lineSegments[0].Length < 0)
                            {
                                fileOk = false;
                            }
                            else
                            {
                                List<Letter> wordObjects = GenerateWordObjects(rowIndex, collumIndex, lineSegments[0], lineSegments[3]);
                                fileOk = AddWordObjectsToBoard(wordObjects); //adds object AND returns the bool based on results in the method
                            }
                        }
                        else if (lineSegments[3] == "rightup")
                        {
                            if (rowIndex + lineSegments[0].Length >= rowIndexes || collumIndex - lineSegments[0].Length < 0)
                            {
                                fileOk = false;
                            }
                            else
                            {
                                List<Letter> wordObjects = GenerateWordObjects(rowIndex, collumIndex, lineSegments[0], lineSegments[3]);
                                fileOk = AddWordObjectsToBoard(wordObjects); //adds object AND returns the bool based on results in the method
                            }
                        }
                        else if (lineSegments[3] == "leftdown")
                        {
                            if (rowIndex - lineSegments[0].Length < 0 || collumIndex + lineSegments[0].Length >= collumIndexes)
                            {
                                fileOk = false;
                            }
                            else
                            {
                                List<Letter> wordObjects = GenerateWordObjects(rowIndex, collumIndex, lineSegments[0], lineSegments[3]);
                                fileOk = AddWordObjectsToBoard(wordObjects); //adds object AND returns the bool based on results in the method
                            }
                        }
                        else if (lineSegments[3] == "rightdown")
                        {
                            if (rowIndex + lineSegments[0].Length >= rowIndexes || collumIndex + lineSegments[0].Length >= collumIndexes)
                            {
                                fileOk = false;
                            }
                            else
                            {
                                List<Letter> wordObjects = GenerateWordObjects(rowIndex, collumIndex, lineSegments[0], lineSegments[3]);
                                fileOk = AddWordObjectsToBoard(wordObjects); //adds object AND returns the bool based on results in the method
                            }
                        }
                        else
                        { //if its none of the above there is a problem, file not ok
                            fileOk = false;
                        }
                    }
                }
            }//close while file ok
            if (!fileOk)
            {
                Console.WriteLine("Sorry but the file you selected is incorrectly configured and can not be loaded. Please press any key to return to the main menu where you can make another choice.");
                Console.ReadKey();
                Program.Startup();
            }
        }//close GameFile

        private bool CheckIfWordAlreadyExists(string input)
        {
            bool found = false;
            int wordsIndex = 0;
            while (!found && wordsIndex < Program.wordsInCurrentGame.Count)
            {
                if (Program.wordsInCurrentGame[wordsIndex].Name == input)
                {
                    found = true;
                }
                wordsIndex++;
            }
            return found;
        }
    }
}
