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
        private List<int> positionList;

        public TextFile(string fileName)
        {
            this.fileName = fileName;
        }

        public List<int> Read()
        {
            StreamReader reader = null;
            string lineOfText = null;
            positionList = new List<int>();
            while((lineOfText = reader.ReadLine()) != null)
            {
                positionList.Add(int.Parse(lineOfText));
            }

            return positionList;
        }
    }
}
