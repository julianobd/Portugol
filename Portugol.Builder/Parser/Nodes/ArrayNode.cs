using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Portugol.Builder.Parser.Nodes
{
    public class ArrayNode : Node
    {
        public List<Node> Elements { get; }

        public ArrayNode(List<Node> elements)
        {
            Elements = elements;
        }
    }
}
