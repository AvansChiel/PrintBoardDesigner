using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public abstract class CircuitComponent
    {
        public string name;
        public List<CircuitComponent> outputs;
        public List<CircuitComponent> inputs;
        public Boolean hasCurrent;
        public Char viewChar;
    }
}