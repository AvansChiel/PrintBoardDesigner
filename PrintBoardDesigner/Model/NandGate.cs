using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public class NandGate : Gate
    {

        public NandGate()
        {
            minInputs = 2;
        }

        public override void CalculateState()
        {
            ///check if gate has at least the minimum of 2 inputs
            if(this.inputs.Count >= 2)
            {
                ///determine first inputs state (voltage)
                var state = (int)this.inputs[0].state;
                ///return if undefined
                if(state == 2)
                {
                    return;
                }
                
                
                for(var i = 1; i < this.inputs.Count; i++)
                {
                    ///return if on of the inputs is undefined
                    if ((int)this.inputs[i].state == 2)
                    {
                        return;
                    }
                    ///compare first input to all other inputs
                    state = state * (int)this.inputs[i].state;
                }

                ///if result of formula is 0, resulting state is true (high voltage)
                if(state == 0)
                {
                    //this.Accept(new HighVoltageVisitor());
                    this.state = States.STATE_TRUE;
                }
                ///if result of formula is 1, resulting state is false (low voltage)
                else if (state == 1)
                {
                    this.state = States.STATE_FALSE;
                    //this.Accept(new LowVoltageVisitor());
                }

            }
        }
        public static String Key
        {
            get
            {
                return "NAND";
            }
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}