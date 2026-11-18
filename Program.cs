foreach (Token token in Tokenize("7+42\n105+8"))
{
    Console.WriteLine(token);
}

// ---------------------------------------------------- Lexer ----------------------------------------------------------
static List<Token> Tokenize(string source)
{
    var tokens = new List<Token>();
    int ligne = 1;
    int debutDeLigne = 0;

    for (int i = 0; i < source.Length; i++)
    {
        if (source[i] == '+')
        {
            tokens.Add(new Token(TokenType.Plus, "+", Ligne: ligne, Colonne: i - debutDeLigne + 1));
        }
        else if (EstUnChiffre(source[i]))
        {
            int debut = i;

            while (i + 1 < source.Length && EstUnChiffre(source[i + 1]))
            {
                i++;
            }

            string texte = source[debut..(i + 1)];
            tokens.Add(new Token(TokenType.Nombre, texte, Ligne: ligne, Colonne: debut - debutDeLigne + 1));
        } else if (source[i] == '\n')
        {
            tokens.Add(new Token(TokenType.FinDeLigne, "\n", Ligne: ligne, Colonne: i - debutDeLigne + 1));
            ligne++;
            debutDeLigne = i + 1;
        }
    }

    return tokens;
}

static bool EstUnChiffre(char caractere)
{
    return caractere >= '0' && caractere <= '9';
}
// ---------------------------------------------------------------------------------------------------------------------

enum TokenType
{
    Plus,
    Nombre,
    FinDeLigne,
    Balance
}

record Token(TokenType Type, string Texte, int Ligne, int Colonne);
