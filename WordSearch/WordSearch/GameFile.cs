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

        public string Name
        {
            get { return this.Name; }
            set { this.Name = value; }
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
    }
}
