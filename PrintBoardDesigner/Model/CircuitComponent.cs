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
        public States state;
        public Char viewChar;

        public CircuitComponent()
        {
            this.outputs = new List<CircuitComponent>();
            this.inputs = new List<CircuitComponent>();
        }

        public abstract void CalculateState();

    }
}