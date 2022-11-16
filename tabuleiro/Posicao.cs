using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tabuleiro
{
    public class Posicao
    {
        public int Linha{get; set;}
        public int Coluna{get; set;}
        
        public Posicao(int linha, int coluna){
           
            Linha = linha;
            Coluna = coluna;
        }

        public override string ToString()
        {
            return Linha
            + ", "
            + Coluna;
        }
    }
}