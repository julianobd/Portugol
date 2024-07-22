// See https://aka.ms/new-console-template for more information


using Portugol.Builder.Interpreter;
using Portugol.Builder.Lexer;
using Portugol.Builder.Parser;

var program = @"
num i = 2;
enquanto i < 5 {
    escreve(i);
    i = i + 1;
}

para (num j = 0; j < 5; j = j + 1) {
    escreve(j);
}

num[] xxx = [1, 2, 3, 4, 5];
foreach (num n em xxx) {
    escreve(n);
}

txt nome = ""Test"";
escreve(nome);

bol condicao = verdadeiro;
se condicao {
    escreve(""A condição é verdadeira"");
} senao {
    escreve(""A condição é falsa"");
}

enquanto verdadeiro {
    escreve(""Loop infinito"");
}
";

var lexer = new LexerToken(program);
var parser = new Parser(lexer);
var tree = parser.Parse();
var interpreter = new Interpreter(tree);
interpreter.Interpret();

Console.WriteLine("Fim");
Console.ReadLine();
