// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Portugol.Builder.Lexer;

namespace Portugol.Builder.Parser.Nodes
{
	public class VariableNode : Node
	{
		public Token Token { get; }
		public string Name => Token.Value;

		public VariableNode(Token token) => Token = token;
	}
}
