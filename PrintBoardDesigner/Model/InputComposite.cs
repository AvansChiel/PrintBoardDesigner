using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public class InputComposite : Node
    {

        private List<Node> _children;


        public InputComposite()
        {
            this._children = new List<Node>();
        }

        public override void Add(Node child)
        {
            this._children.Add(child);
        }

        public override void Remove(Node child)
        {
            this._children.Remove(child);
        }

        public override void Activate()
        {
            foreach (Node child in _children)
            {
                child.Activate();
            }
        }

        public List<Node> GetChildren()
        {
            return this._children;
        }

        public override void CalculateState()
        {
            throw new Exception("Can not calculate state on composite");
        }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public static String Key
        {
            get
            {
                return "INPUTCOMPOSITE";
            }
        }
    }
}