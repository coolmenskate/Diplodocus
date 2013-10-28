using System;

namespace Diplodocus
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (Diplodocus game = new Diplodocus())
            {
                game.Run();
            }
        }
    }
#endif
}

