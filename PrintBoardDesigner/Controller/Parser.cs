using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public class ParseController
    {
        public CircuitComponentFactory CircuitComponentFactory
        {
            get => default(CircuitComponentFactory);
            set
            {
            }
        }

        public Queue<CircuitComponent> ParseFile()
        {
            return new Queue<CircuitComponent>();
        }
    }
}