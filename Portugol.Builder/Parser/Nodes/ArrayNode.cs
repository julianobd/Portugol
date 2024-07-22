﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Portugol.Builder.Parser.Nodes
{
	public class ArrayNode : Node
	{
		public List<Node> Elements { get; }

		public ArrayNode(List<Node> elements) => Elements = elements;
	}
}
