using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public abstract class CircuitComponent
    {
        public string name;
        public int minInputs;
        public List<CircuitComponent> outputs;
        public List<CircuitComponent> inputs;
        protected States _initialState;
        public States state;

        virtual public States InitialState
        {
            get
            {
                return this._initialState;
            }
            set
            {
                this._initialState = value;
            }
        }

        public abstract void Accept(IVisitor visitor);


        public CircuitComponent()
        {
            this.outputs = new List<CircuitComponent>();
            this.inputs = new List<CircuitComponent>();
            this.InitialState = States.STATE_UNDEFINED;
        }

        public abstract void CalculateState();

    }
}