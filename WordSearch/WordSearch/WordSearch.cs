using System;
using System.Collections.Generic;
using System.Text;

namespace WordSearch
{
    class WordSearch
    {
        private Board board;
        private Validation validation;
        private List<Word> wordsInCurrentGame;
        private Storage storage;

        public WordSearch()
        {
            validation = new Validation();
            storage = new Storage();
            InitialMenu();
            PrintWordChoices();
            InGameChoices();
        }

        private void InitialMenu()
        {
            wordsInCurrentGame = new List<Word>();
            Console.Clear();
            Console.WriteLine("Select An Option \n" +
                "1. Use default wordsearch \n" +
                "2. Load wordsearch from file \n" +
                "\n" +
                "Enter a number from 1 to 2 inclusive");
            string menuChoice = Console.ReadLine();
            menuChoice = validation.InitialMenu(menuChoice);
            LoadGameChoice(menuChoice);
            wordSearch = new Board(userInput);
        }

        private void LoadGameChoice(string menuChoice)
        {
            if (menuChoice == "1")
            {
                gameIndex = -1; //one lower than any of the possible indexes found in the files array       
            }
            else if (menuChoice == "2")
            {
                loadedGame = new List<string>();

                gameIndex = FileChoicesMenu();
            }
            storage.LoadFile(gameIndex);
            textColour = new ConsoleColor();
            lastWrongStart = new Vector();
            lastWrongEnd = new Vector();
            //cellObjects = new List<GameObject>[Program.validation.CellObjects.GetLength(0), Program.validation.CellObjects.GetLength(1)];
            FillEmptyCells();
            DisplayBoard();
        }
    }
}
}
