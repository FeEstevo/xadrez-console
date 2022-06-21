using tabuleiro;

namespace xadrez
{
    class Cavalo : Peca
    {

        public Cavalo(Tabuleiro tab, Cor cor) : base(tab, cor) { }

        public override string ToString()
        {
            return "N";
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];
            Posicao pos = new Posicao(0, 0);

            //Cima-esquerda
            pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna -1);
            if (Tab.PosicaoValida(pos))
            {
                mat[pos.Linha, pos.Coluna] = PodeMover(pos);
            }
            //Cima-direita
            pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna + 1);
            if (Tab.PosicaoValida(pos))
            {
                mat[pos.Linha, pos.Coluna] = PodeMover(pos);
            }
            //Direita-cima
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 2);
            if (Tab.PosicaoValida(pos))
            {
                mat[pos.Linha, pos.Coluna] = PodeMover(pos);
            }
            //Direita-baixo
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 2);
            if (Tab.PosicaoValida(pos))
            {
                mat[pos.Linha, pos.Coluna] = PodeMover(pos);
            }
            //Baixo-direita
            pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna + 1);
            if (Tab.PosicaoValida(pos))
            {
                mat[pos.Linha, pos.Coluna] = PodeMover(pos);
            }
            //Baixo-esquerda
            pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna - 1);
            if (Tab.PosicaoValida(pos))
            {
                mat[pos.Linha, pos.Coluna] = PodeMover(pos);
            }
            //Esquerda-cima
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 2);
            if (Tab.PosicaoValida(pos))
            {
                mat[pos.Linha, pos.Coluna] = PodeMover(pos);
            }
            //Esquerda-baixo
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 2);
            if (Tab.PosicaoValida(pos))
            {
                mat[pos.Linha, pos.Coluna] = PodeMover(pos);
            }
            return mat;
        }
    }
}
