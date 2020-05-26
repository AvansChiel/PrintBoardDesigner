using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public class InputNode : Node
    {
        public override void CalculateState()
        {
            foreach(var next in this.outputs)
            {
                next.CalculateState();
            }
        }
    }
}