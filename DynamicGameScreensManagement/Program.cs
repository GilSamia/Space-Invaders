using SpaceInvaders;
using System;

namespace GameScreens
{

    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new GameWithScreens())
                game.Run();
        }
    }
}
