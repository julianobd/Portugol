// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using Portugol.Builder.Lexer;
using Portugol.Builder.Parser.Nodes;

namespace Portugol.Builder.Parser
{
	public class Parser
	{
		private readonly LexerToken _lexer;
		private Token _currentToken;

		public Parser(LexerToken lexer)
		{
			_lexer = lexer;
			_currentToken = _lexer.GetNextToken();
		}

		private void Eat(List<TokenType> tokenType) => _currentToken = tokenType.Contains(_currentToken.Type) ? _lexer.GetNextToken() : throw new Exception($"Unexpected token: {_currentToken}");

		private void Eat(TokenType tokenType) => _currentToken = _currentToken.Type == tokenType ? _lexer.GetNextToken() : throw new Exception($"Unexpected token: {_currentToken}");

		private Node Factor()
		{
			var token = _currentToken;
			if (token.Type == TokenType.Number)
			{
				Eat(TokenType.Number);
				return new NumberNode(token);
			}
			else if (token.Type is TokenType.BoolTrue or TokenType.BoolFalse)
			{
				Eat(token.Type);
				return new BoolNode(token);
			}
			else if (token.Type == TokenType.Identifier)
			{
				VariableNode variable = new(token);
				Eat(TokenType.Identifier);
				return variable;
			}
			else if (token.Type == TokenType.LeftParen)
			{
				Eat(TokenType.LeftParen);
				var node = Expr();
				Eat(TokenType.RightParen);
				return node;
			}
			else if (token.Type == TokenType.LeftBracket)
			{
				return Array();
			}
			else if (token.Type == TokenType.Txt)
			{
				Eat(TokenType.Txt);
				Eat(TokenType.Identifier);
				return new StringNode(token);
			}
			else if (token.Type == TokenType.String)
			{
				Eat(TokenType.String);
				return new StringNode(token);
			}
			throw new Exception($"Unexpected token: {_currentToken}");
		}

		private Node Term()
		{
			var node = Factor();

			while (_currentToken.Type is TokenType.Multiply or TokenType.Divide or TokenType.Modulus)
			{
				var token = _currentToken;
				if (token.Type == TokenType.Multiply)
				{
					Eat(TokenType.Multiply);
				}
				else if (token.Type == TokenType.Divide)
				{
					Eat(TokenType.Divide);
				}
				else if (token.Type == TokenType.Modulus)
				{
					Eat(TokenType.Modulus);
				}
				node = new BinOpNode((NumberNode) node, token, (NumberNode) Factor());
			}
			return node;
		}

		private Node Expr()
		{
			var node = Term();
			while (_currentToken.Type is TokenType.Plus or TokenType.Minus)
			{
				var token = _currentToken;
				if (token.Type == TokenType.Plus)
				{
					Eat(TokenType.Plus);
				}
				else if (token.Type == TokenType.Minus)
				{
					Eat(TokenType.Minus);
				}
				node = new BinOpNode(node, token, Term());
			}
			return node;
		}

		private Node Comparison()
		{
			var node = Expr();
			while (_currentToken.Type is TokenType.Equal or TokenType.NotEqual or
				   TokenType.LessThan or TokenType.GreaterThan or
				   TokenType.LessEqual or TokenType.GreaterEqual)
			{
				var token = _currentToken;
				switch (token.Type)
				{
					case TokenType.Equal:
						Eat(TokenType.Equal);
						break;
					case TokenType.NotEqual:
						Eat(TokenType.NotEqual);
						break;
					case TokenType.LessThan:
						Eat(TokenType.LessThan);
						break;
					case TokenType.GreaterThan:
						Eat(TokenType.GreaterThan);
						break;
					case TokenType.LessEqual:
						Eat(TokenType.LessEqual);
						break;
					case TokenType.GreaterEqual:
						Eat(TokenType.GreaterEqual);
						break;
				}
				node = new BinOpNode(node, token, Expr());
			}
			return node;
		}

		private Node IfStatement()
		{
			Eat(TokenType.If);
			var condition = Comparison();
			Eat(TokenType.LeftBrace);
			var thenBranch = Block();
			Eat(TokenType.RightBrace);

			Node? elseBranch = null;
			if (_currentToken.Type == TokenType.Else)
			{
				Eat(TokenType.Else);
				Eat(TokenType.LeftBrace);
				elseBranch = Block();
				Eat(TokenType.RightBrace);
			}
			return new IfNode(condition, thenBranch, elseBranch);
		}

		private Node WhileStatement()
		{
			Eat(TokenType.While);
			var condition = Comparison();
			Eat(TokenType.LeftBrace);
			var body = Block();
			Eat(TokenType.RightBrace);
			return new WhileNode(condition, body);
		}

		private Node ForStatement()
		{
			Eat(TokenType.For);
			Eat(TokenType.LeftParen);
			var initializer = Statement();
			var condition = Comparison();
			Eat(TokenType.Semicolon);
			var iterator = Statement();
			//Eat(new List<TokenType> { TokenType.Semicolon, TokenType.RightParen });
			Eat(TokenType.LeftBrace);
			//Eat(TokenType.RightParen);
			//Eat(TokenType.LeftBrace);
			var body = Block();
			Eat(TokenType.RightBrace);
			return new ForNode(initializer, condition, iterator, body);
		}

		private Node ForeachStatement()
		{
			Eat(TokenType.Foreach);
			Eat(TokenType.LeftParen);
			Eat(TokenType.Num);
			VariableNode variable = new(_currentToken);
			Eat(TokenType.Identifier);
			Eat(TokenType.In);
			var collection = Expr();
			Eat(TokenType.RightParen);
			Eat(TokenType.LeftBrace);
			var body = Block();
			Eat(TokenType.RightBrace);
			return new ForeachNode(variable, collection, body);
		}

		private Node Array()
		{
			Eat(TokenType.LeftBracket);
			List<Node> elements = [];
			while (_currentToken.Type != TokenType.RightBracket)
			{
				elements.Add(Expr());
				if (_currentToken.Type == TokenType.Comma)
				{
					Eat(TokenType.Comma);
				}
			}
			Eat(TokenType.RightBracket);
			return new ArrayNode(elements);
		}

		private Node Statement()
		{
			if (_currentToken.Type == TokenType.If)
			{
				return IfStatement();
			}
			else if (_currentToken.Type == TokenType.While)
			{
				return WhileStatement();
			}
			else if (_currentToken.Type == TokenType.For)
			{
				return ForStatement();
			}
			else if (_currentToken.Type == TokenType.Foreach)
			{
				return ForeachStatement();
			}
			else if (_currentToken.Type == TokenType.Identifier)
			{
				VariableNode variable = new(_currentToken);
				Eat(TokenType.Identifier);

				if (_currentToken.Type == TokenType.Assign)
				{
					Eat(TokenType.Assign);
					var value = Expr();
					Eat([TokenType.Semicolon, TokenType.RightParen]);
					return new AssignNode(variable, value);
				}
			}
			else if (_currentToken.Type is TokenType.Num or TokenType.Txt or TokenType.Bol)
			{
				var varType = _currentToken.Type;
				Eat(varType);

				if (_currentToken.Type == TokenType.LeftBracket)
				{
					Eat(TokenType.LeftBracket);
					Eat(TokenType.RightBracket);
					return new ArrayNode([]);
				}
				else
				{
					VariableNode variable = new(_currentToken);
					Eat(TokenType.Identifier);

					if (_currentToken.Type == TokenType.Assign)
					{
						Eat(TokenType.Assign);
						Node? value = null;
						if (varType == TokenType.Num)
						{
							value = Expr();
						}
						else if (varType == TokenType.Txt)
						{
							value = new StringNode(_currentToken);
							Eat(TokenType.String);
						}
						else if (varType == TokenType.Bol)
						{
							value = _currentToken.Type == TokenType.BoolTrue ? new BoolNode(_currentToken) : new BoolNode(_currentToken);
							Eat(_currentToken.Type);
						}
						Eat(TokenType.Semicolon);
						return new AssignNode(variable, value);
					}
				}
			}
			else if (_currentToken.Type == TokenType.Print)
			{
				Eat(TokenType.Print);
				Eat(TokenType.LeftParen);
				var expression = Expr();
				Eat(TokenType.RightParen);
				Eat(TokenType.Semicolon);
				return new PrintNode(expression);
			}
			else if (_currentToken.Type == TokenType.ReadLine)
			{
				Eat(TokenType.ReadLine);
				Eat(TokenType.LeftParen);
				VariableNode variable = new(_currentToken);
				Eat(TokenType.Identifier);
				Eat(TokenType.RightParen);
				Eat(TokenType.Semicolon);
				return new ReadLineNode(variable);
			}
			throw new Exception($"Unexpected token: {_currentToken}");
		}

		private BlockNode Block()
		{
			List<Node> statements = [];
			while (_currentToken.Type is not TokenType.RightBrace and not TokenType.EOF)
			{
				statements.Add(Statement());
			}
			return new BlockNode(statements);
		}

		public Node Parse() => Block();
	}
}
