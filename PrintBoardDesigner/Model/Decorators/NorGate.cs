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
            minInputs = 2;
        }

        public override void CalculateState()
        {

            var results = new List<int>();
            ///for each input for nor gate
            foreach(var prev in this.decoratedComponent.inputs)
            {
                if (prev.state == States.STATE_UNDEFINED)
                {
                    return;
                }

                ///create nandgate
                var comp = new NandGate();
                ///set inputs for nandgate
                comp.inputs.Add(prev);
                comp.inputs.Add(prev);
                ///calculate this nandgate result state
                comp.CalculateState();
                ///save result in results array for later use
                results.Add((int)comp.state);
            }

            ///create following nandgate
            var secComp = new NandGate();
            ///loop through results from input nandgates
            foreach(var result in results)
            {
                ///simulate inputs for next gate
                var prevComp = new NotGateDecorator(new NandGate());
                prevComp.state = (States)result;
                ///add inputs to next gate
                secComp.inputs.Add(prevComp);
            }

            ///calculate next gate
            secComp.CalculateState();

            ///set the previous result as input for the last gate, both inputs should be the same
            this.decoratedComponent.inputs = new List<CircuitComponent>();
            this.decoratedComponent.inputs.Add(secComp);
            this.decoratedComponent.inputs.Add(secComp);
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
                return "NOR";
            }
        }
    }
}