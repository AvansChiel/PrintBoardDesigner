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

        private bool connectionMode;

        private Dictionary<string, string> _circuitComponentDict = new Dictionary<string, string>();
        private Dictionary<string, string[]> _circuitConnectionDict = new Dictionary<string, string[]>();

        public CircuitParser()
        {
            connectionMode = false;
        }

        public Dictionary<string, string> CircuitComponentDict
        {
            get { return _circuitComponentDict; }
            set { _circuitComponentDict = value; }
        }

        public Dictionary<string, string[]> CircuitConnectionDict
        {
            get { return _circuitConnectionDict; }
            set { _circuitConnectionDict = value; }
        }

        public void ParseFile(List<string> fileLines)
        {
            foreach (var line in fileLines)
            {
                if (line == "")
                {
                    connectionMode = true;
                    continue;
                }
                /// If line Starts with #, treat as comment and skip
                if (line.Substring(0,1) == "#")
                {
                    continue;
                }

                /// Strip any white space (TAB or SPACE)
                string ln = Regex.Replace(line, @"\s+", "");

                string identifier = ln.Substring(0, ln.IndexOf(':'));
                string descriptor = ln.Substring(ln.IndexOf(':')+1, (ln.IndexOf(';')-1 - (ln.IndexOf(':'))));

                /// If the type is not one of the Node Descriptors, treat parsed elements as connections
                if (!connectionMode)
                {
                    _circuitComponentDict[identifier] = descriptor;
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

                    _circuitConnectionDict[identifier] = connList;
                }

            }
        }
    }

}