using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public class Circuit
    {
        //private List<CircuitComponent> inputNodes;

        private InputComposite inputComposite;

        public Components components;

        public InputComposite InputComposite
        {
            get { return inputComposite; }
            set { inputComposite = value; }
        }


        public Circuit(InputComposite inputComposite)
        {
            this.inputComposite = inputComposite;
        }
       
    }
}