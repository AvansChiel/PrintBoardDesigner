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
        }

        public override void CalculateState()
        {
            

            var comp = new NandGate();

            foreach(var prev in this.decoratedComponent.inputs)
            {
                comp.inputs.Add(prev);
            }

            comp.CalculateState();
            var result = (int)comp.state;

            this.decoratedComponent.inputs = new List<CircuitComponent>();

            var prevComp = new NotGateDecorator(new NandGate());
            prevComp.state = (States)result;

            this.decoratedComponent.inputs.Add(prevComp);
            this.decoratedComponent.inputs.Add(prevComp);
            base.CalculateState();
        }
    }
}