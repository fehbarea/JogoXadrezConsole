using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tabuleiro
{
    public class Tabuleiro
    {
        public int Linhas { get; set; }
        public int Colunas {get; set;}
        private Peca[,] Peca;

        public Tabuleiro(int linhas, int colunas){
          
            Linhas = linhas;
            Colunas  = colunas;
            Peca = new Peca[Linhas, Colunas];
        }
    }
}