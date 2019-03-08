using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DrCanoli
{
    // TextFile Class
    class TextFile
    {
        // Field and property for name of file
        private string fileName;
        public string FileName
        {
            get
            {
                return fileName;
            }
            set
            {
                fileName = value;
            }
        }
        // List of characters that respresents level data
        private List<List<char>> positionList;

        // Class constructor
        public TextFile(string fileName)
        {
            this.fileName = fileName;
        }

        public List<List<char>> Read()
        {
            try
            {
                // Initialize stream reader, string variable, and char variable
                StreamReader reader = new StreamReader(fileName);
                string lineOfText = null;
                // Create positionList
                positionList = new List<List<char>>();
                // Read in each line as a list of characters, then add this list to positionList
                while ((lineOfText = reader.ReadLine()) != null)
                {
                    List<char> row = new List<char>();
                    foreach(char ch in lineOfText)
                    {
                        row.Add(ch);
                    }
                    positionList.Add(row);
                }
            }
            // Catch any file errors
            catch (Exception e)
            {
                Console.WriteLine("File error");
            }

            return positionList;
        }
    }
}
