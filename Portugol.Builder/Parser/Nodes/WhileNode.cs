using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Portugol.Builder.Parser.Nodes
{
    public class WhileNode : Node
    {
        public Node Condition { get; }
        public Node Body { get; }

        public WhileNode(Node condition, Node body)
        {
            Condition = condition;
            Body = body;
        }
    }
}
