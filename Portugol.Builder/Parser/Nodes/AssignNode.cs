// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

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
