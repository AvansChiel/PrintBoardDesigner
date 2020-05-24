using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public class MainController
    {
        private GuiController guiController;
        private Circuit circuit;
        private CircuitBuilder circuitBuilder;

        public MainController()
        {
            this.CircuitBuilder = new CircuitBuilder();
        }


        public GuiController GuiController
        {
            get { return guiController; }
            set { guiController = value; }
        }

        public Circuit Circuit
        {
            get { return circuit; }
            set { circuit = value; }
        }

        public CircuitBuilder CircuitBuilder
        {
            get { return circuitBuilder; }
            set { circuitBuilder = value; }
        }

        public void init()
        {
            this.circuit = circuitBuilder.buildCircuit();
        }
    }
}