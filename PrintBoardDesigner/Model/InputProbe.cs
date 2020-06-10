using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public class InputNode : Node
    {

        public override void Activate()
        {
            this.CalculateState();
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
            foreach (var next in this.outputs)
            {
                next.CalculateState();
            }
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }



        public static String Key
        {
            get
            {
                return "INPUT";
            }
        }
    }
}