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
        private int gameIndex;

        ConsoleColor textColour;
        Vector lastWrongStartIndex;
        Vector lastWrongEndIndex;
        
        public WordSearch()
        {
            validation = new Validation();
            storage = new Storage();
            InitialMenu();
            //PrintWordChoices();
            //InGameChoices();
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
            //wordSearch = new Board(userInput);
        }

        private void LoadGameChoice(string menuChoice)
        {
            if (menuChoice == "1")
            {
                gameIndex = -1; //one lower than any of the possible indexes found in the files array       
            }
            else if (menuChoice == "2")
            {
                gameIndex = FileChoicesMenu();
            }
            storage.LoadFile(gameIndex);

            /*
            textColour = new ConsoleColor();
            lastWrongStartIndex = new Vector();
            lastWrongEndIndex = new Vector();
            //cellObjects = new List<GameObject>[Program.validation.CellObjects.GetLength(0), Program.validation.CellObjects.GetLength(1)];
            FillEmptyCells();
            DisplayBoard();
            */
        }

        private int FileChoicesMenu()
        {
            Console.Clear();
            Console.WriteLine("Select a file to load");
            for (int filesIndex = 0; filesIndex < storage.Files.Length; filesIndex++)
            {
                Console.WriteLine(filesIndex + 1 + ". " + storage.Files[filesIndex]);
            }
            Console.WriteLine("\n" +
                "Enter a number from: 1 to " + storage.Files.Length + " inclusive");
            int userChoice = validation.CheckIntInRange(Console.ReadLine(), 1, storage.Files.Length);
            userChoice--; //subtract 1 to give the file index in the array of availible files
            return userChoice;
        }
    }
}

