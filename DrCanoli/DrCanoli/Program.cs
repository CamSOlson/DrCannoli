using System;

namespace DrCanoli
{
#if WINDOWS || LINUX
    /// <summary>
    /// The main class.
    /// Ravioli ravioli we're submitting this to Cascioli
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (var game = new Game1())
                game.Run();
        }
    }
#endif
}
