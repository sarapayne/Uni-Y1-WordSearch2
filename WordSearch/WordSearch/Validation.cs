using System;
using System.Collections.Generic;
using System.Text;

namespace WordSearch
{
  
    class Validation
    {
        private static List<Letter>[,] validationArray;
        private static List<Word> words;
        static int collumIndexes;
        static int collumIndex;
        static int rowIndexes;
        static int rowIndex;
        static int numberOfWords;

        public static string InitialMenu(string input)
        {
            while (input != "1" && input != "2")
            {
                Console.WriteLine("Sorry but the value: " + input + " which you entered is not valid. Your entry must be 1 or 2, please try again");
                input = Console.ReadLine();
            }
            return input;
        }

        public static bool GameFile(string[] fileContents, out List<Word> wordsOut)
        {
            words = new List<Word>();
            string[] lineSegments;
            bool fileOk = true; //set to true then if it changes to false at any time break the loop
            int lineIndex = 0;
            while (fileOk && lineIndex < fileContents.Length)
            {
                if (lineIndex == 0)
                {
                    lineSegments = fileContents[lineIndex].Split(",");
                    if (lineSegments.Length != 3)
                    {
                        fileOk = false; //drop strait out of the while loop
                    }
                    bool isIntRows = int.TryParse(lineSegments[0], out rowIndexes);
                    bool isIntCollums = int.TryParse(lineSegments[1], out collumIndexes);
                    bool isIntWords = int.TryParse(lineSegments[2], out numberOfWords);
                    if (!(isIntCollums && isIntRows && isIntWords && (fileContents.Length == (numberOfWords + 1))))
                    {
                        fileOk = false;
                    }
                    if (fileContents.Length != (numberOfWords + 1))
                    {
                        fileOk = false;
                    }
                    validationArray = new List<Letter>[rowIndexes+1, collumIndexes+1];
                }//close line inxex is 0
                else
                {
                    lineSegments = fileContents[lineIndex].Split(",");
                    bool isIntRow = int.TryParse(lineSegments[1], out rowIndex);
                    bool isIntCol = int.TryParse(lineSegments[2], out collumIndex);
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
                    }
                    else if (lineSegments[3] == "right")
                    {
                        if (rowIndex + lineSegments[0].Length >= rowIndexes)
                        {
                            fileOk = false;
                        }
                    }
                    else if (lineSegments[3] == "up")
                    {
                        if (collumIndex - lineSegments[0].Length < 0)
                        {
                            fileOk = false;
                        }
                    }
                    else if (lineSegments[3] == "down")
                    {
                        if (collumIndex + lineSegments[0].Length >= collumIndexes)
                        {
                            fileOk = false;
                        }
                    }
                    else if (lineSegments[3] == "leftup")
                    {
                        if (rowIndex - lineSegments[0].Length < 0 || collumIndex - lineSegments[0].Length < 0)
                        {
                            fileOk = false;
                        }
                    }
                    else if (lineSegments[3] == "rightup")
                    {
                        if (rowIndex + lineSegments[0].Length >= rowIndexes || collumIndex - lineSegments[0].Length < 0)
                        {
                            fileOk = false;
                        }
                     }
                    else if (lineSegments[3] == "leftdown")
                    {
                        if (rowIndex - lineSegments[0].Length < 0 || collumIndex + lineSegments[0].Length >= collumIndexes)
                        {
                            fileOk = false;
                        }
                    }
                    else if (lineSegments[3] == "rightdown")
                    {
                        if (rowIndex + lineSegments[0].Length >= rowIndexes || collumIndex + lineSegments[0].Length >= collumIndexes)
                        {
                            fileOk = false;
                        }
                    }
                    else
                    { //if its none of the above there is a problem, file not ok
                        fileOk = false;
                    }
                    List<Letter> wordObjects = GenerateWordObjects(rowIndex, collumIndex, lineSegments[0], lineSegments[3]);
                    Word word = new Word(lineSegments[0], wordObjects, false);
                    words.Add(word);
                    fileOk = TestLettersOnBoard(wordObjects); //adds object AND returns the bool based on results in the method
                }//close else not line 1
                lineIndex++;
            }//close while file ok and line index is less than limit
            wordsOut = words;
            return fileOk;
        }//close GameFile

        private static List<Letter> GenerateWordObjects(int firstRowIndex, int firstCollumIndex, string word, string direction)
        {
            List<Letter> wordObjects = new List<Letter>();
            for (int charIndex = 0; charIndex < word.Length; charIndex++)
            {
                Letter letter = new Letter();
                letter.Direction = direction;
                letter.Word = word;
                string displayChar = Convert.ToString(word[charIndex]);
                letter.Character = displayChar;
                if (charIndex == 0)
                {
                    letter.WordStart = true;
                }
                else
                {
                    letter.WordStart = false;
                }
                if (charIndex == word.Length - 1)
                {
                    letter.WordEnd = true;
                }
                else
                {
                    letter.WordEnd = false;
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
                letter.Positon = position;
                wordObjects.Add(letter);
            }//close loop through characters in the word
            return wordObjects;
        }

        private static bool TestLettersOnBoard(List<Letter> wordObjects)
        {
            bool completedSuccessfully = true;
            int wordIndex = 0;
            while (completedSuccessfully && wordIndex < wordObjects.Count)
            {

                int rowIndex = wordObjects[wordIndex].Positon.Row;
                int colIndex = wordObjects[wordIndex].Positon.Collum;
                int rowIndexes = validationArray.GetLength(0);
                int colIndexes = validationArray.GetLength(1);
                if (validationArray[rowIndex, colIndex] == null)
                {
                    List<Letter> cellList = new List<Letter>();
                    cellList.Add(wordObjects[wordIndex]);
                    validationArray[wordObjects[wordIndex].Positon.Row, wordObjects[wordIndex].Positon.Collum] = cellList;
                }
                else
                {
                    List<Letter> cellList = validationArray[wordObjects[wordIndex].Positon.Row, wordObjects[wordIndex].Positon.Collum];
                    for (int cellListIndex = 0; cellListIndex < cellList.Count; cellListIndex++)
                    {
                        if (cellList[cellListIndex].Character != wordObjects[wordIndex].Character)
                        {
                            completedSuccessfully = false;
                        }
                    }
                    cellList.Add(wordObjects[wordIndex]);
                }
                wordIndex++;
            }
            return completedSuccessfully;
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

        public static string InitialMenuChoice(string userInput)
        {
            while (!(userInput == "1" || userInput =="2"))
            {
                Console.WriteLine("Sorry but your entry of " + userInput + " is invalid, valid choices are 1 or 2. Please try again.");
                userInput = Console.ReadLine();
            }
            return userInput;
        }
    }
}
