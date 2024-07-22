// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Text.RegularExpressions;

namespace Portugol.Builder.Lexer
{
	public class LexerToken
	{
		private static readonly Regex TokenRegex = new(@"\d+|""[^""]*""|[(){}[\];+\-*\/%=<>!,]|senao|enquanto|para|foreach|se|em|escreve|lerlinha|num|txt|bol|verdadeiro|falso|[a-zA-Z_][a-zA-Z0-9_]*", RegexOptions.Compiled);
		private readonly string _text;
		private int _pos;

		public LexerToken(string text) => _text = text;

		public Token GetNextToken()
		{
			while (_pos < _text.Length && char.IsWhiteSpace(_text[_pos]))
			{
				_pos++;
			}

			if (_pos >= _text.Length)
			{
				return new Token(TokenType.EOF, "");
			}

			var match = TokenRegex.Match(_text, _pos);
			if (!match.Success || match.Index != _pos)
			{
				throw new Exception($"Invalid character at position {_pos} [{match.Value}]");
			}

			_pos += match.Length;
			var value = match.Value;

			return value switch
			{
				"se" => new Token(TokenType.If, value),
				"senao" => new Token(TokenType.Else, value),
				"enquanto" => new Token(TokenType.While, value),
				"para" => new Token(TokenType.For, value),
				"foreach" => new Token(TokenType.Foreach, value),
				"em" => new Token(TokenType.In, value),
				"escreve" => new Token(TokenType.Print, value),
				"lerlinha" => new Token(TokenType.ReadLine, value),
				"num" => new Token(TokenType.Num, value),
				"txt" => new Token(TokenType.Txt, value),
				"bol" => new Token(TokenType.Bol, value),
				"verdadeiro" => new Token(TokenType.BoolTrue, value),
				"falso" => new Token(TokenType.BoolFalse, value),
				"{" => new Token(TokenType.LeftBrace, value),
				"}" => new Token(TokenType.RightBrace, value),
				";" => new Token(TokenType.Semicolon, value),
				"," => new Token(TokenType.Comma, value),
				"+" => new Token(TokenType.Plus, value),
				"-" => new Token(TokenType.Minus, value),
				"*" => new Token(TokenType.Multiply, value),
				"/" => new Token(TokenType.Divide, value),
				"%" => new Token(TokenType.Modulus, value),
				"(" => new Token(TokenType.LeftParen, value),
				")" => new Token(TokenType.RightParen, value),
				"[" => new Token(TokenType.LeftBracket, value),
				"]" => new Token(TokenType.RightBracket, value),
				"==" => new Token(TokenType.Equal, value),
				"!=" => new Token(TokenType.NotEqual, value),
				"<" => new Token(TokenType.LessThan, value),
				">" => new Token(TokenType.GreaterThan, value),
				"<=" => new Token(TokenType.LessEqual, value),
				">=" => new Token(TokenType.GreaterEqual, value),
				"=" => new Token(TokenType.Assign, value),
				_ when value.StartsWith("\"") && value.EndsWith("\"") => new Token(TokenType.String, value[1..^1]),
				_ when int.TryParse(value, out _) => new Token(TokenType.Number, value),
				_ when char.IsLetter(value[0]) => new Token(TokenType.Identifier, value),
				_ => throw new Exception($"Invalid character at position {_pos}"),
			};
		}
	}
}
