using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public abstract class CircuitComponent
    {
        public string name;
        public List<CircuitComponent> outputs;
        public List<CircuitComponent> inputs;
        public States initialState;
        public States state;

        public abstract void Accept(IVisitor visitor);


        public CircuitComponent()
        {
            this.outputs = new List<CircuitComponent>();
            this.inputs = new List<CircuitComponent>();
            this.initialState = States.STATE_UNDEFINED;
        }

        public abstract void CalculateState();

    }
}