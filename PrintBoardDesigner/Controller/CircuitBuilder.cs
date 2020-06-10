using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public class CircuitBuilder
    {

        private CircuitComponentFactory _circuitComponentFactory;
        private CircuitParser _circuitParser;
        private FileReader _fileReader;

        private Circuit _preparedCircuit;

        private Components _componentsObjectStructure;

        public CircuitBuilder()
        {
            this._componentsObjectStructure = new Components();
            this._circuitComponentFactory = new CircuitComponentFactory();
            this._fileReader = new FileReader();
            this._circuitParser = new CircuitParser();
        }

        public Circuit GetPreparedCircuit()
        {
            return _preparedCircuit;
        }

        public void PrepareCircuit(string fileLocation)
        {
            /// Read File
            List<string> fileLines = this._fileReader.ReadFile(fileLocation);
            /// Parse File
            this._circuitParser.ParseFile(fileLines);
            /// Get Dictionaries from parser
            Dictionary<string, string> componentsStringsDict = this._circuitParser.CircuitComponentDict;
            Dictionary<string, string[]> connectionsStringsDict = this._circuitParser.CircuitConnectionDict;

            Dictionary<string, CircuitComponent> componentsDict = new Dictionary<string, CircuitComponent>();
            List<CircuitComponent> inputNodesList = new List<CircuitComponent>();
           

            foreach (KeyValuePair<string, string> entry in componentsStringsDict)
            {
                string type = entry.Value;
                if (type.Contains("INPUT")){
                    type = "INPUT";
                }
                CircuitComponent component = this._circuitComponentFactory.CreateCircuitComponent(entry.Key, type);
                this.AddToVisitorObjectStructure(component);
                if (type == "INPUT")
                {
                    if (entry.Value.Contains("HIGH"))
                    {
                        component.state = States.STATE_TRUE;
                        component.InitialState = States.STATE_TRUE;
                    }
                    else
                    {
                        component.state = States.STATE_FALSE;
                        component.InitialState = States.STATE_FALSE;

                    }
                    inputNodesList.Add(component);
                }
                else
                {
                    component.state = States.STATE_UNDEFINED;
                }
                componentsDict.Add(entry.Key, component);
            }

            foreach (KeyValuePair<string, string[]> entry in connectionsStringsDict)
            {
                foreach(string val in entry.Value)
                {
                    if(!componentsDict.ContainsKey(val))
                    {
                        throw new ArgumentException("Invalid Circuit: Cannot create connection to or from component: " + val);
                    }
                    componentsDict[entry.Key].outputs.Add(componentsDict[val]);
                    CircuitComponent outputComp = componentsDict[val];
                    outputComp.inputs.Add(componentsDict[entry.Key]);
                }
                
            }
            CheckForInifinteLoop(inputNodesList);

            CheckForDisconnectedComponent(componentsDict);


            InputComposite inputComposite = new InputComposite();
            foreach(Node node in inputNodesList)
            {
                inputComposite.Add(node);
            }

            Circuit circuit = new Circuit(inputComposite);
            circuit.components = this._componentsObjectStructure;
            this._preparedCircuit = circuit;
        }

        private void AddToVisitorObjectStructure(CircuitComponent component)
        {
            this._componentsObjectStructure.Attach(component);
        }

        private void CheckForInifinteLoop(List<CircuitComponent> inputNodeList)
        {
            foreach (CircuitComponent inputNode in inputNodeList)
            {

                List<CircuitComponent> roundList = new List<CircuitComponent>();
                roundList.Add(inputNode);
                this.RecursivelyCheckOutputs(inputNode, roundList);
            }
        }

        private void RecursivelyCheckOutputs(CircuitComponent component, List<CircuitComponent> roundList)
        {
            foreach (CircuitComponent comp in component.outputs)
            {
                if (roundList.Contains(comp))
                {
                    throw new ArgumentException("Invalid Circuit: contains infinite loop");
                }
                List<CircuitComponent> newList = new List<CircuitComponent>();
                foreach(var item in roundList)
                {
                    newList.Add(item);
                }
                newList.Add(comp);
 
                this.RecursivelyCheckOutputs(comp, newList);
            }
        }
        private void CheckForDisconnectedComponent(Dictionary<string, CircuitComponent> componentsDict)
        {
            List<CircuitComponent> probes = new List<CircuitComponent>();

            foreach (KeyValuePair<string, CircuitComponent> entry in componentsDict)
            {

                var component = entry.Value;
                if(component.GetType() == typeof(Probe))
                {
                    probes.Add(component);
                }

            }

            foreach (var probe in probes)
            {
                RecursivelyCheckInputs(probe);
            }

        }

        private void RecursivelyCheckInputs(CircuitComponent component)
        {
            int inputs = component.inputs.Count;
            int minInputs = component.minInputs;

            if (inputs < minInputs)
            {
                var type = component.GetType().GetProperty("Key").GetValue(null, null);
                throw new ArgumentException("Invalid Circuit: " + component.name + " ("+ type.ToString() +") has " + inputs + " inputs, but needs " + minInputs + " inputs.");
            }

            foreach (CircuitComponent comp in component.inputs)
            {
                this.RecursivelyCheckInputs(comp);
            }
        }

    }
}