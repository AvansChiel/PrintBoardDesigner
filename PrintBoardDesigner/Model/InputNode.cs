using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public class InputNode : Node
    {

        public InputNode()
        {
            minInputs = 2;
            if (this.InitialState == States.STATE_TRUE)
            {
                this.State = new ConcreteTrueState();
            }
            else
            {
                this.State = new ConcreteFalseState();
            }
        }

        public override States InitialState
        {
            get
            {
                return this._initialState;
            }
            set
            {
                
                this._initialState = value;
                if(this._initialState == States.STATE_TRUE)
                {
                    this.State = new ConcreteTrueState();
                }
                else
                {
                    this.State = new ConcreteFalseState();
                }
            }
        }

        private State _state;

        // Constructor

        public InputNode(State state)
        {
            this.State = state;
        }

        // Gets or sets the state

        public State State
        {
            get { return _state; }
            set

            {
                _state = value;
               
            }
        }

        public void Request()
        {
            _state.Handle(this);
        }


        public override void Activate()
        {
            this.CalculateState();
        }

        public override void Add(Node c)
        {
            throw new NotImplementedException();
        }


        public override void Remove(Node c)
        {
            throw new NotImplementedException();
        }

        public override void CalculateState()
        {
            foreach (var next in this.outputs)
            {
                next.CalculateState();
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
                return "INPUT";
            }
        }
    }
}