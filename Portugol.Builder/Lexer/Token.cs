using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public override string ToString()
        {
            return $"Token({Type}, {Value})";
        }
    }
}
