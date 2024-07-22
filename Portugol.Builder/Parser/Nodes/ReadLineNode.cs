// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Portugol.Builder.Parser.Nodes
{
	public class ReadLineNode : Node
	{
		public VariableNode Variable { get; }

		public ReadLineNode(VariableNode variable) => Variable = variable;
	}
}
