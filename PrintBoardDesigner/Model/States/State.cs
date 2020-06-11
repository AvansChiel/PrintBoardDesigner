using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public abstract class State
    {
        public abstract void Handle(InputNode context);
    }
}