using Portugol.Builder.Lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portugol.Builder.Parser.Nodes
{
    public class BinOpNode : Node
    {
        public Node Left { get; }
        public Token Op { get; }
        public Node Right { get; }

        public BinOpNode(Node left, Token op, Node right)
        {
            Left = left;
            Op = op;
            Right = right;
        }

    }
}
