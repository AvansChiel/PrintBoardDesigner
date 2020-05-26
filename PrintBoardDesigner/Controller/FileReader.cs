using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public class FileReader
    {
        //Default file. MAKE SURE TO CHANGE THIS LOCATION AND FILE PATH TO YOUR FILE   
        //static readonly string textFile = @"D:\Github\PrintDesigner\Circuit1_FullAdder.txt";

        public FileReader()
        {
        }

        public List<string> ReadFile(string fileLocation)
        {
            List<string> fileLines = new List<string>();

            if (File.Exists(fileLocation))
            {
                /// Read file using StreamReader. Reads file line by line  
                using (StreamReader file = new StreamReader(fileLocation))
                {
                    string ln;

                    while ((ln = file.ReadLine()) != null)
                    {
                        fileLines.Add(ln);
                    }
                    file.Close();
                }
            }

            return fileLines;

        }

    }
}