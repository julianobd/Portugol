using Portugol.Builder.Lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portugol.Builder.Parser.Nodes
{
    public class NumberNode : Node
    {
        public Token Token { get; }
        public int Value => int.Parse(Token.Value);

        public NumberNode(Token token)
        {
            Token = token;
        }
    }
}
