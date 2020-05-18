using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public class Circuit
    {
        public List<InputNode> inputNodes;
        public Queue<CircuitComponent> currentQueue;
        public Queue<CircuitComponent> nextQueue;

        public CircuitComponent CircuitComponent
        {
            get => default(CircuitComponent);
            set
            {
            }
        }

        public void ProcessQueue()
        {
            //begin tick
            //move nextQueue to currentQueue
            //process items in queue
            //end tick
        }
    }
}