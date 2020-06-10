using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public class Probe : Node
    {

        public Probe()
        {
            minInputs = 1;
        }

        public override void Activate()
        {
            throw new NotImplementedException();
        }

        public override void Add(Node c)
        {
            throw new NotImplementedException();
        }

        public override void Remove(Node c)
        {
            throw new NotImplementedException();
        }

        public override void CalculateState()
        {
            this.state = this.inputs[0].state;
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
        public static String Key
        {
            get
            {
                return "PROBE";
            }
        }
    }
}