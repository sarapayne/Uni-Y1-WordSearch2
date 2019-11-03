using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace WordSearch
{
    class Storage
    {
        private string[] defaultGame; //default game list, same format as the loaded games files
        private List<GameFile> gameFiles;

        public Storage()
        {
            defaultGame = new string[] { "9,5,2", "algorithm,0,1,right", "virus,5,3,left" };
            gameFiles = new List<GameFile>();
            PopulateGameFiles();
        }

        public List<GameFile> GameFiles
        {
            get { return this.gameFiles; }
            set { this.gameFiles = value; }
        }

        private void PopulateGameFiles()
        {
            string[] files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.wrd");
            for (int filesIndex = 0; filesIndex < files.Length; filesIndex++)
            {
                GameFile gameFile = new GameFile();
                gameFile.Name = files[filesIndex];
                gameFile.FileContents = File.ReadAllLines(files[filesIndex]);
                gameFile.Validated = Validation.GameFile(gameFile.FileContents, out List<Word>words, out Vector boardDimensions, out string rejectReason);
                gameFile.Words = words;
                gameFile.BoardDimensions = boardDimensions;
                gameFile.RejectReason = rejectReason;
                gameFiles.Add(gameFile);
            }
        }
    }
}
