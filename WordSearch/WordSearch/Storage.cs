using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace WordSearch
{
    class Storage
    {
        private string[] files;
        private string[] fileContents;
        private string[] defaultGame; //default game list, same format as the loaded games files

        public Storage()
        {
            files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.wrd");
            defaultGame = new string[] { "9,5,2", "algorithm,0,1,right", "virus,5,3,left" };
        }

        public string[] Files
        {
            get { return this.files; }
            set { this.files = value; }
        }

        public string[] FileContents
        {
            get { return this.fileContents; }
            set { this.fileContents = value; }
        }

        public void LoadFile(int filesIndex)
        {
            if (filesIndex == -1)
            {
                fileContents = defaultGame;
            }
            else
            {
                fileContents = File.ReadAllLines(files[filesIndex]);
            }
            Program.validation.GameFile(fileContents);
        }
    }
}
