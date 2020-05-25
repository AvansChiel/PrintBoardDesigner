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
            Console.WriteLine("beginbuild");
            this.CircuitParser.ParseFile();
            Dictionary<string, string> componentsStringsDict = this.CircuitParser.CircuitComponentDict;
            Dictionary<string, string[]> connectionsStringsDict = this.CircuitParser.CircuitConnectionDict;

            Dictionary<string, CircuitComponent> componentsDict = new Dictionary<string, CircuitComponent>();
            List<CircuitComponent> inputNodesList = new List<CircuitComponent>();

            foreach (KeyValuePair<string, string> entry in componentsStringsDict)
            {
                string type = entry.Value;
                if (type.Contains("INPUT")){
                    type = "INPUT";
                }
                CircuitComponent component = this.CircuitComponentFactory.CreateCircuitComponent(entry.Key, type);
                //substr on input nodes to create correct component
                if (type == "INPUT")
                {
                    if (entry.Value.Contains("HIGH"))
                    {
                        component.hasCurrent = true;
                    }
                    else
                    {
                        component.hasCurrent = false;
                    }
                    inputNodesList.Add(component);
                }
                componentsDict.Add(entry.Key, component);
                // do something with entry.Value or entry.Key
            }

            foreach (KeyValuePair<string, string[]> entry in connectionsStringsDict)
            {
                foreach(string val in entry.Value)
                {
                    componentsDict[entry.Key].outputs.Add(componentsDict[val]);
                    //Console.WriteLine("val V");
                    //Console.WriteLine(val);
                    CircuitComponent outputComp = componentsDict[val];
                    outputComp.inputs.Add(componentsDict[entry.Key]);
                }
                
                // do something with entry.Value or entry.Key
            }





            Console.WriteLine("camehere");
            Circuit circuit = new Circuit(inputNodesList);
            return circuit;
        }
    }
}