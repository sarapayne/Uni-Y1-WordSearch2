using System;
using System.Collections.Generic;
using System.Text;

namespace WordSearch
{
    class Letter
    {

        private bool wordStart;
        private bool wordEnd;
        private Vector position;
        private string character;
        private string word;
        private string direction;
        private ConsoleColor color;

        public Letter()
        {
            color = ConsoleColor.White;
        }

        public Letter(ConsoleColor colour)
        {
            color = colour;
        }

        public Letter(bool isStart, bool isEnd, Vector pos, string letter, string inWord, ConsoleColor colour)
        {
            wordStart = isStart;
            wordEnd = isEnd;
            position = pos;
            character = letter;
            word = inWord;
            color = colour;
        }

        public Letter (string letter, Vector pos)
        {
            character = letter;
            direction = "";
            position = pos;
            word = "";
            wordEnd = false;
            WordStart = false;
        }

        public string Direction
        {
            get { return this.direction; }
            set { this.direction = value; }
        }

        public bool WordStart
        {
            get { return this.wordStart; }
            set { this.wordStart = value; }
        }

        public bool WordEnd
        {
            get { return this.wordEnd; }
            set { this.wordEnd = value; }
        }

        public Vector Positon
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public string Character
        {
            get { return this.character; }
            set { this.character = value; }
        }

        public string Word
        {
            get { return this.word; }
            set { this.word = value; }
        }

        public ConsoleColor Color
        {
            get { return this.color; }
            set { this.color = value; }
        }
    }
}
