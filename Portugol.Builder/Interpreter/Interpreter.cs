using Portugol.Builder.Lexer;
using Portugol.Builder.Parser.Nodes;

namespace Portugol.Builder.Interpreter
{
    public class Interpreter
    {
        private readonly Node _tree;
        private readonly Dictionary<string, object> _variables = [];

        public Interpreter(Node tree) => _tree = tree;

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
                    if (_variables.ContainsKey(v.Name))
                        return _variables[v.Name];
                    throw new Exception($"Variável não definida: {v.Name}");
                case AssignNode a:
                    object value = Visit(a.Value);
                    _variables[a.Variable.Name] = value;
                    return value;
                case PrintNode p:
                    object exprValue = Visit(p.Expression);
                    Console.WriteLine(exprValue);
                    return null;
                case ReadLineNode r:
                    string? input = Console.ReadLine();
                    if (_variables.ContainsKey(r.Variable.Name))
                    {
                        Type varType = _variables[r.Variable.Name].GetType();
                        if (varType == typeof(int))
                        {
                            _variables[r.Variable.Name] = int.TryParse(input, out int intValue) ? (object)intValue : throw new Exception($"Entrada inválida: {input}");
                        }
                        else if (varType == typeof(string))
                        {
                            _variables[r.Variable.Name] = input;
                        }
                        else if (varType == typeof(bool))
                        {
                            _variables[r.Variable.Name] = bool.TryParse(input, out bool boolValue) ? (object)boolValue : throw new Exception($"Entrada inválida: {input}");
                        }
                    }
                    return null;
                case BlockNode b:
                    foreach (Node statement in b.Statements)
                    {
                        _ = Visit(statement);
                    }
                    return null;
                case IfNode i:
                    bool conditionValue = (bool)Visit(i.Condition);
                    if (conditionValue)
                    {
                        _ = Visit(i.ThenBranch);
                    }
                    else if (i.ElseBranch != null)
                    {
                        _ = Visit(i.ElseBranch);
                    }
                    return null;
                case WhileNode w:
                    while ((bool)Visit(w.Condition))
                    {
                        _ = Visit(w.Body);
                    }
                    return null;
                case ForNode f:
                    for (Visit(f.Initializer); (bool)Visit(f.Condition); Visit(f.Iterator))
                    {
                        _ = Visit(f.Body);
                    }
                    return null;
                case ForeachNode e:
                    List<object>? collection = Visit(e.Collection) as List<object>;
                    int index = 0;
                    foreach (object item in collection)
                    {
                        _variables[e.Variable.Name] = item;
                        _ = Visit(e.Body);
                        index++;
                    }
                    return null;
                case ArrayNode a:
                    List<object> elements = [];
                    foreach (Node element in a.Elements)
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
                        TokenType.Equal => Visit(b.Left) == Visit(b.Right),
                        TokenType.NotEqual => Visit(b.Left) != Visit(b.Right),
                        TokenType.LessThan => (int)Visit(b.Left) < (int)Visit(b.Right),
                        TokenType.GreaterThan => (int)Visit(b.Left) > (int)Visit(b.Right),
                        TokenType.LessEqual => (int)Visit(b.Left) <= (int)Visit(b.Right),
                        TokenType.GreaterEqual => (int)Visit(b.Left) >= (int)Visit(b.Right),
                        _ => throw new Exception($"Invalid operator {b.Op}")
                    };
                default:
                    throw new Exception($"Nodo inválido {node}");
            }
        }

        public void Interpret() => Visit(_tree);
    }
}
