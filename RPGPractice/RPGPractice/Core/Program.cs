using RPGPractice.Engine;

namespace RPGPractice.Core
{
    internal static class Program
    {

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Start EventManager to handle Event relay between engine and Gui
            EventManager eventManager = new EventManager();

            //start GameEngine (Game logic handler)
            GameEngine gameEngine = new GameEngine(eventManager);

            // Start GameForm GUI for display
            ApplicationConfiguration.Initialize();
            Application.Run(new GameForm(eventManager));
        }
    }
}