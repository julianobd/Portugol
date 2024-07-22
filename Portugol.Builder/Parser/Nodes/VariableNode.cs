using Portugol.Builder.Lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portugol.Builder.Parser.Nodes
{
    public class VariableNode : Node
    {
        public Token Token { get; }
        public string Name => Token.Value;

        public VariableNode(Token token)
        {
            Token = token;
        }
    }
}
