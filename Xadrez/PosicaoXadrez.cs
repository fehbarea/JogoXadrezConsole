using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tabuleiro;

namespace Xadrez
{
    public class PosicaoXadrez
    {
        public char Coluna {get; private set;}
        public int Linha {get; set;}

        public PosicaoXadrez(char coluna, int linha) {
            Coluna = coluna;
            Linha = linha;
        }

        public Posicao toPosicao() {
            return new Posicao(8 - Linha, Coluna - 'a');
        }

        public override string ToString()
        {
            return "" + Coluna + Linha;
        }
    }
}