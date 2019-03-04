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
        private List<List<string>> positionList;

        public TextFile(string fileName)
        {
            this.fileName = fileName;
        }

        public List<List<string>> Read()
        {
            try
            {
                StreamReader reader = new StreamReader(fileName);
                string lineOfText = null;
                string element = null;
                positionList = new List<List<string>>();
                while ((lineOfText = reader.ReadLine()) != null)
                {
                    List<string> row = new List<string>();
                    while ((element = reader.Read().ToString()) != null)
                    {
                        row.Add(element);
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
