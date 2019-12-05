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

        
        /// <summary>
        /// Starts the main application by generating a new wordSearchObject. 
        /// </summary>
        public static void Startup()
        {
            wordSearch = new WordSearch();
        }
    }
}
