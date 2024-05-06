using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace RPGPractice.Core
{
    /// <summary>
    /// FileManager class
    /// Developer: Taylor Fox
    /// Handles File Management for hard-stored data such as About files and save data
    /// </summary>
    internal class FileManager
    {
        private const string ABOUT = "About.txt";
        private const string LEADERBOARD = "LeaderBoard.TXT";

        /// <summary>
        /// Returns the contents of the program About file
        /// </summary>
        /// <returns></returns>
        public static string GetAbout { get { return GetTxtToString(ABOUT); } }

        /// <summary>
        /// Returns the contents of the Leaderboard file
        /// </summary>
        public static string LeaderBoard { get { return GetTxtToString(LEADERBOARD); } }

        /// <summary>
        /// Adds a new line to the LeaderBoard
        /// </summary>
        /// <param name="result">String of text to be added</param>
        public static void AddToLeaderBoard(string result)
        {
            //Append text to end of Leaderboard file
            AppendStringToTxt(result, LEADERBOARD);
        }


        /// <summary>
        /// Read in txt file and displays it as a MessageBox
        /// </summary>
        /// <param name="filePath">Path to the TXT file</param>
        /// <returns></returns>
        public static string GetTxtToString(string filePath)
        {
            String data = "";

            StreamReader reader = new StreamReader(filePath);

            while (!reader.EndOfStream)
            {
                data += reader.ReadLine() + "\r\n";
            }

            return data;
        }

        /// <summary>
        /// Adds a new Line to an existing Txt file
        /// </summary>
        /// <param name="newLine">New string to be added</param>
        /// <param name="filePath">Path to the TXT file</param>
        public static void AppendStringToTxt(string newLine, string filePath)
        {
            // Append the string to the file
            using (StreamWriter writer = File.AppendText(filePath))
            {
                writer.WriteLine(newLine);
            }

            Console.WriteLine("String appended to the file.");
        }
    }
}
