using Portugol.Builder.Lexer;

namespace Portugol.Builder.Parser.Nodes
{
    public class StringNode : Node
    {
        public Token Token { get; }
        public string Value => Token.Value;

        public StringNode(Token token) => Token = token;
    }
}
