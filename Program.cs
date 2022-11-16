using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tabuleiro;

namespace Xadrez_console
{
    public class Program
    {
        public static void Main(string[] args){

            Tabuleiro tab = new Tabuleiro(8 , 8);

            Tela.imprimirTabuleiro(tab);
        }
    }
}