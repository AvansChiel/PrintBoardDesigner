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
            minInputs = 1;
        }

        public override void CalculateState()
        {
            ///not gate consists of single nandgate with a single input voltage
            if(this.decoratedComponent.inputs != null && this.decoratedComponent.inputs.Count == 1)
            {
                
                ///take only 1 input
                var tempInput = this.decoratedComponent.inputs[0].state;
                if (tempInput == States.STATE_UNDEFINED)
                {
                    return;
                }
                ///temporarily clear inputs
                this.decoratedComponent.inputs = new List<CircuitComponent>();
                ///simulate input component
                var prevComp = new NotGateDecorator(new NandGate());
                ///set the temporary state for simulated component
                prevComp.state = tempInput;
                ///set the input as the same component for both inputs of nandgate
                this.decoratedComponent.inputs.Add(prevComp);
                this.decoratedComponent.inputs.Add(prevComp);
                ///calculate ultimate state
                base.CalculateState();

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
                return "NOT";
            }
        }
    }
}