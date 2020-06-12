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

                    ln = file.ReadLine();

                    while (ln != null)
                    {
                        fileLines.Add(ln);
                        ln = file.ReadLine();
                    }
                    file.Close();
                }
            }
            else
            {
                throw new ArgumentException("File does not exist");
            }

            return fileLines;

        }

    }
}