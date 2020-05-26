﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public class CircuitBuilder
    {

        private CircuitComponentFactory circuitComponentFactory;
        private CircuitParser circuitParser;
        private FileReader fileReader;

        private Circuit preparedCircuit;

        public CircuitBuilder()
        {
            this.circuitComponentFactory = new CircuitComponentFactory();
            this.fileReader = new FileReader();
            this.circuitParser = new CircuitParser();
        }

        public Circuit GetPreparedCircuit()
        {
            return preparedCircuit;
        }

        //public CircuitComponentFactory CircuitComponentFactory
        //{
        //    get { return circuitComponentFactory; }
        //    set { circuitComponentFactory = value; }
        //}

        //public CircuitParser CircuitParser
        //{
        //    get { return circuitParser; }
        //    set { circuitParser = value; }
        //}

        public void PrepareCircuit(string fileLocation)
        {
            List<string> fileLines = this.fileReader.ReadFile(fileLocation);
            this.circuitParser.ParseFile(fileLines);
            Dictionary<string, string> componentsStringsDict = this.circuitParser.CircuitComponentDict;
            Dictionary<string, string[]> connectionsStringsDict = this.circuitParser.CircuitConnectionDict;

            Dictionary<string, CircuitComponent> componentsDict = new Dictionary<string, CircuitComponent>();
            List<CircuitComponent> inputNodesList = new List<CircuitComponent>();

            foreach (KeyValuePair<string, string> entry in componentsStringsDict)
            {
                string type = entry.Value;
                if (type.Contains("INPUT")){
                    type = "INPUT";
                }
                CircuitComponent component = this.circuitComponentFactory.CreateCircuitComponent(entry.Key, type);
                //substr on input nodes to create correct component
                if (type == "INPUT")
                {
                    if (entry.Value.Contains("HIGH"))
                    {
                        component.state = States.STATE_TRUE;
                    }
                    else
                    {
                        component.state = States.STATE_FALSE;
                    }
                    inputNodesList.Add(component);
                }
                else
                {
                    component.state = States.STATE_UNDEFINED;
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

            Circuit circuit = new Circuit(inputNodesList);

            this.preparedCircuit = circuit;
        }
    }
}