using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portugol.Builder.Parser.Nodes
{
    public class BlockNode : Node
    {
        public List<Node> Statements { get; }

        public BlockNode(List<Node> statements)
        {
            Statements = statements;
        }
    }
}
