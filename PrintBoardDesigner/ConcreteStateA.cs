using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public class ConcreteTrueState : State
    {
        public override void Handle(InputNode context)
        {
            context.state = States.STATE_FALSE;
            context.State = new ConcreteFalseState();
        }
    }
}