using Portugol.Builder.Lexer;

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
