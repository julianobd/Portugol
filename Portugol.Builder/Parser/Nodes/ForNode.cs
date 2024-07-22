// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Portugol.Builder.Parser.Nodes
{
	public class ForNode : Node
	{
		public Node Initializer { get; }
		public Node Condition { get; }
		public Node Iterator { get; }
		public Node Body { get; }

		public ForNode(Node initializer, Node condition, Node iterator, Node body)
		{
			Initializer = initializer;
			Condition = condition;
			Iterator = iterator;
			Body = body;
		}
	}
}
