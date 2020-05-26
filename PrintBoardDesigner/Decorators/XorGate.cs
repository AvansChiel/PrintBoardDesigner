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
            this.decoratedComponent = decoratedComponent;

        }

        public override void CalculateState()
        {
            throw new NotImplementedException();
        }
    }
}