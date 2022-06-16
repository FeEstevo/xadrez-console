using tabuleiro;

namespace xadrez
{
    class Torre : Peca
    {

        public Torre(Tabuleiro tab, Cor cor) : base(tab, cor) { }

        public override string ToString()
        {
            return "R";
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];
            Posicao pos = new Posicao(0, 0);

            //abaixo
            for (int i = Posicao.Linha + 1; Tab.PosicaoValida(pos.DefValores(i, Posicao.Coluna)) 
                && PodeMover(pos.DefValores(i, Posicao.Coluna)); i++)
            {
                mat[i, Posicao.Coluna] = true;
            }
            //acima
            for (int i = Posicao.Linha - 1; Tab.PosicaoValida(pos.DefValores(i, Posicao.Coluna)) 
                && PodeMover(pos.DefValores(i, Posicao.Coluna)); i--)
            {
                mat[i, Posicao.Coluna] = true;
            }
            //direita
            for (int i = Posicao.Coluna + 1; Tab.PosicaoValida(pos.DefValores(Posicao.Linha, i)) 
                && PodeMover(pos.DefValores(Posicao.Linha, i)); i++)
            {
                mat[Posicao.Linha, i] = true;
            }
            //esquerda
            for (int i = Posicao.Coluna - 1; Tab.PosicaoValida(pos.DefValores(Posicao.Linha, i)) 
                && PodeMover(pos.DefValores(Posicao.Linha, i)); i--)
            {
                mat[Posicao.Linha, i] = true;
            }

            return mat;
        }
    }
}
