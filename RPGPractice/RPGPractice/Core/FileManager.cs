using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace RPGPractice.Core
{
    internal class FileManager
    {
        private const string ABOUT = "About.txt";
        private const string LEADERBOARD = "LeaderBoard.TXT";

        /// <summary>
        /// Returns the contents of Programs About file
        /// </summary>
        /// <returns></returns>
        public string GetAbout { get { return GetTxtToString(ABOUT); } }

        public string LeaderBoard { get { return GetTxtToString(LEADERBOARD); } }

        public void AddToLeaderBoard(string result)
        {
            //Append text to end of Leaderboard file
            AppendStringToTxt(result, LEADERBOARD);
        }


        /// <summary>
        /// Read in txt file and displays it as a MessageBox
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string GetTxtToString(string fileName)
        {
            String data = "";

            StreamReader reader = new StreamReader(fileName);

            while (!reader.EndOfStream)
            {
                data += reader.ReadLine() + "\r\n";
            }

            return data;
        }

        public void AppendStringToTxt(string newLine, string filePath)
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
