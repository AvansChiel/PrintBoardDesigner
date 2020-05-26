using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public class NotGateDecorator : GateDecorator
    {
        public NotGateDecorator(Gate decoratedComponent) : base(decoratedComponent)
        {
            this.decoratedComponent = decoratedComponent;
        }

        public override void CalculateState()
        {
            if(this.decoratedComponent.inputs != null && this.decoratedComponent.inputs.Count == 1)
            {
    
                var tempInput = this.decoratedComponent.inputs[0].state;
                if (tempInput == States.STATE_UNDEFINED)
                {
                    return;
                }
                this.decoratedComponent.inputs = new List<CircuitComponent>();

                var prevComp = new NotGateDecorator(new NandGate());
                prevComp.state = tempInput;
                this.decoratedComponent.inputs.Add(prevComp);
                this.decoratedComponent.inputs.Add(prevComp);

                base.CalculateState();

            }
        }
    }
}