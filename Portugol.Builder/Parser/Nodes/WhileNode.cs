﻿namespace Portugol.Builder.Parser.Nodes
{
    public class WhileNode : Node
    {
        public Node Condition { get; }
        public Node Body { get; }

        public WhileNode(Node condition, Node body)
        {
            Condition = condition;
            Body = body;
        }
    }
}
