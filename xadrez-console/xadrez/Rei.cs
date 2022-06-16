using tabuleiro;

namespace xadrez
{
    class Rei : Peca
    {

        public Rei(Tabuleiro tab, Cor cor) : base(tab, cor) { }

        public override string ToString()
        {
            return "K";
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];
            Posicao pos = new Posicao(0, 0);

            //norte
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
            if (Tab.PosicaoValida(pos))
            {
                mat[pos.Linha, pos.Coluna] = PodeMover(pos);
            }
            //nordeste
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
            if (Tab.PosicaoValida(pos))
            {
                mat[pos.Linha, pos.Coluna] = PodeMover(pos);
            }
            //noroeste
            pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
            if (Tab.PosicaoValida(pos))
            {
                mat[pos.Linha, pos.Coluna] = PodeMover(pos);
            }
            //leste
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna + 1);
            if (Tab.PosicaoValida(pos))
            {
                mat[pos.Linha, pos.Coluna] = PodeMover(pos);
            }
            //oeste
            pos.DefinirValores(Posicao.Linha, Posicao.Coluna - 1);
            if (Tab.PosicaoValida(pos))
            {
                mat[pos.Linha, pos.Coluna] = PodeMover(pos);
            }
            //sul
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
            if (Tab.PosicaoValida(pos))
            {
                mat[pos.Linha, pos.Coluna] = PodeMover(pos);
            }
            //sudeste
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
            if (Tab.PosicaoValida(pos))
            {
                mat[pos.Linha, pos.Coluna] = PodeMover(pos);
            }
            //sudoeste
            pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
            if (Tab.PosicaoValida(pos))
            {
                mat[pos.Linha, pos.Coluna] = PodeMover(pos);
            }
            return mat;
        }
    }
}
