using System;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tab)
        {
            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (tab.Peca(i, j) != null)
                    {
                        ImprimirPeca(tab.Peca(i, j));
                        Console.Write(" ");
                    }
                    else
                    {
                        if (i % 2.0 == 0 && j % 2.0 == 0 || i % 2 != 0 && j % 2 != 0)
                        {
                            Colorir("- ", ConsoleColor.DarkGray);
                        }
                        else
                        {
                            Colorir("- ", ConsoleColor.White);
                        }
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
        }

        public static void ImprimirTabuleiro(Tabuleiro tab, bool[,] posicoesPossiveis)
        {
            ConsoleColor fundoOriginal = Console.BackgroundColor;
            ConsoleColor fundoAlterado = ConsoleColor.DarkCyan;

            for (int i = 0; i < tab.Linhas; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < tab.Colunas; j++)
                {
                    if (posicoesPossiveis[i, j])
                    {
                        Console.BackgroundColor = fundoAlterado;
                    }
                    else
                    {
                        Console.BackgroundColor = fundoOriginal;
                    }

                    if (tab.Peca(i, j) != null)
                    {
                        ImprimirPeca(tab.Peca(i, j));
                        Console.Write(" ");
                    }
                    else
                    {
                        if (i % 2.0 == 0 && j % 2.0 == 0 || i % 2 != 0 && j % 2 != 0)
                        {
                            Colorir("- ", ConsoleColor.DarkGray);
                        }
                        else
                        {
                            Colorir("- ", ConsoleColor.White);
                        }
                    }
                    Console.BackgroundColor = fundoOriginal;
                }
                Console.WriteLine();
            }
            Console.WriteLine("  A B C D E F G H");
        }

        public static PosicaoXadrez LerPosicaoXadrez()
        {
            string s = Console.ReadLine();
            char coluna = s[0];
            int linha = int.Parse(s[1] + "");

            return new PosicaoXadrez(coluna, linha);

        }

        public static void ImprimirPeca(Peca peca)
        {
            if (peca.Cor == Cor.Branca)
            {
                Colorir(peca, ConsoleColor.Cyan);
            }
            else
            {
                Colorir(peca, ConsoleColor.Yellow);
            }
        }

        private static void Colorir(object x, ConsoleColor consoleColor)
        {
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = consoleColor;
            Console.Write(x);
            Console.ForegroundColor = aux;
        }
    }
}
