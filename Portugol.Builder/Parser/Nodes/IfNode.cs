using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portugol.Builder.Parser.Nodes
{
    public class IfNode : Node
    {
        public Node Condition { get; }
        public Node ThenBranch { get; }
        public Node ElseBranch { get; }

        public IfNode(Node condition, Node thenBranch, Node elseBranch = null)
        {
            Condition = condition;
            ThenBranch = thenBranch;
            ElseBranch = elseBranch;
        }
    }
}
