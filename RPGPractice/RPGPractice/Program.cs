using RPGPractice.Engine;
using RPGPractice.Events;

namespace RPGPractice
{
    internal static class Program
    {

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //create GameEngine and EventManager
            EventManager eventManager= new EventManager();
            GameEngine gameEngine = new GameEngine(eventManager);

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new GameForm(eventManager));
        }
    }
}