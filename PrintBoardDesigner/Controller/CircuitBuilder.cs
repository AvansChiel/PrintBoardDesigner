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
            /// Get Connections and Components from parser
            Dictionary<string, string> componentsStringsDict = _circuitParser.CircuitComponentDict;
            Dictionary<string, string[]> connectionsStringsDict = _circuitParser.CircuitConnectionDict;

            /// Initialize the dictionary used for TODO
            Dictionary<string, CircuitComponent> componentsDict = new Dictionary<string, CircuitComponent>();
            List<CircuitComponent> inputNodesList = new List<CircuitComponent>();
           
            /// Loop through the list of components
            foreach (KeyValuePair<string, string> entry in componentsStringsDict)
            {
                /// INPUT_HIGH and INPUT_LOW should become INPUT
                string type = entry.Value;
                if (type.Contains("INPUT")){
                    type = "INPUT";
                }
                CircuitComponent component = this._circuitComponentFactory.CreateCircuitComponent(entry.Key, type);
                /// Add Visitor for Reset functionality
                this.AddToVisitorObjectStructure(component);

                /// Set State depending on INPUT_HIGH or _LOW
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
                    /// Add the input node to the inputnodes list (Will be registered in Circuit)
                    inputNodesList.Add(component);
                }
                else
                {
                    component.state = States.STATE_UNDEFINED;
                }
                /// Add non-input components to the list with components, later used to add connections
                componentsDict.Add(entry.Key, component);
            }

            /// Loop through the list of components, to add connections
            foreach (KeyValuePair<string, string[]> entry in connectionsStringsDict)
            {
                foreach(string val in entry.Value)
                {
                    /// A connection cannot be made to or from an undefined component.
                    if(!componentsDict.ContainsKey(val))
                    {
                        throw new ArgumentException("Invalid Circuit: Cannot create connection to or from component: " + val);
                    }
                    componentsDict[entry.Key].outputs.Add(componentsDict[val]);
                    CircuitComponent outputComp = componentsDict[val];
                    outputComp.inputs.Add(componentsDict[entry.Key]);
                }
                
            }
            /// Validation
            CheckForInifinteLoop(inputNodesList);

            CheckForDisconnectedComponent(componentsDict);

            /// Create and add composite.
            InputComposite inputComposite = new InputComposite();
            foreach(Node node in inputNodesList)
            {
                inputComposite.Add(node);
            }

            Circuit circuit = new Circuit(inputComposite);
            circuit.components = _componentsObjectStructure;
            _preparedCircuit = circuit;
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
                /// Create new list for new branch
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