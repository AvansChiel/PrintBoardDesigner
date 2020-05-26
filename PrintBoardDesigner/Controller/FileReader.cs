using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public class FileReader
    {
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