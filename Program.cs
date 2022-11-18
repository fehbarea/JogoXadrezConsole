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
        public static void Main(string[] args)
        {
            Console.Clear();

            try
            {
                PartidaDeXadrez partida = new PartidaDeXadrez();

                while (!partida.terminada)
                {
                    Tela.imprimirTabuleiro(partida.tab);

                    System.Console.WriteLine();
                    System.Console.WriteLine("Origem: ");
                    Posicao origem = Tela.LerPosicaoXadrez().toPosicao();
                    System.Console.WriteLine("Destini: ");
                    Posicao destino = Tela.LerPosicaoXadrez().toPosicao();

                    partida.ExecutarMovimento(origem, destino);
                }
            }

            catch (TabuleiroException e)
            {
                System.Console.WriteLine(e.Message);
            }
        }
    }
}