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
