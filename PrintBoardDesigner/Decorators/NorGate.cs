using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public class NorGateDecorator : GateDecorator
    {
        public NorGateDecorator(Gate decoratedComponent) : base(decoratedComponent)
        {
        }

        public override void CalculateState()
        {
            var results = new List<int>();

            foreach(var prev in this.decoratedComponent.inputs)
            {
                if (prev.state == States.STATE_UNDEFINED)
                {
                    return;
                }

                var comp = new NandGate();

                comp.inputs.Add(prev);
                comp.inputs.Add(prev);

                comp.CalculateState();

                results.Add((int)comp.state);
            }

            var secComp = new NandGate();
            foreach(var result in results)
            {
                var prevComp = new NotGateDecorator(new NandGate());
                prevComp.state = (States)result;
                secComp.inputs.Add(prevComp);
            }

            secComp.CalculateState();
            this.decoratedComponent.inputs = new List<CircuitComponent>();
            this.decoratedComponent.inputs.Add(secComp);
            this.decoratedComponent.inputs.Add(secComp);

            base.CalculateState();

        }
    }
}