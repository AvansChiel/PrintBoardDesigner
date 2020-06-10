using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public class Components
    {
        private List<CircuitComponent> _components = new List<CircuitComponent>();

        public void Attach(CircuitComponent component)
        {
            _components.Add(component);
        }

        public void Detach(CircuitComponent component)
        {
            _components.Remove(component);
        }

        public void Accept(IVisitor visitor)
        {
            foreach (CircuitComponent e in _components)
            {
                e.Accept(visitor);
            }
            Console.WriteLine();
        }
    }
}