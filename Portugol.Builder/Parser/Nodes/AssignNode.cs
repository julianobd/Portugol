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
