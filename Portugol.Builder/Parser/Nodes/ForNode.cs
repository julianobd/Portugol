using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Portugol.Builder.Parser.Nodes
{
    public class ForNode : Node
    {
        public Node Initializer { get; }
        public Node Condition { get; }
        public Node Iterator { get; }
        public Node Body { get; }

        public ForNode(Node initializer, Node condition, Node iterator, Node body)
        {
            Initializer = initializer;
            Condition = condition;
            Iterator = iterator;
            Body = body;
        }
    }
}
