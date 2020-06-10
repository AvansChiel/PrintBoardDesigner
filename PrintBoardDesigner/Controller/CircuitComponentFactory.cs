using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PrintBoardDesigner
{
    public class CircuitComponentFactory
    {

        private Dictionary<string, Type> _types = new Dictionary<string, Type>();
        public CircuitComponentFactory()
        {
            this.Initialize();
        }

        public void AddComponentType(string name, Type type)
        {
            _types[name] = type;
        }

        public CircuitComponent CreateCircuitComponent(string name, string type)
        {
            if(!DictionaryHasType(type))
            {
                throw new ArgumentException("Invalid CircuitComponent Type: " + type);
            }

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

        internal void Initialize()
        {
            Assembly asm = Assembly.GetExecutingAssembly();

            Type[] allTypes = asm.GetTypes();
            foreach (Type type in allTypes)
            {
                if (type.IsClass && !type.IsAbstract)
                {

                    if (type.IsSubclassOf(typeof(CircuitComponent)))
                    {

                        var info = type.GetProperty("Key").GetValue(null, null);
                        String value = info.ToString();

                        if (value != null)
                        {
                            _types[value] = type;
                        }
                    }
                }
            }
        }

    }
}