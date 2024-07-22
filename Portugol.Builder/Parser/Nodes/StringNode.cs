using Portugol.Builder.Lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portugol.Builder.Parser.Nodes
{
    public class StringNode : Node
    {
        public Token Token { get; }
        public string Value => Token.Value;

        public StringNode(Token token)
        {
            Token = token;
        }
    }
}
