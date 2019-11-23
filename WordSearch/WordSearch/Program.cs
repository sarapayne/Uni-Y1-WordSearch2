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
        /// Starts the main aplication by generating a new wordSearch object
        /// </summary>
        /// <param name="name of the parameter here"> Description of the parameter here </param>
        /// <returns> if something gets returned add it here </returns>

        
        /// <summary>
        /// Starts the main application by generating a new wordSearchObject. 
        /// </summary>
        public static void Startup()
        {
            wordSearch = new WordSearch();
        }
    }
}
