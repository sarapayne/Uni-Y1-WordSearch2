using System;
using System.Collections.Generic;
using System.Text;

namespace WordSearch
{
    class WordSearch
    {
        private Board board;
        private Validation validation;
        private static List<Word> wordsInCurrentGame;
        private Storage storage;
        private int gameIndex;

        ConsoleColor textColour;
        Vector lastWrongStartIndex;
        Vector lastWrongEndIndex;
        
        public WordSearch()
        {
            validation = new Validation();
            wordsInCurrentGame = new List<Word>();
            storage = new Storage();
            InitialMenu();
            gameIndex = new int();
            DisplayWordChoices();
            //InGameChoices();
        }

        public Storage Storage
        {
            get { return this.Storage; }
            set { this.Storage = value; }
        }

        public static void AddWordToGameWordsList(Word word)
        {
            Word newWord = new Word();
            newWord = word;
            wordsInCurrentGame.Add(newWord);
        }

        private void DisplayWordChoices()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Words To Find");
            foreach (Word word in wordsInCurrentGame)
            {
                if (word.Found)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.White;
                }
                Console.WriteLine(word.Name);
            }
            Console.ResetColor();
        }

        public void InitialMenu()
        {
            wordsInCurrentGame = new List<Word>();
            Console.Clear();
            Console.WriteLine("Select An Option \n" +
                "1. Use default wordsearch \n" +
                "2. Load wordsearch from file \n" +
                "\n" +
                "Enter a number from 1 to 2 inclusive");
            string menuChoice = Console.ReadLine();
            menuChoice = Validation.InitialMenu(menuChoice);
            GameFile gameFileToLoad = LoadGameChoice(menuChoice);
            wordsInCurrentGame = new List<Word>();
            board = new Board(gameFileToLoad);
        }

        private GameFile LoadGameChoice(string menuChoice)
        {
            GameFile gameToLoad = new GameFile();
            if (menuChoice == "1")
            {
                bool gameFile1Valid = storage.GameFiles[0].Validated;
                if (gameFile1Valid)
                {   //we know this is the same as file index0, so if its valid save some leg work. 
                    gameToLoad = storage.DefaultGameFile;
                } 
            }
            else if (menuChoice == "2")
            {
                gameIndex = FileChoiceMenu();
                gameToLoad = storage.GameFiles[gameIndex];
            }
            return gameToLoad;
        }

        private int FileChoiceMenu()
        {
            Console.Clear();
            Console.Write("Select a file to load, games shown in ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("blue ");
            Console.ResetColor();
            Console.Write("are saved but have errors and can not be loaded");
            Console.WriteLine();
            for (int filesIndex = 0; filesIndex < storage.GameFiles.Count; filesIndex++)
            {
                Console.Write(filesIndex + 1 + ". ");
                if (!(storage.GameFiles[filesIndex].Validated))
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                }
                Console.Write(storage.GameFiles[filesIndex].Name);
                if (storage.GameFiles[filesIndex].RejectReason != "")
                {
                    Console.Write(": " + storage.GameFiles[filesIndex].RejectReason);
                }
                Console.WriteLine();
                Console.ResetColor();
            }
            Console.ResetColor();
            Console.WriteLine("\n" +
                "Enter a number from: 1 to " + storage.GameFiles.Count + " inclusive");
            int userChoice = (Validation.CheckIntInRange(Console.ReadLine(), 1, storage.GameFiles.Count));
            bool validFile = storage.GameFiles[userChoice - 1].Validated;
            if (!validFile)
            {
                Console.WriteLine("Sorry but the file choice you chose has an error, please press any key to return to the main menu then select a choice in white");
                Console.ReadKey();
                InitialMenu();
            }
            return userChoice;

        }
    }
}



