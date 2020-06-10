using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public abstract class Gate : CircuitComponent
    {

        protected void NotifyStateChanged()
        {
            if(base.outputs != null)
            {
                foreach(var nextGate in base.outputs)
                {
                    nextGate.CalculateState();
                }
            }
        }
    }
}