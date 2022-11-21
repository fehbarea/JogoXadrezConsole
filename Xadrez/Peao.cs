using tabuleiro;

namespace Xadrez
{

    class Peao : Peca
    {

        private PartidaDeXadrez Partida;

        public Peao(Tabuleiro Tab, Cor Cor, PartidaDeXadrez partida) : base(Tab, Cor)
        {
            Partida = partida;
        }

        public override string ToString()
        {
            return "P";
        }

        private bool existeInimigo(Posicao pos)
        {
            Peca p = Tab.peca(pos);
            return p != null && p.Cor != Cor;
        }

        private bool livre(Posicao pos)
        {
            return Tab.peca(pos) == null;
        }

        public override bool[,] MovimentosPossiveis()
        {
            bool[,] mat = new bool[Tab.Linhas, Tab.Colunas];

            Posicao pos = new Posicao(0, 0);

            if (Cor == Cor.Branca)
            {
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna);
                if (Tab.PosicaoValida(pos) && livre(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(Posicao.Linha - 2, Posicao.Coluna);
                Posicao p2 = new Posicao(Posicao.Linha - 1, Posicao.Coluna);
                if (Tab.PosicaoValida(p2) && livre(p2) && Tab.PosicaoValida(pos) && livre(pos) && QtdMovimentos == 0)
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna - 1);
                if (Tab.PosicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(Posicao.Linha - 1, Posicao.Coluna + 1);
                if (Tab.PosicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }

                // jogada especial empassant
                if (Posicao.Linha == 3)
                {

                    Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    if (Tab.PosicaoValida(esquerda) && existeInimigo(esquerda) && Tab.peca(esquerda) == Partida.VulneravelEmpassant)
                    {
                        mat[esquerda.Linha - 1, esquerda.Coluna] = true;
                    }

                    Posicao Direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    if (Tab.PosicaoValida(Direita) && existeInimigo(Direita) && Tab.peca(Direita) == Partida.VulneravelEmpassant)
                    {
                        mat[Direita.Linha - 1, Direita.Coluna] = true;
                    }
                }
            }
            
            else
            {
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna);
                if (Tab.PosicaoValida(pos) && livre(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(Posicao.Linha + 2, Posicao.Coluna);
                Posicao p2 = new Posicao(Posicao.Linha + 1, Posicao.Coluna);
                if (Tab.PosicaoValida(p2) && livre(p2) && Tab.PosicaoValida(pos) && livre(pos) && QtdMovimentos == 0)
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna - 1);
                if (Tab.PosicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                pos.DefinirValores(Posicao.Linha + 1, Posicao.Coluna + 1);
                if (Tab.PosicaoValida(pos) && existeInimigo(pos))
                {
                    mat[pos.Linha, pos.Coluna] = true;
                }
                
                // jogada especial empassant
                if (Posicao.Linha == 4)
                {

                    Posicao esquerda = new Posicao(Posicao.Linha, Posicao.Coluna - 1);
                    if (Tab.PosicaoValida(esquerda) && existeInimigo(esquerda) && Tab.peca(esquerda) == Partida.VulneravelEmpassant)
                    {
                        mat[esquerda.Linha + 1, esquerda.Coluna] = true;
                    }

                    Posicao Direita = new Posicao(Posicao.Linha, Posicao.Coluna + 1);
                    if (Tab.PosicaoValida(Direita) && existeInimigo(Direita) && Tab.peca(Direita) == Partida.VulneravelEmpassant)
                    {
                        mat[Direita.Linha + 1, Direita.Coluna] = true;
                    }
                }
            }


            return mat;
        }
    }
}