// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Portugol.Builder.Lexer;

namespace Portugol.Builder.Parser.Nodes
{
	public class NumberNode : Node
	{
		public Token Token { get; }
		public int Value => int.Parse(Token.Value);

		public NumberNode(Token token) => Token = token;
	}
}
