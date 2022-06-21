using System;
using System.Collections.Generic;
using tabuleiro;

namespace xadrez
{
    class PartidaDeXadrez
    {
        private HashSet<Peca> Pecas;
        private HashSet<Peca> Capturadas;

        public Peca VulneravelEnPassant;
        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; set; }
        public bool Xeque { get; private set; }

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro(8, 8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            Xeque = false;
            VulneravelEnPassant = null;
            Pecas = new HashSet<Peca>();
            Capturadas = new HashSet<Peca>();
            ColocarPecas();
        }

        public Peca ExecutaMovimento(Posicao origem, Posicao destino)
        {
            Peca p = Tab.RetirarPeca(origem);
            Peca PecaCapturada = Tab.RetirarPeca(destino);
            p.IncrementarQtdMovimentos();
            Tab.ColocarPeca(p, destino);
            if (PecaCapturada != null)
            {
                Capturadas.Add(PecaCapturada);
            }

            // # jogada especial roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = Tab.RetirarPeca(origemT);
                T.IncrementarQtdMovimentos();
                Tab.ColocarPeca(T, destinoT);
            }

            // # jogada especial roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = Tab.RetirarPeca(origemT);
                T.IncrementarQtdMovimentos();
                Tab.ColocarPeca(T, destinoT);
            }

            // # jogada especial en passant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && PecaCapturada == null)
                    {
                    Posicao posP;
                    if (p.Cor == Cor.Branca)
                    {
                        posP = new Posicao(destino.Linha + 1, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(destino.Linha - 1, destino.Coluna);
                    }
                    PecaCapturada = Tab.RetirarPeca(posP);
                    Capturadas.Add(PecaCapturada);
                }
            }

            return PecaCapturada;
        }

        public void DesfazMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {
            Peca p = Tab.RetirarPeca(destino);
            p.DecrementarQtdMovimentos();
            if (pecaCapturada != null)
            {
                Tab.ColocarPeca(pecaCapturada, destino);
                Capturadas.Remove(pecaCapturada);
            }
            Tab.ColocarPeca(p, origem);

            // # jogada especial roque pequeno
            if (p is Rei && destino.Coluna == origem.Coluna + 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
                Peca T = Tab.RetirarPeca(destinoT);
                T.DecrementarQtdMovimentos();
                Tab.ColocarPeca(T, origemT);
            }

            // # jogada especial roque grande
            if (p is Rei && destino.Coluna == origem.Coluna - 2)
            {
                Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
                Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
                Peca T = Tab.RetirarPeca(destinoT);
                T.DecrementarQtdMovimentos();
                Tab.ColocarPeca(T, origemT);
            }

            // # jogada especial en passant
            if (p is Peao)
            {
                if (origem.Coluna != destino.Coluna && pecaCapturada == VulneravelEnPassant)
                {
                    Peca peao = Tab.RetirarPeca(destino);
                    Posicao posP;
                    if (peao.Cor == Cor.Branca)
                    {
                        posP = new Posicao(3, destino.Coluna);
                    }
                    else
                    {
                        posP = new Posicao(4, destino.Coluna);
                    }
                    Tab.ColocarPeca(peao, posP);
                }
            }
        }

        public void RealizaJogada(Posicao origem, Posicao destino)
        {
            Peca pecaCapturada = ExecutaMovimento(origem, destino);

            if (EstaEmXeque(JogadorAtual))
            {
                DesfazMovimento(origem, destino, pecaCapturada);
                throw new TabuleiroException("Você não pode se colocar em xeque");
            }

            Peca p = Tab.Peca(destino);

            // # jogada especial promocao
            if (p is Peao)
            {
                if (p.Cor == Cor.Branca && destino.Linha == 0 || p.Cor == Cor.Preta && destino.Linha == 7)
                {
                    p = Tab.RetirarPeca(destino);
                    Pecas.Remove(p);
                    Console.WriteLine();
                    Console.Write("Promoção, digite a peça (Q, N, R, B): ");
                    char peca = char.Parse(Console.ReadLine());
                    Console.WriteLine();
                    Peca nova;
                    switch (peca)
                    {
                        case 'Q':
                            nova = new Dama(Tab, p.Cor);
                            Tab.ColocarPeca(nova, destino);
                            Pecas.Add(nova);
                            break;
                        case 'N':
                            nova = new Cavalo(Tab, p.Cor);
                            Tab.ColocarPeca(nova, destino);
                            Pecas.Add(nova);
                            break;
                        case 'R':
                            nova = new Torre(Tab, p.Cor);
                            Tab.ColocarPeca(nova, destino);
                            Pecas.Add(nova);
                            break;
                        case 'B':
                            nova = new Bispo(Tab, p.Cor);
                            Tab.ColocarPeca(nova, destino);
                            Pecas.Add(nova);
                            break;

                        case 'q':
                            nova = new Dama(Tab, p.Cor);
                            Tab.ColocarPeca(nova, destino);
                            Pecas.Add(nova);
                            break;
                        case 'n':
                            nova = new Cavalo(Tab, p.Cor);
                            Tab.ColocarPeca(nova, destino);
                            Pecas.Add(nova);
                            break;
                        case 'r':
                            nova = new Torre(Tab, p.Cor);
                            Tab.ColocarPeca(nova, destino);
                            Pecas.Add(nova);
                            break;
                        case 'b':
                            nova = new Bispo(Tab, p.Cor);
                            Tab.ColocarPeca(nova, destino);
                            Pecas.Add(nova);
                            break;
                    }
                }
            }

            if (EstaEmXeque(Adversaria(JogadorAtual)))
            {
                Xeque = true;
            }
            else
            {
                Xeque = false;
            }

            if (TesteXequeMate(Adversaria(JogadorAtual)))
            {
                Terminada = true;
            }
            else
            {
                Turno++;
                MudaJogador();
            }

            // # jogada especial en passant
            if (p is Peao && destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2)
            {
                VulneravelEnPassant = p;
            }
            else
            {
                VulneravelEnPassant = null;
            }

        }

        public void ValidarPosicaoDeOrigem(Posicao pos)
        {
            if (Tab.Peca(pos) == null)
            {
                throw new TabuleiroException("Não existe pessa nessa posição.");
            }
            if (JogadorAtual != Tab.Peca(pos).Cor)
            {
                throw new TabuleiroException("É a vez das " + JogadorAtual + "s");
            }
            if (!Tab.Peca(pos).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Esta peça não pode se mover");
            }
        }

        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!Tab.Peca(origem).MovimentoPossivel(destino))
            {
                throw new TabuleiroException("Posição de destino inválida");
            }
        }


        private void MudaJogador()
        {
            if (JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
                JogadorAtual = Cor.Branca;
            }
        }

        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca i in Capturadas)
            {
                if (i.Cor == cor)
                {
                    aux.Add(i);
                }
            }
            return aux;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca i in Pecas)
            {
                if (i.Cor == cor)
                {
                    aux.Add(i);
                }
            }
            aux.ExceptWith(PecasCapturadas(cor));
            return aux;
        }

        private Peca Rei(Cor cor)
        {
            foreach (Peca i in PecasEmJogo(cor))
            {
                if (i is Rei)
                {
                    return i;
                }
            }
            return null;
        }

        public bool EstaEmXeque(Cor cor)
        {
            Peca R = Rei(cor);
            if (R == null)
            {
                throw new TabuleiroException($"Não existe rei {cor} no tabuleiro");
            }

            foreach (Peca i in PecasEmJogo(Adversaria(cor)))
            {
                bool[,] mat = i.MovimentosPossiveis();
                if (mat[R.Posicao.Linha, R.Posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public bool TesteXequeMate(Cor cor)
        {
            if (!EstaEmXeque(cor))
            {
                return false;
            }

            foreach (Peca x in PecasEmJogo(cor))
            {
                bool[,] mat = x.MovimentosPossiveis();
                for (int i = 0; i < Tab.Linhas; i++)
                {
                    for (int j = 0; j < Tab.Colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.Posicao;
                            Posicao destino = new Posicao(i, j);

                            Peca PecaCapturada = ExecutaMovimento(origem, destino);
                            bool testeXeque = EstaEmXeque(cor);
                            DesfazMovimento(origem, destino, PecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        private Cor Adversaria(Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            return Cor.Branca;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            Tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).ToPosicao());
            Pecas.Add(peca);
        }

        private void ColocarPecas()
        {
            ColocarNovaPeca('b', 8, new Cavalo(Tab, Cor.Preta));
            ColocarNovaPeca('g', 8, new Cavalo(Tab, Cor.Preta));
            ColocarNovaPeca('a', 8, new Torre(Tab, Cor.Preta));
            ColocarNovaPeca('h', 8, new Torre(Tab, Cor.Preta));
            ColocarNovaPeca('c', 8, new Bispo(Tab, Cor.Preta));
            ColocarNovaPeca('f', 8, new Bispo(Tab, Cor.Preta));
            ColocarNovaPeca('d', 8, new Dama(Tab, Cor.Preta));
            ColocarNovaPeca('e', 8, new Rei(Tab, Cor.Preta, this));

            for (char i = 'h'; i >= 'a'; i--)
            {
                ColocarNovaPeca(i, 7, new Peao(Tab, Cor.Preta, this));
            }

            ColocarNovaPeca('b', 1, new Cavalo(Tab, Cor.Branca));
            ColocarNovaPeca('g', 1, new Cavalo(Tab, Cor.Branca));
            ColocarNovaPeca('a', 1, new Torre(Tab, Cor.Branca));
            ColocarNovaPeca('h', 1, new Torre(Tab, Cor.Branca));
            ColocarNovaPeca('c', 1, new Bispo(Tab, Cor.Branca));
            ColocarNovaPeca('f', 1, new Bispo(Tab, Cor.Branca));
            ColocarNovaPeca('d', 1, new Dama(Tab, Cor.Branca));
            ColocarNovaPeca('e', 1, new Rei(Tab, Cor.Branca, this));

            for (char i = 'h'; i >= 'a'; i--)
            {
                ColocarNovaPeca(i, 2, new Peao(Tab, Cor.Branca, this));
            }
        }
    }
}