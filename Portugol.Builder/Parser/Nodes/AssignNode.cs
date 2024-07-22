using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portugol.Builder.Parser.Nodes
{
    public class AssignNode : Node
    {
        public VariableNode Variable { get; }
        public Node Value { get; }

        public AssignNode(VariableNode variable, Node value)
        {
            Variable = variable;
            Value = value;
        }
    }
}
