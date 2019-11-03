using System;
using System.Collections.Generic;
using System.Text;

namespace WordSearch
{
    class Word
    {
        private string name;
        private List<Letter> letters;
        private bool found;

        public Word()
        {

        }

        public Word (string nameString, List<Letter>letterObjects, bool isfound)
        {
            name = nameString;
            letters = letterObjects;
            found = isfound;
        }

        public List<Letter> Letters
        {
            get { return this.letters; }
            set { this.letters = value; }
        }

        public Word(string wordName, bool isfound)
        {
            this.name = wordName;
            this.found = isfound;
        }

        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        public bool Found
        {
            get { return this.found; }
            set { this.found = value; }
        }
    }
}
