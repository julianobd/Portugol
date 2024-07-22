// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Portugol.Builder.Lexer;

namespace Portugol.Builder.Parser.Nodes
{
	public class StringNode : Node
	{
		public Token Token { get; }
		public string Value => Token.Value;

		public StringNode(Token token) => Token = token;
	}
}
