using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrintBoardDesigner;

namespace PrintBoardView.Model
{
    class CircuitComponentDrawing
    {

        private CircuitComponent component;
        private int x;
        private int y;

        public CircuitComponentDrawing(CircuitComponent component, int x, int y)
        {
            this.Component = component;
            this.X = x;
            this.Y = y;
        }

        public CircuitComponent Component
        {
            get { return this.component; }
            set { this.component = value; }
        }

        public int X
        {
            get { return this.x; }
            set { this.x = value; }
        }

        public int Y
        {
            get { return this.y; }
            set { this.y = value; }
        }
    }
}
