// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace Portugol.Builder.Lexer
{
	public enum TokenType
	{
		Number,
		Identifier,
		Assign,
		Plus,
		Minus,
		Multiply,
		Divide,
		Modulus,
		LeftParen,
		RightParen,
		LeftBrace,
		RightBrace,
		LeftBracket,
		RightBracket,
		If,
		Else,
		While,
		For,
		Foreach,
		In,
		Print,
		ReadLine,
		Equal,
		NotEqual,
		LessThan,
		GreaterThan,
		LessEqual,
		GreaterEqual,
		Semicolon,
		Comma,
		Num,
		Txt,
		Bol,
		BoolTrue,
		BoolFalse,
		String,
		EOF
	}

	public class Token
	{
		public TokenType Type { get; }
		public string Value { get; }

		public Token(TokenType type, string value)
		{
			Type = type;
			Value = value;
		}

		public override string ToString() => $"Token({Type}, {Value})";
	}
}
