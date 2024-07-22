using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Portugol.Builder.Parser.Nodes
{
    public class ListNode : Node
    {
        public List<Node> Elements { get; }

        public ListNode(List<Node> elements)
        {
            Elements = elements;
        }
    }
}
