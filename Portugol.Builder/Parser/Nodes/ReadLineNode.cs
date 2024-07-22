using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portugol.Builder.Parser.Nodes
{
    public class ReadLineNode : Node
    {
        public VariableNode Variable { get; }

        public ReadLineNode(VariableNode variable)
        {
            Variable = variable;
        }
    }
}
