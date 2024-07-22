using Portugol.Builder.Lexer;
using Portugol.Builder.Parser.Nodes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Portugol.Builder.Interpreter
{
    public class Interpreter
    {
        private readonly Node _tree;
        private readonly Dictionary<string, object> _variables = new Dictionary<string, object>();

        public Interpreter(Node tree)
        {
            _tree = tree;
        }

        private object Visit(Node node)
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
                    if (_variables.ContainsKey(v.Name))
                        return _variables[v.Name];
                    throw new Exception($"Variável não definida: {v.Name}");
                case AssignNode a:
                    var value = Visit(a.Value);
                    _variables[a.Variable.Name] = value;
                    return value;
                case PrintNode p:
                    var exprValue = Visit(p.Expression);
                    Console.WriteLine(exprValue);
                    return null;
                case ReadLineNode r:
                    var input = Console.ReadLine();
                    if (_variables.ContainsKey(r.Variable.Name))
                    {
                        var varType = _variables[r.Variable.Name].GetType();
                        if (varType == typeof(int))
                        {
                            if (int.TryParse(input, out var intValue))
                                _variables[r.Variable.Name] = intValue;
                            else
                                throw new Exception($"Entrada inválida: {input}");
                        }
                        else if (varType == typeof(string))
                        {
                            _variables[r.Variable.Name] = input;
                        }
                        else if (varType == typeof(bool))
                        {
                            if (bool.TryParse(input, out var boolValue))
                                _variables[r.Variable.Name] = boolValue;
                            else
                                throw new Exception($"Entrada inválida: {input}");
                        }
                    }
                    return null;
                case BlockNode b:
                    foreach (var statement in b.Statements)
                    {
                        Visit(statement);
                    }
                    return null;
                case IfNode i:
                    var conditionValue = (bool)Visit(i.Condition);
                    if (conditionValue)
                    {
                        Visit(i.ThenBranch);
                    }
                    else if (i.ElseBranch != null)
                    {
                        Visit(i.ElseBranch);
                    }
                    return null;
                case WhileNode w:
                    while ((bool)Visit(w.Condition))
                    {
                        Visit(w.Body);
                    }
                    return null;
                case ForNode f:
                    for (Visit(f.Initializer); (bool)Visit(f.Condition); Visit(f.Iterator))
                    {
                        Visit(f.Body);
                    }
                    return null;
                case ForeachNode e:
                    var collection = Visit(e.Collection) as List<object>;
                    var index = 0;
                    foreach (var item in collection)
                    {
                        _variables[e.Variable.Name] = item;
                        Visit(e.Body);
                        index++;
                    }
                    return null;
                case ArrayNode a:
                    var elements = new List<object>();
                    foreach (var element in a.Elements)
                    {
                        elements.Add(Visit(element));
                    }
                    return elements;
                case BinOpNode b:
                    return b.Op.Type switch
                    {
                        TokenType.Plus => (int)Visit(b.Left) + (int)Visit(b.Right),
                        TokenType.Minus => (int)Visit(b.Left) - (int)Visit(b.Right),
                        TokenType.Multiply => (int)Visit(b.Left) * (int)Visit(b.Right),
                        TokenType.Divide => (int)Visit(b.Left) / (int)Visit(b.Right),
                        TokenType.Equal => Visit(b.Left) == Visit(b.Right) ? true : false,
                        TokenType.NotEqual => Visit(b.Left) != Visit(b.Right) ? true : false,
                        TokenType.LessThan => (int)Visit(b.Left) < (int)Visit(b.Right) ? true : false,
                        TokenType.GreaterThan => (int)Visit(b.Left) > (int)Visit(b.Right) ? true : false,
                        TokenType.LessEqual => (int)Visit(b.Left) <= (int)Visit(b.Right) ? true : false,
                        TokenType.GreaterEqual => (int)Visit(b.Left) >= (int)Visit(b.Right) ? true : false,
                        _ => throw new Exception($"Invalid operator {b.Op}")
                    };
                default:
                    throw new Exception($"Nodo inválido {node}");
            }
        }

        public void Interpret()
        {
            Visit(_tree);
        }
    }

}
