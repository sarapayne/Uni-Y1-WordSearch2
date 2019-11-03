using System;

namespace WordSearch
{
    class Program
    {
        public static WordSearch wordSearch;

        static void Main(string[] args)
        {
            Startup();
        }

        public static void Startup()
        {
            wordSearch = new WordSearch();
        }
    }
}
