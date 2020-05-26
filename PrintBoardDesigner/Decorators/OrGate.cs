using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public class OrGateDecorator : GateDecorator
    {
        public OrGateDecorator(Gate decoratedComponent) : base(decoratedComponent)
        {}

        public override void CalculateState()
        {
            var results = new List<int>();
            ///and OR gate consists of 2 NAND gates leading into 1 NAND gate
            ///create a nand gate for each input
            foreach(var prev in this.decoratedComponent.inputs)
            {
                var comp = new NandGate();
                ///inputs should both be the same for each gate
                comp.inputs.Add(prev);
                comp.inputs.Add(prev);

                ///calculate state for this gate
                comp.CalculateState();
                ///save result for later use
                results.Add((int)comp.state);
            }

            //clear list so the link does not get interupted (will get restored later in base)
            this.decoratedComponent.inputs = new List<CircuitComponent>();
            foreach(var result in results)
            {
                //create fake inputs for decorated component
                var prevComp = new NotGateDecorator(new NandGate());
                //set states
                prevComp.state = (States)result;
                //add fake inputs to inputslist
                this.decoratedComponent.inputs.Add(prevComp);
            }

            //calculate state using fake inputs
            base.CalculateState();

        }

        public void StartNextComponents()
        {
            if(this.outputs != null && this.outputs.Count > 0)
            {
                foreach(CircuitComponent output in this.outputs)
                {
                    output.CalculateState();
                }
            }
        }
    }
}