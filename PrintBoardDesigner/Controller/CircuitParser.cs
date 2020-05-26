using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace PrintBoardDesigner
{
    public class CircuitParser
    {
        private static string[] NodeDescriptors = new string[] { "INPUT_HIGH", "INPUT_LOW", "PROBE", "OR", "AND", "NOT", "NAND", "NOR", "XOR" };

        //TODO Deze weghalen en teruggeven met ParseFile
        private Dictionary<string, string> circuitComponentDict = new Dictionary<string, string>();
        private Dictionary<string, string[]> circuitConnectionDict = new Dictionary<string, string[]>();

        public Dictionary<string, string> CircuitComponentDict
        {
            get { return circuitComponentDict; }
            set { circuitComponentDict = value; }
        }

        public Dictionary<string, string[]> CircuitConnectionDict
        {
            get { return circuitConnectionDict; }
            set { circuitConnectionDict = value; }
        }

        public void ParseFile(List<string> fileLines)
        {
            foreach (var line in fileLines)
            {
                /// If line Starts with #, treat as comment and skip
                if (line == "" || line.Substring(0,1) == "#")
                {
                    continue;
                }

                /// Strip any white space (TAB or SPACE)
                string ln = Regex.Replace(line, @"\s+", "");

                string identifier = ln.Substring(0, ln.IndexOf(':'));
                string descriptor = ln.Substring(ln.IndexOf(':')+1, (ln.IndexOf(';')-1 - (ln.IndexOf(':'))));

                /// If the type is not one of the Node Descriptors, treat parsed elements as connections
                if (NodeDescriptors.Contains(descriptor))
                {
                    circuitComponentDict[identifier] = descriptor;
                }
                else
                {
                    string[] connList;

                    if (descriptor.Contains(','))
                    {
                        connList = descriptor.Split(',');
                    }
                    else
                    {
                        connList = new string[] { descriptor };
                    }

                    circuitConnectionDict[identifier] = connList;
                }

            }
        }
    }

}