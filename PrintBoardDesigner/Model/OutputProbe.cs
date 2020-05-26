using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public class Probe : Node
    {
        public override void CalculateState()
        {
            this.state = this.inputs[0].state;
        }
    }
}