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
            if(ContainsDisconnectedComponent(componentsDict))
            {
                throw new ArgumentException("File contains disconnected components");
            }

            CheckForInifinteLoop(inputNodesList);

            InputComposite inputComposite = new InputComposite();
            foreach(Node node in inputNodesList)
            {
                inputComposite.Add(node);
            }

            Circuit circuit = new Circuit(inputComposite);

            this.preparedCircuit = circuit;
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
                    throw new ArgumentException("File contains inifinte loop");
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

        private bool ContainsDisconnectedComponent(Dictionary<string, CircuitComponent> componentsDict)
        {
            foreach(KeyValuePair<string, CircuitComponent> entry in componentsDict)
            {
                var component = entry.Value;
                var id = entry.Key;
                /// Check for Dead End
                if (component.outputs.Count <= 0)
                {
                    if (component.GetType() != typeof(Probe))
                    {
                        return true;
                    }
                }
                /// Check for Dead Start
                if(component.inputs.Count <= 0)
                {
                    if(component.GetType() != typeof(InputNode))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //private void ValidateCircuit()
        //{
        //    List<CircuitComponent> nodes = new List<CircuitComponent>();
        //    foreach (var node in preparedCircuit.InputNodes)
        //    {
        //        CircuitComponent inputNode = node;
        //        nodes.Add(inputNode);

        //        var currentNode = inputNode.outputs[0];
        //        while (currentNode.outputs.Count > 0)
        //        {
        //            for (int i = 0; i < currentNode.outputs.Count; i++)
        //            {
        //                nodes.Add(currentNode);
        //                currentNode = currentNode.outputs[i];
        //            }
        //        }

        //    }
        //}

    }
}