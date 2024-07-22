// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Portugol.Builder.Lexer;
using Portugol.Builder.Parser.Nodes;

namespace Portugol.Builder.Interpreter
{
	public class Interpreter
	{
		private readonly Node tree;
		private readonly Dictionary<string, object> variables = [];

		public Interpreter(Node tree) => this.tree = tree;

		private object? Visit(Node node)
		{
			switch (node)
			{
				case StringNode s:
					return s.Value;
				case NumberNode n:
					return n.Value;
				case BoolNode b:
					return b.Value;
				case VariableNode v:
					if (variables.ContainsKey(v.Name))
					{
						return variables[v.Name];
					}

					throw new Exception($"Variável não definida: {v.Name}");
				case AssignNode a:
					var value = Visit(a.Value);
					variables[a.Variable.Name] = value ?? throw new Exception($"Variável não definida: {a.Variable.Name}");
					return value;
				case PrintNode p:
					var exprValue = Visit(p.Expression);
					Console.WriteLine(exprValue);
					return null;
				case ReadLineNode r:
					var input = Console.ReadLine();
					if (variables.ContainsKey(r.Variable.Name))
					{
						var varType = variables[r.Variable.Name].GetType();
						if (varType == typeof(int))
						{
							variables[r.Variable.Name] = int.TryParse(input, out var intValue) ? (object) intValue : throw new Exception($"Entrada inválida: {input}");
						}
						else if (varType == typeof(string))
						{
							variables[r.Variable.Name] = input ?? throw new Exception($"Variável não definida: {r.Variable.Name}");
						}
						else if (varType == typeof(bool))
						{
							variables[r.Variable.Name] = bool.TryParse(input, out var boolValue) ? (object) boolValue : throw new Exception($"Entrada inválida: {input}");
						}
					}
					return null;
				case BlockNode b:
					foreach (var statement in b.Statements)
					{
						_ = Visit(statement);
					}
					return null;
				case IfNode i:
					var conditionValue = (bool?)Visit(i.Condition);
					if (conditionValue.GetValueOrDefault())
					{
						_ = Visit(i.ThenBranch);
					}
					else if (i.ElseBranch != null)
					{
						_ = Visit(i.ElseBranch);
					}
					return null;
				case WhileNode w:
					while (((bool?)Visit(w.Condition)).GetValueOrDefault())
					{
						_ = Visit(w.Body);
					}
					return null;
				case ForNode f:
					for (Visit(f.Initializer); ((bool?) Visit(f.Condition)).GetValueOrDefault(); Visit(f.Iterator))
					{
						_ = Visit(f.Body);
					}
					return null;
				case ForeachNode e:
					var collection = Visit(e.Collection) as List<object> ?? throw new Exception($"Variável não definida: {e.Variable.Name}");
					var index = 0;
					foreach (var item in collection)
					{
						variables[e.Variable.Name] = item;
						_ = Visit(e.Body);
						index++;
					}
					return null;
				case ArrayNode a:
					List<object> elements = [];
					foreach (var element in a.Elements)
					{
						elements.Add(Visit(element));
					}
					return elements;
				case BinOpNode b:
					return b.Op.Type switch
					{
						TokenType.Plus => (int) Visit(b.Left) + (int) Visit(b.Right),
						TokenType.Minus => (int) Visit(b.Left) - (int) Visit(b.Right),
						TokenType.Multiply => (int) Visit(b.Left) * (int) Visit(b.Right),
						TokenType.Divide => (int) Visit(b.Left) / (int) Visit(b.Right),
						TokenType.Equal => Visit(b.Left) == Visit(b.Right),
						TokenType.NotEqual => Visit(b.Left) != Visit(b.Right),
						TokenType.LessThan => (int) Visit(b.Left) < (int) Visit(b.Right),
						TokenType.GreaterThan => (int) Visit(b.Left) > (int) Visit(b.Right),
						TokenType.LessEqual => (int) Visit(b.Left) <= (int) Visit(b.Right),
						TokenType.GreaterEqual => (int) Visit(b.Left) >= (int) Visit(b.Right),
						_ => throw new Exception($"Invalid operator {b.Op}")
					};
				default:
					throw new Exception($"Nodo inválido {node}");
			}
		}

		public void Interpret() => Visit(tree);
	}
}
