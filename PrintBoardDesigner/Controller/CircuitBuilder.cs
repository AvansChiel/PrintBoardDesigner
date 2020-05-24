using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public class CircuitBuilder
    {

        private CircuitComponentFactory circuitComponentFactory;
        private CircuitParser circuitParser;

        public CircuitBuilder()
        {
            this.CircuitComponentFactory = new CircuitComponentFactory();
            this.CircuitParser = new CircuitParser();
        }

        public CircuitComponentFactory CircuitComponentFactory
        {
            get { return circuitComponentFactory; }
            set { circuitComponentFactory = value; }
        }

        public CircuitParser CircuitParser
        {
            get { return circuitParser; }
            set { circuitParser = value; }
        }

        public Circuit buildCircuit()
        {
            this.CircuitParser.ParseFile();
            Dictionary<string, string> componentsStringsDict = this.CircuitParser.CircuitComponentDict;
            Dictionary<string, string[]> connectionsStringsDict = this.CircuitParser.CircuitConnectionDict;

            Dictionary<string, CircuitComponent> componentsDict = new Dictionary<string, CircuitComponent>();

            foreach (KeyValuePair<string, string> entry in componentsStringsDict)
            {
                CircuitComponent component = this.CircuitComponentFactory.CreateCircuitComponent(entry.Key, entry.Value);
                componentsDict.Add(entry.Key, component);
                // do something with entry.Value or entry.Key
            }

            foreach (KeyValuePair<string, string[]> entry in connectionsStringsDict)
            {
                foreach(string val in entry.Value)
                {
                    componentsDict[entry.Key].outputs.Add(componentsDict[val]);
                }
                
                // do something with entry.Value or entry.Key
            }






            Circuit circuit = new Circuit();
            return circuit;
        }
    }
}