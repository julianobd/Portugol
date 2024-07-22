// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Portugol.Builder.Lexer;

namespace Portugol.Builder.Parser.Nodes
{
	public class BinOpNode : Node
	{
		public Node Left { get; }
		public Token Op { get; }
		public Node Right { get; }

		public BinOpNode(Node left, Token op, Node right)
		{
			Left = left;
			Op = op;
			Right = right;
		}
	}
}
