// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

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
