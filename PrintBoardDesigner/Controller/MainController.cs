using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public class MainController
    {
        private Circuit circuit;

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
            /// Build and retrieve the Circuit using the Prepared Builder Pattern
            CircuitBuilder circuitBuilder = new CircuitBuilder();
            circuitBuilder.PrepareCircuit(fileLocation);
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