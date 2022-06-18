using System;
using System.Collections.Generic;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    class Tela
    {
        public static void ImprimirPartida(PartidaDeXadrez Partida)
        {
            ImprimirTabuleiro(Partida.Tab);
            Console.WriteLine();
            ImprimirPecasCapturadas(Partida);
            Console.WriteLine();
            Console.WriteLine("Turno: " + Partida.Turno);
            Console.WriteLine("Jogam as " + Partida.JogadorAtual + "s");
            if (Partida.Xeque)
            {
                Console.WriteLine("Xeque!");
            }
        }

        public static void ImprimirPecasCapturadas(PartidaDeXadrez Partida)
        {
            Console.WriteLine("Peças capturadas: ");
            Console.Write("Brancas: ");
            ImprimirConjunto(Partida.PecasCapturadas(Cor.Branca));
            Console.WriteLine();
            Console.Write("Pretas: ");
            ImprimirConjunto(Partida.PecasCapturadas(Cor.Preta));
            Console.WriteLine();
        }

        public static void ImprimirConjunto(HashSet<Peca> conjunto)
        {
            foreach (Peca i in conjunto)
            {
                if (i.Cor == Cor.Branca)
                {
                    Colorir(i + " ", ConsoleColor.Cyan);
                }
                else
                {
                    Colorir(i + " ", ConsoleColor.Yellow);
                }
            }
        }

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
