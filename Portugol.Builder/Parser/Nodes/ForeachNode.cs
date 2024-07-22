namespace Portugol.Builder.Parser.Nodes
{
    public class ForeachNode : Node
    {
        public VariableNode Variable { get; }
        public Node Collection { get; }
        public Node Body { get; }

        public ForeachNode(VariableNode variable, Node collection, Node body)
        {
            Variable = variable;
            Collection = collection;
            Body = body;
        }
    }
}
