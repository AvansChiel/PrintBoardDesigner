using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public abstract class GateDecorator : Gate
    {

        protected Gate decoratedComponent;

        protected GateDecorator(Gate decoratedComponent)
        {
            this.decoratedComponent = decoratedComponent;
            this.decoratedComponent.inputs = this.inputs;
            this.decoratedComponent.outputs = this.outputs;
        }

        public override void CalculateState()
        {
            if (decoratedComponent != null)
            {
                decoratedComponent.CalculateState();
                this.state = decoratedComponent.state;

                //restore orig
                this.decoratedComponent.inputs = this.inputs;

                //notify statechange
                base.NotifyStateChanged();
            }
        }
    }
}