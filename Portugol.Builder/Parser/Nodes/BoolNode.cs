// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Portugol.Builder.Lexer;

namespace Portugol.Builder.Parser.Nodes
{
	public class BoolNode : Node
	{
		public Token Token { get; }
		public bool Value => Token.Value == "verdadeiro";

		public BoolNode(Token token) => Token = token;
	}
}
