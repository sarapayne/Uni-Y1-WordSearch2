using System;
using System.Collections.Generic;
using System.Text;

namespace WordSearch
{
    class Word
    {
        private string name;
        private bool found;

        public Word()
        {

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
