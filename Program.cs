﻿using System;
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

            /*try{
            Tabuleiro tab = new Tabuleiro(8 , 8);

            tab.ColocarPeca(new Torre(tab, Cor.Preta), new Posicao(0,0));
            tab.ColocarPeca(new Torre(tab, Cor.Preta), new Posicao(1,8));
            tab.ColocarPeca(new Rei(tab, Cor.Preta), new Posicao(0,0));

            Tela.imprimirTabuleiro(tab);
            }

            catch(TabuleiroException e){
                System.Console.WriteLine(e.Message);
            }*/

            PosicaoXadrez pos = new PosicaoXadrez('c', 7);
            System.Console.WriteLine(pos);
            System.Console.WriteLine(pos.toPosicao());
        }
    }
}