using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portugol.Builder.Parser.Nodes
{
    public class PrintNode : Node
    {
        public Node Expression { get; }

        public PrintNode(Node expression)
        {
            Expression = expression;
        }
    }
}
