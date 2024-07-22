namespace Portugol.Builder.Parser.Nodes
{
    public class ArrayNode : Node
    {
        public List<Node> Elements { get; }

        public ArrayNode(List<Node> elements)
        {
            Elements = elements;
        }
    }
}
