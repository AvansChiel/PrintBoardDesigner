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
        }

        public override void CalculateState()
        {

            var inputA = this.decoratedComponent.inputs[0];
            var inputB = this.decoratedComponent.inputs[1];
            if (inputA.state == States.STATE_UNDEFINED || inputB.state == States.STATE_UNDEFINED)
            {
                return;
            }

            var firstNand = new NandGate();

            firstNand.inputs.Add(inputA);
            firstNand.inputs.Add(inputB);

            var secondNand = new NandGate();

            secondNand.inputs.Add(inputA);
            secondNand.inputs.Add(firstNand);

            var thirdNand = new NandGate();

            thirdNand.inputs.Add(firstNand);
            thirdNand.inputs.Add(inputB);
          
            firstNand.CalculateState();
            secondNand.CalculateState();
            thirdNand.CalculateState();

            this.decoratedComponent.inputs = new List<CircuitComponent>();

            this.decoratedComponent.inputs.Add(secondNand);
            this.decoratedComponent.inputs.Add(thirdNand);

            base.CalculateState();






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