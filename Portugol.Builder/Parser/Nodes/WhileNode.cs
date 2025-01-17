﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Portugol.Builder.Parser.Nodes
{
	public class WhileNode : Node
	{
		public Node Condition { get; }
		public Node Body { get; }

		public WhileNode(Node condition, Node body)
		{
			Condition = condition;
			Body = body;
		}
	}
}
