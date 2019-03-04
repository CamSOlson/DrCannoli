using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DrCanoli
{
    class TextFile
    {
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
        private List<List<char>> positionList;

        public TextFile(string fileName)
        {
            this.fileName = fileName;
        }

        public List<List<char>> Read()
        {
            try
            {
                StreamReader reader = new StreamReader(fileName);
                string lineOfText = null;
                string element = null;
                positionList = new List<List<char>>();
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
            catch(Exception e)
            {
                Console.WriteLine("File error");
            }

            return positionList;
        }
    }
}
