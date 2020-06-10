using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrintBoardDesigner
{
    public class InputComposite : Node
    {

        public InputComposite()
        {
            this._children = new List<Node>();
        }

        private List<Node> _children;


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

        public List<Node> getChildren()
        {
            return this._children;
        }

        public override void CalculateState()
        {
            throw new NotImplementedException();
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