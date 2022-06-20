using System;
using tabuleiro;
using xadrez;

namespace xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                PartidaDeXadrez Partida = new PartidaDeXadrez();

                while (!Partida.Terminada)
                {
                    try
                    {
                        Console.Clear();                        
                        Tela.ImprimirPartida(Partida);

                        Console.WriteLine();
                        Console.Write("Origem: ");
                        Posicao origem = Tela.LerPosicaoXadrez().ToPosicao();
                        Partida.ValidarPosicaoDeOrigem(origem);

                        bool[,] posicoesPossiveis = Partida.Tab.Peca(origem).MovimentosPossiveis();

                        Console.Clear();
                        Tela.ImprimirTabuleiro(Partida.Tab, posicoesPossiveis);

                        Console.WriteLine();
                        Console.Write("Destino: ");
                        Posicao destino = Tela.LerPosicaoXadrez().ToPosicao();
                        Partida.ValidarPosicaoDeDestino(origem, destino);

                        Partida.RealizaJogada(origem, destino);
                    }
                    catch (TabuleiroException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Erro!");
                        Console.ReadLine();
                    }
                }
                Console.Clear();
                Tela.ImprimirPartida(Partida);
            }
            catch (TabuleiroException e)
            {
                Console.Write(e.Message);
            }
        }
    }
}
