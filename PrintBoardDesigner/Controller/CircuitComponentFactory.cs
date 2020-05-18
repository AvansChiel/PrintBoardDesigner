using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public class CircuitComponentFactory
    {

        private Dictionary<string, Type> _types = new Dictionary<string, Type>();
        public CircuitComponentFactory()
        {
           // _types = new Dictionary<string, Type>();

            this.AddComponentType("INPUT", typeof(InputNode));
            this.AddComponentType("PROBE", typeof(Probe));
            this.AddComponentType("OR", typeof(OrGate));
            this.AddComponentType("AND", typeof(AndGate));
            this.AddComponentType("NOT", typeof(NotGate));
        }

        public void AddComponentType(string name, Type type)
        {
            _types[name] = type;
        }

        public CircuitComponent CreateCircuitComponent(string type)
        {
            Type t = _types[type];
            CircuitComponent c = (CircuitComponent)Activator.CreateInstance(t);
            return c;
        }

        public bool DictionaryHasType(string type)
        {
            return _types.ContainsKey(type);
        }

    }
}