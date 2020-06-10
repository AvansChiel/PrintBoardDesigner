using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public abstract class Node : CircuitComponent
    {
        public abstract void Add(Node c);
        public abstract void Remove(Node c);
        public abstract void Activate();
    }
}