using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public class Circuit
    {

        private InputComposite _inputComposite;

        public Components components;

        public InputComposite InputComposite
        {
            get { return _inputComposite; }
            set { _inputComposite = value; }
        }


        public Circuit(InputComposite inputComposite)
        {
            this._inputComposite = inputComposite;
        }
       
    }
}