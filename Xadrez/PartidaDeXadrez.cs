using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tabuleiro;

namespace Xadrez
{
    public class PartidaDeXadrez
    {
        public Tabuleiro tab {get; set;}
        public int turno{get; private set;}
        public Cor jogadorAtual{get; private set;}
        public bool terminada{get; private set;}

        public PartidaDeXadrez(){

            tab = new Tabuleiro(8,8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            ColocarPecas();
        }

        public void ExecutarMovimento(Posicao origem, Posicao destino){

            Peca p = tab.RetirarPeca(origem);
            p.IncrementarQtdMovimento();
            Peca pecaCapturada = tab.RetirarPeca(destino);
            tab.ColocarPeca(p, destino);
        }

        public void realizajogada(Posicao origem, Posicao destino){

            ExecutarMovimento(origem, destino);
            turno++;
            MuldaJogador();
        }

        private void MuldaJogador(){
            if(jogadorAtual == Cor.Branca){
                jogadorAtual = Cor.Preta;
            }
            else{
                jogadorAtual = Cor.Branca;
            }
        }

        public void ValidarPosicaoDeOrigem(Posicao pos){
            
            if(tab.peca(pos) == null){
                throw new TabuleiroException("Não existe peça na posição de origem!!");
            }
            if(jogadorAtual != tab.peca(pos).Cor){
                throw new TabuleiroException("A peça não é sua!");
            }
            if(!tab.peca(pos).ExiseMovimentosPossiveis()){
                throw new TabuleiroException("Não a movimentos possíveis para a peça escolhida!!");
            }
        }
        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino){
            if(!tab.peca(origem).PodeMoverPara(destino)){
                throw new TabuleiroException("Posiçao de destino inválida");
            }
        }

        private void ColocarPecas(){

            tab.ColocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('c',1).toPosicao());
            tab.ColocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('c',2).toPosicao());
            tab.ColocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('d',2).toPosicao());
            tab.ColocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('e',2).toPosicao());
            tab.ColocarPeca(new Torre(tab, Cor.Branca), new PosicaoXadrez('e',1).toPosicao());
            tab.ColocarPeca(new Rei(tab, Cor.Branca), new PosicaoXadrez('d',1).toPosicao());

            tab.ColocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('c',7).toPosicao());
            tab.ColocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('c',8).toPosicao());
            tab.ColocarPeca(new Torre(tab, Cor.Preta), new PosicaoXadrez('d',8).toPosicao());


        }
    }
}