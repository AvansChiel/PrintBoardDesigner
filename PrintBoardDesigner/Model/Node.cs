using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public abstract class Gate : CircuitComponent
    {
        public abstract void CalculateState();
    }
}