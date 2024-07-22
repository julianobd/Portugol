namespace Portugol.Builder.Parser.Nodes
{
    public class ListNode : Node
    {
        public List<Node> Elements { get; }

        public ListNode(List<Node> elements) => Elements = elements;
    }
}
