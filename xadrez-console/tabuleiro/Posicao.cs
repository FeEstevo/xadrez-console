namespace tabuleiro
{
    class Posicao
    {
        public int Linha { get; set; }
        public int Coluna { get; set; }

        public Posicao(int linha, int coluna)
        {
            Linha = linha;
            Coluna = coluna;
        }

        public void DefinirValores(int linha, int coluna)
        {
            Linha = linha;
            Coluna = coluna;
        }

        public Posicao DefValores(int linha, int coluna)
        {
            Linha = linha;
            Coluna = coluna;
            return this;
        }

        public override string ToString()
        {
            return Linha + ", " + Coluna;
        }
    }
}
