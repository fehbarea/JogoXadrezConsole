using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tabuleiro
{
    public class Tabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas { get; set; }
        private Peca[,] Peca;

        public Tabuleiro (int linhas, int colunas)
        {

            Linhas = linhas;
            Colunas = colunas;
            Peca = new Peca[Linhas, Colunas];
        }

        public Peca peca (int linha, int coluna)
        {

            return Peca[linha, coluna];
        }

        public Peca peca (Posicao pos)
        {
            return Peca[pos.Linha, pos.Coluna];
        }

        public void ColocarPeca (Peca p, Posicao pos)
        {
            if(ExistePeca(pos)) {
                throw new TabuleiroException("Já existe peça");
            }
            Peca[pos.Linha, pos.Coluna] = p;
            p.Posicao = pos;
        }

        public Peca RetirarPeca(Posicao pos){
            if(peca(pos) == null){
                return null;
            }
            Peca aux = peca(pos);
            aux.Posicao = null;
            Peca[pos.Linha, pos.Coluna] = null;
            return aux;
        }
        public bool ExistePeca (Posicao pos)
        {

            ValidarPosicao(pos);
            return peca(pos) != null;
        }

        public bool PosicaoValida(Posicao pos)
        {
            if (pos.Linha < 0 || pos.Coluna < 0 || pos.Linha >= Linhas || pos.Coluna >= Colunas)
            {
                return false;
            }
            return true;
        }
        public void ValidarPosicao(Posicao pos)
        {
            if (!PosicaoValida(pos))
            {
                throw new TabuleiroException("Posição Inválida");
            }
        }
    }
}