using System;
using System.Collections.Generic;
using System.Text;

namespace WordSearch
{
    class GameFile
    {
        private string name;
        private string[] fileContents;
        private bool validated;
        private List<Word> words;

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public string[] FileContents
        {
            get { return this.fileContents; }
            set { this.fileContents = value; }
        }

        public bool Validated
        {
            get { return this.validated; }
            set { this.validated = value; }
        }

        public List<Word> Words
        {
            get { return this.words; }
            set { this.Words = value; }
        }
    }
}
