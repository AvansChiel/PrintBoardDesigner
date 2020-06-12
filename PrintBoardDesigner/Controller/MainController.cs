using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public class MainController
    {
        private Circuit circuit;
        private CircuitParser _circuitParser;
        private FileReader _fileReader;

        public MainController()
        {

        }


        public Circuit Circuit
        {
            get { return circuit; }
            set { circuit = value; }
        }


        public bool BuildCircuit(string fileLocation)
        {
            this._fileReader = new FileReader();
            this._circuitParser = new CircuitParser();

            /// Read File
            List<string> fileLines = this._fileReader.ReadFile(fileLocation);

            /// Parse File
            this._circuitParser.ParseFile(fileLines);

            /// Get Connections and Components from parser
            Dictionary<string, string> componentsStringsDict = _circuitParser.CircuitComponentDict;
            Dictionary<string, string[]> connectionsStringsDict = _circuitParser.CircuitConnectionDict;

            /// Build and retrieve the Circuit using the Prepared Builder Pattern
            CircuitBuilder circuitBuilder = new CircuitBuilder();
            circuitBuilder.PrepareCircuit(componentsStringsDict, connectionsStringsDict);
            Circuit circuit = circuitBuilder.GetPreparedCircuit();

            this.circuit = circuit;
            StartCircuitLoop();
            return true;
        }

        public void StartCircuitLoop()
        {
            this.circuit.InputComposite.Activate();
        }

    }
}