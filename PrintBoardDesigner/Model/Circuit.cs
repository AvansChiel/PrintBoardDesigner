using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public class Circuit
    {
        private List<CircuitComponent> inputNodes;
        private Queue<CircuitComponent> currentQueue;
        private Queue<CircuitComponent> nextQueue;

        public List<CircuitComponent> InputNodes
        {
            get { return inputNodes; }
            set { inputNodes = value; }
        }

        public Queue<CircuitComponent> CurrentQueue
        {
            get { return currentQueue; }
            set { currentQueue = value; }
        }

        public Queue<CircuitComponent> NextQueue
        {
            get { return nextQueue; }
            set { nextQueue = value; }
        }

        public Circuit(List<CircuitComponent> inputNodes)
        {
            foreach(CircuitComponent node in inputNodes)
            {
                this.nextQueue.Enqueue(node);
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