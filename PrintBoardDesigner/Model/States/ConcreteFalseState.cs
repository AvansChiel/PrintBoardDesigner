using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public class ConcreteFalseState : State
    {
        public override void Handle(InputNode context)
        {
            context.state = States.STATE_TRUE;
            context.State = new ConcreteTrueState();
        }   
    }
}