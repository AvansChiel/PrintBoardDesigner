using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public class CircuitParser
    {
        //Default file. MAKE SURE TO CHANGE THIS LOCATION AND FILE PATH TO YOUR FILE   
        static readonly string textFile = @"C:\Users\Chiel\Documents\studie\dp\circuits\Circuit1_FullAdder.txt";

        private CircuitComponentFactory circuitComponentFactory;

        public CircuitParser()
        {
            this.circuitComponentFactory = new CircuitComponentFactory();
        }



        public CircuitComponentFactory CircuitComponentFactory
        {
            get { return circuitComponentFactory; }
        }

        public Queue<CircuitComponent> ParseFile()
        {
            Dictionary<string, string> circuitComponentDict = new Dictionary<string, string>();
            Dictionary<string, string[]> circuitConnectionDict = new Dictionary<string, string[]>();
            if (File.Exists(textFile))
            {
                // Read file using StreamReader. Reads file line by line  
                using (StreamReader file = new StreamReader(textFile))
                {
                    int counter = 0;
                    string ln;

                    while ((ln = file.ReadLine()) != null)
                    {
                        //Console.WriteLine(ln);
                        if(ln == "" || ln.Substring(0,1) == "#")
                        {
                            continue;
                        }
                        string first = ln.Substring(0, ln.IndexOf(':'));
                        string second = ln.Substring(ln.IndexOf(':')+2, (ln.IndexOf(';')-2 - (ln.IndexOf(':'))));
                        //string type;
                        //string state = "LOW";
                        ////check if second contains underscore. if so, decide state
                        //if (second.Contains("_"))
                        //{
                        //    type = second.Substring(0, second.IndexOf('_'));
                        //    int fst = second.IndexOf('_') + 1;
                        //    int scd = second.Length - 1 - second.IndexOf('_') + 1;
                        //    state = second.Substring(second.IndexOf('_') + 1, second.Length - 1 - second.IndexOf('_'));
                        //}
                        //else
                        //{
                        //    type = second;
                        //}


                        /// If not is a type, Add to connections list
                        if (!this.CircuitComponentFactory.DictionaryHasType(second))
                        {
                            string[] connList;

                            if (second.Contains(','))
                            {
                                connList = second.Split(',');
                            }
                            else
                            {
                                connList = new string[]{ second };
                            }

                            circuitConnectionDict[first] = connList;
                            
                     
                        }
                        //else parse components
                        else
                        {
                         
                            //create component
                            //CircuitComponent component = this.CircuitComponentFactory.CreateCircuitComponent(second);
                            //if(state == "HIGH")
                            //{
                            //    component.hasCurrent = true;
                            //}
                            //add component do dict key=name, value = component
                            circuitComponentDict[first] = second;

                        }

                        Console.Write(first + " ");
                        Console.Write(second);
                        Console.WriteLine("");

                        counter++;
                    }
                    file.Close();
                    Console.WriteLine($"File has {counter} lines.");
                    Console.Write(circuitComponentDict["NODE1"]);
                    Console.Write(circuitConnectionDict["NODE1"][0]);
                }
            }

            Console.ReadKey();
            return new Queue<CircuitComponent>();
        }
    }
}