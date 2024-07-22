// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Portugol.Builder.Parser.Nodes
{
	public class BlockNode : Node
	{
		public List<Node> Statements { get; }

		public BlockNode(List<Node> statements) => Statements = statements;
	}
}
