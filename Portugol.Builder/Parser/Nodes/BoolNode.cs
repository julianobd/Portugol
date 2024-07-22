using Portugol.Builder.Lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portugol.Builder.Parser.Nodes
{
    public class BoolNode : Node
    {
        public Token Token { get; }
        public bool Value => Token.Value == "verdadeiro";

        public BoolNode(Token token)
        {
            Token = token;
        }
    }
}
