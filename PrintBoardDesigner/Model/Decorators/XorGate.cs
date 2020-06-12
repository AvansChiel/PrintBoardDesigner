using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public class XorGateDecorator : GateDecorator
    {

        public XorGateDecorator(Gate decoratedComponent) : base(decoratedComponent)
        {
            minInputs = 2;
        }

        public override void CalculateState()
        {
            ///remember both inputs for later use.
            var inputA = this.decoratedComponent.inputs[0];
            var inputB = this.decoratedComponent.inputs[1];
            if (inputA.state == States.STATE_UNDEFINED || inputB.state == States.STATE_UNDEFINED)
            {
                return;
            }

            ///create first nand gate
            var firstNand = new NandGate();
            ///first gate uses both inupts
            firstNand.inputs.Add(inputA);
            firstNand.inputs.Add(inputB);

            ///second gate 
            var secondNand = new NandGate();
            ///second gate uses one of the initial inputs + first gate
            secondNand.inputs.Add(inputA);
            secondNand.inputs.Add(firstNand);

            var thirdNand = new NandGate();
            ///second gate uses one of the initial inputs + first gate
            thirdNand.inputs.Add(firstNand);
            thirdNand.inputs.Add(inputB);
          
            ///calculate states in the right order
            firstNand.CalculateState();
            secondNand.CalculateState();
            thirdNand.CalculateState();

            ///temporarily clear inputs
            this.decoratedComponent.inputs = new List<CircuitComponent>();

            ///add calculated gates as input
            this.decoratedComponent.inputs.Add(secondNand);
            this.decoratedComponent.inputs.Add(thirdNand);

            ///calculate ultimate state
            base.CalculateState();


        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public static String Key
        {
            get
            {
                return "XOR";
            }
        }
    }
}