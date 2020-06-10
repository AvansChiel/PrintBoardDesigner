using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public class ResetVisitor :  IVisitor
    {
        public void Visit(CircuitComponent element)
        {
            element.state = element.initialState;
        }
    }
}