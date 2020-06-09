using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public class Circuit
    {
        private List<CircuitComponent> inputNodes;

        public List<CircuitComponent> InputNodes
        {
            get { return inputNodes; }
            set { inputNodes = value; }
        }


        public Circuit(List<CircuitComponent> inputNodes)
        {
            this.inputNodes = inputNodes;
        }
       
    }
}