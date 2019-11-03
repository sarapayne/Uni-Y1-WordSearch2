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

        public static bool GameFile(string[] fileContents, out List<Word> wordsOut, out string rejectReason)
        {
            words = new List<Word>();
            string fileRejectReason = "";
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
                        fileRejectReason = "1st Line or later does not contain 3 elements as a CSV";
                        fileOk = false; //drop strait out of the while loop
                    }
                    bool isIntRows = int.TryParse(lineSegments[0], out rowIndexes);
                    bool isIntCollums = int.TryParse(lineSegments[1], out collumIndexes);
                    bool isIntWords = int.TryParse(lineSegments[2], out numberOfWords);
                    if(rowIndexes < 1)
                    {   //reversed row/collum names for user perspective. 
                        fileRejectReason = "Board has no collums";
                        fileOk = false;
                    }
                    if (collumIndexes < 1 )
                    {   //reversed row/collum names for user perspective.
                        fileRejectReason = "Board has no rows";
                        fileOk = false;
                    }
                    if (numberOfWords <1 )
                    {
                        fileRejectReason = "Board has no words";
                        fileOk = false;
                    }
                    if (!(isIntCollums && isIntRows && isIntWords))
                    {
                        fileRejectReason = "Board size integers are not integers in the file";
                        fileOk = false;
                    }
                    if (fileContents.Length != (numberOfWords + 1))
                    {
                        fileRejectReason = "Number of words reported does not match the number of lines in the file";
                        fileOk = false;
                    }
                    validationArray = new List<Letter>[rowIndexes+1, collumIndexes+1];
                }//close line inxex is 0
                else
                {
                    lineSegments = fileContents[lineIndex].Split(",");
                    if (lineSegments.Length == 4)
                    {
                        bool isIntRow = int.TryParse(lineSegments[1], out rowIndex);
                        bool isIntCol = int.TryParse(lineSegments[2], out collumIndex);
                        int numIndexesToCheck = (lineSegments[0].Length) - 1;//remove one as we are already at first index. 
                        rowIndex++;//add 1 to row and collum indexes to allow for formatting rows and collums
                        collumIndex++; //add 1 to row and collum indexes to allow for formatting rows and collums
                        if (!(isIntRow && isIntCol))
                        {
                            fileRejectReason = "Row or Collum provided is not an integer";
                            fileOk = false;
                            break;
                        }
                        else if (lineSegments[3] == "left")
                        {
                            if (rowIndex - numIndexesToCheck < 0)
                            {
                                fileRejectReason = lineSegments[0] + " Index out of bounds left";
                                fileOk = false;
                                break;
                            }
                        }
                        else if (lineSegments[3] == "right")
                        {
                            if (rowIndex + numIndexesToCheck > rowIndexes)
                            {
                                fileRejectReason = lineSegments[0] + " Index out of bounds right";
                                fileOk = false;
                                break;
                            }
                        }
                        else if (lineSegments[3] == "up")
                        {
                            if (collumIndex - numIndexesToCheck < 0)
                            {
                                fileRejectReason = lineSegments[0] + " Index out of bounds up";
                                fileOk = false;
                                break;
                            }
                        }
                        else if (lineSegments[3] == "down")
                        {
                            if (collumIndex + numIndexesToCheck > collumIndexes)
                            {
                                fileRejectReason = lineSegments[0] + " Index out of bounds down";
                                fileOk = false;
                                break;
                            }
                        }
                        else if (lineSegments[3] == "leftup")
                        {
                            if (rowIndex - numIndexesToCheck < 0 || collumIndex - lineSegments[0].Length < 0)
                            {
                                fileRejectReason = lineSegments[0] + " Index out of bounds leftup";
                                fileOk = false;
                                break;
                            }
                        }
                        else if (lineSegments[3] == "rightup")
                        {
                            if (rowIndex + numIndexesToCheck > rowIndexes || collumIndex - lineSegments[0].Length < 0)
                            {
                                fileRejectReason = lineSegments[0] + " Index out of bounds rightup";
                                fileOk = false;
                                break;
                            }
                        }
                        else if (lineSegments[3] == "leftdown")
                        {
                            if (rowIndex - numIndexesToCheck < 0 || collumIndex + lineSegments[0].Length > collumIndexes)
                            {
                                fileRejectReason = lineSegments[0] + " Index out of bounds leftdown";
                                fileOk = false;
                                break;
                            }
                        }
                        else if (lineSegments[3] == "rightdown")
                        {
                            if (rowIndex + numIndexesToCheck > rowIndexes || collumIndex + lineSegments[0].Length > collumIndexes)
                            {
                                fileRejectReason = lineSegments[0] + " Index out of bounds rightdown";
                                fileOk = false;
                                break;
                            }
                        }
                        else
                        { //if its none of the above there is a problem, file not ok
                            fileRejectReason = "malformed direction";
                            fileOk = false;
                            break;
                        }
                    }
                    else
                    {
                        fileRejectReason = "1 or more of the word lines does not have 4 elements as CSV";
                        fileOk = false;
                        break;
                    }
                    if (!fileOk)
                    {
                        break;
                    }
                    List<Letter> wordObjects = GenerateWordObjects(rowIndex, collumIndex, lineSegments[0], lineSegments[3]);
                    bool wordPlacementOk = TestLettersOnBoard(wordObjects, lineSegments[0], out string reject, out int rowReject, out int colReject); //adds object AND returns the bool based on results in the method
                    if (wordPlacementOk)
                    {
                        Word word = new Word(lineSegments[0], wordObjects, false);
                        words.Add(word);
                    }
                    else
                    {
                        fileRejectReason = "Crossing Words at (" + rowReject + "," + colReject + ") incompatible.";
                        fileOk = false;
                        break;
                    }
                    if (!fileOk)
                    {
                        break;
                    }
                }//close else not line 1
                lineIndex++;
            }//close while file ok and line index is less than limit
            wordsOut = words;
            rejectReason = fileRejectReason;
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
                    position.Collum = firstCollumIndex - charIndex;
                }
                else if (direction == "down")
                {
                    position.Row = firstRowIndex;
                    position.Collum = firstCollumIndex + charIndex;
                }
                else if (direction == "leftup")
                {
                    position.Row = firstRowIndex - charIndex;
                    position.Collum = firstCollumIndex - charIndex;
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

        private static bool TestLettersOnBoard(List<Letter> wordObjects, string wordName, out string reject, out int rowReject, out int colReject)
        {
            string rejectWord = "";
            int colRefused = 0;
            int rowRefused = 0;
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
                            rejectWord = wordName;
                            rowRefused = rowIndex;
                            colIndex = colRefused;
                            completedSuccessfully = false;
                        }
                    }
                    cellList.Add(wordObjects[wordIndex]);
                }
                wordIndex++;
            }
            reject = rejectWord;
            rowReject = rowRefused;
            colReject = colRefused;
            return completedSuccessfully;
        }

        public static int CheckIntInRange(string input, int lowest, int highest)
        {
            bool isInt = int.TryParse(input, out int number);
            bool valid = isInt && (number > lowest) && (number < highest) && Program.wordSearch.Storage.GameFiles[number-1].Validated;
            while (!valid)
            {
                Console.WriteLine("Sorry but your entry of: " + input + "was not valid, you must choose a integer number from " + lowest + "to" + highest + ". It must also be an availible game.");
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


