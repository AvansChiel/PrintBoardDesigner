using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public class MainController
    {

        private CircuitParser circuitParser;
        private GuiController guiController;
        private Circuit circuit;

        public MainController()
        {
            this.CircuitParser = new CircuitParser();
        }


        public GuiController GuiController
        {
            get { return guiController; }
            set { guiController = value; }
        }

        public CircuitParser CircuitParser
        {
            get { return circuitParser; }
            set { circuitParser = value; }
        }

        public Circuit Circuit
        {
            get { return circuit; }
            set { circuit = value; }
        }

    
    }
}