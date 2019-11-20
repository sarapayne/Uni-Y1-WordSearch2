using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace WordSearch
{
    class Storage
    {
        private string[] defaultGame; //default game list, same format as the loaded games files
        private GameFile defaultGameFile;
        private List<GameFile> gameFiles;

        public Storage()
        {
            defaultGame = new string[] { "9,5,2", "algorithm,0,1,right", "virus,5,4,left" };
            gameFiles = new List<GameFile>();
            PopulateGameFiles();
        }

        public List<GameFile> GameFiles
        {
            get { return this.gameFiles; }
            set { this.gameFiles = value; }
        }

        public GameFile DefaultGameFile
        {
            get { return this.defaultGameFile; }
            set { this.defaultGameFile = value; }
        }

        private void PopulateGameFiles()
        {
            List<Word> words = new List<Word>();
            Vector boardDimensions = new Vector();
            string rejectReason = "";
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.wrd");
            for (int filesIndex = 0; filesIndex < files.Length; filesIndex++)
            {
                GameFile gameFile = new GameFile();
                gameFile.Name = files[filesIndex];
                try
                {
                    gameFile.FileContents = File.ReadAllLines(files[filesIndex]);
                }
                catch
                {
                    gameFile.FileContents = new string[]{""};
                }
                gameFile.Validated = Validation.GameFile(gameFile.FileContents, out words, out boardDimensions, out rejectReason);
                gameFile.Words = words;
                gameFile.BoardDimensions = boardDimensions;
                gameFile.RejectReason = rejectReason;
                gameFiles.Add(gameFile);
            }
            defaultGameFile = new GameFile();
            defaultGameFile.Name = "DefaultGame";
            defaultGameFile.FileContents = defaultGame;
            defaultGameFile.Validated = Validation.GameFile(defaultGameFile.FileContents, out words, out boardDimensions, out rejectReason);
            defaultGameFile.Words = words;
            defaultGameFile.BoardDimensions = boardDimensions;
            defaultGameFile.RejectReason = rejectReason;
        }
    }
}
