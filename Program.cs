using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tabuleiro;
using Xadrez;

namespace Xadrez_console
{
    public class Program
    {
        public static void Main(string[] args){
            Console.Clear();

            try{
            Tabuleiro tab = new Tabuleiro(8 , 8);

            tab.ColocarPeca(new Torre(tab, Cor.Preta), new Posicao(0,0));
            tab.ColocarPeca(new Torre(tab, Cor.Preta), new Posicao(1,3));
            tab.ColocarPeca(new Rei(tab, Cor.Branca), new Posicao(2,4));

            Tela.imprimirTabuleiro(tab);
            }

            catch(TabuleiroException e){
                System.Console.WriteLine(e.Message);
            }
        }
    }
}