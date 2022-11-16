using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tabuleiro;

namespace Xadrez_console
{
    public class Tela
    {
        public static void imprimirTabuleiro(Tabuleiro tab){
            for(int i = 0; i < tab.Linhas; i++){
                for(int j = 0; j < tab.Colunas; j++ ){

                    if(tab.peca(i,j) == null){
                        System.Console.Write("- ");
                    }
                    else{
                        System.Console.Write(tab.peca(i,j) + " ");
                    }
                }
                System.Console.WriteLine();
            }
        }
    }
}