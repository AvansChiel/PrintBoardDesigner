using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public interface IVisitor
    {
        void Visit(CircuitComponent element);
    }
}