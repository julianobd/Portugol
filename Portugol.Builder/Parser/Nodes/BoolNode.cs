using Portugol.Builder.Lexer;

namespace Portugol.Builder.Parser.Nodes
{
    public class BoolNode : Node
    {
        public Token Token { get; }
        public bool Value => Token.Value == "verdadeiro";

        public BoolNode(Token token) => Token = token;
    }
}
