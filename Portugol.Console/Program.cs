using Portugol.Builder.Interpreter;
using Portugol.Builder.Lexer;
using Portugol.Builder.Parser;
using Portugol.Builder.Parser.Nodes;

string program = @"
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

LexerToken lexer = new(program);
Parser parser = new(lexer);
Node tree = parser.Parse();
Interpreter interpreter = new(tree);
interpreter.Interpret();

Console.WriteLine("Fim");
Console.ReadLine();
