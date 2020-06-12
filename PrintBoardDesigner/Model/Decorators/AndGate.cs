using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public class AndGateDecorator : GateDecorator
    {

        public AndGateDecorator(Gate decoratedComponent) : base(decoratedComponent)
        {
            minInputs = 2;
        }

        public override void CalculateState()
        {
            ///and gates consists of 2 nand gates following each other
            ///create first nand gate
            var comp = new NandGate();
            ///points inputs to first nand
            foreach(var prev in this.decoratedComponent.inputs)
            {
                if(prev.state == States.STATE_UNDEFINED)
                {
                    return;
                }
                comp.inputs.Add(prev);
            }

            ///calculate first nand gate state
            comp.CalculateState();
            var result = (int)comp.state;

            ///clear second gate inputs
            this.decoratedComponent.inputs = new List<CircuitComponent>();

            ///create a dummy gate to use as input
            var prevComp = new NotGateDecorator(new NandGate());
            ///use result from first nand gate
            prevComp.state = (States)result;
            
            ///both inputs are the same
            this.decoratedComponent.inputs.Add(prevComp);
            this.decoratedComponent.inputs.Add(prevComp);
            ///calculate ultimate state
            base.CalculateState();
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        ///register key
        public static String Key
        {
            get
            {
                return "AND";
            }
        }
    }
}