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
            this.AddComponentType("OR", typeof(OrGateDecorator));
            this.AddComponentType("AND", typeof(AndGateDecorator));
            this.AddComponentType("NOT", typeof(NotGateDecorator));
        }

        public void AddComponentType(string name, Type type)
        {
            _types[name] = type;
        }

        public CircuitComponent CreateCircuitComponent(string name, string type)
        {
            Type t = _types[type];
            if (type == "INPUT" || type == "PROBE" )
            {

                CircuitComponent c = (CircuitComponent)Activator.CreateInstance(t);
                c.name = name;
                return c;
            }
            else
            {
                CircuitComponent c = (CircuitComponent)Activator.CreateInstance(t, new NandGate());
                c.name = name;
                return c;
            }
        }

        public bool DictionaryHasType(string type)
        {
            return _types.ContainsKey(type);
        }

    }
}