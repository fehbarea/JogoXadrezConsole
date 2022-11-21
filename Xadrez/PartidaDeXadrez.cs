using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tabuleiro;

namespace Xadrez
{
    public class PartidaDeXadrez
    {
        public Tabuleiro tab { get; set; }
        public int turno { get; private set; }
        public Cor jogadorAtual { get; private set; }
        public bool terminada { get; private set; }
        private HashSet<Peca> pecas;
        private HashSet<Peca> capturads;
        public bool xeque { get; private set; }
        public Peca VulneravelEmpassant {get; private set;}

        public PartidaDeXadrez()
        {

            tab = new Tabuleiro(8, 8);
            turno = 1;
            jogadorAtual = Cor.Branca;
            terminada = false;
            pecas = new HashSet<Peca>();
            capturads = new HashSet<Peca>();
            xeque = false;
            VulneravelEmpassant = null;
            ColocarPecas();
        }
        private Cor adversaria(Cor cor)
        {
            if (cor == Cor.Branca)
            {
                return Cor.Preta;
            }
            else
            {
                return Cor.Branca;
            }
        }

        private Peca rei(Cor cor)
        {
            foreach (Peca x in pecasEmJogo(cor))
            {
                if (x is Rei)
                {
                    return x;
                }
            }
            return null;
        }

        public Peca ExecutarMovimento(Posicao origem, Posicao destino)
        {

            Peca p = tab.RetirarPeca(origem);
            p.IncrementarQtdMovimento();
            Peca pecaCapturada = tab.RetirarPeca(destino);
            tab.ColocarPeca(p, destino);
            if (pecaCapturada != null)
            {
                capturads.Add(pecaCapturada);
            }

            // jogada Roque Pequeno
            if(p is Rei  && destino.Coluna == origem.Coluna + 2){
                Posicao OrigemT = new(origem.Linha, origem.Coluna + 3);
                Posicao DestinoT = new(origem.Linha, origem.Coluna + 1);
                Peca T = tab.RetirarPeca(OrigemT);
                T.IncrementarQtdMovimento();
                tab.ColocarPeca(T, DestinoT);
            }

            // jogada Roque Pequeno
            if(p is Rei  && destino.Coluna == origem.Coluna - 2){
                Posicao OrigemT = new(origem.Linha, origem.Coluna - 4);
                Posicao DestinoT = new(origem.Linha, origem.Coluna - 1);
                Peca T = tab.RetirarPeca(OrigemT);
                T.IncrementarQtdMovimento();
                tab.ColocarPeca(T, DestinoT);
            }

            // jogada especial empassant

            if(p is Peao){
                if(origem.Coluna != destino.Coluna && pecaCapturada == null){
                    Posicao posP;
                    if(p.Cor == Cor.Branca){
                        posP = new Posicao(destino.Linha + 1, destino.Coluna);
                    }
                    else{
                        posP = new Posicao(destino.Coluna - 1, destino.Coluna);
                    }
                    pecaCapturada = tab.RetirarPeca(posP);
                    capturads.Add(pecaCapturada);
                }
            }
            return pecaCapturada;

        }



        public void desfazerMovimento(Posicao origem, Posicao destino, Peca pecaCapturada)
        {

            Peca p = tab.RetirarPeca(destino);
            p.DecrementarQtdMovimento();
            if (pecaCapturada != null)
            {
                tab.ColocarPeca(pecaCapturada, destino);
                capturads.Remove(pecaCapturada);
            }
            tab.ColocarPeca(p, origem);

            // jogada Roque Pequeno
            if(p is Rei  && destino.Coluna == origem.Coluna + 2){
                Posicao OrigemT = new(origem.Linha, origem.Coluna + 3);
                Posicao DestinoT = new(origem.Linha, origem.Coluna + 1);
                Peca T = tab.RetirarPeca(DestinoT);
                T.DecrementarQtdMovimento();
                tab.ColocarPeca(T, OrigemT);
            }

             // jogada Roque Pequeno
            if(p is Rei  && destino.Coluna == origem.Coluna - 2){
                Posicao OrigemT = new(origem.Linha, origem.Coluna - 4);
                Posicao DestinoT = new(origem.Linha, origem.Coluna - 1);
                Peca T = tab.RetirarPeca(DestinoT);
                T.DecrementarQtdMovimento();
                tab.ColocarPeca(T, OrigemT);
            }
            // Jogada especial empasant
            if(p is Peao){
                if(origem.Coluna != destino.Coluna && pecaCapturada == VulneravelEmpassant){
                    Peca peao = tab.RetirarPeca(destino);
                    Posicao posP;
                    if(p.Cor == Cor.Branca){
                        posP = new Posicao(3, destino.Coluna);
                    }
                    else{
                        posP = new Posicao (3, destino.Coluna);
                    }
                    tab.ColocarPeca(peao, posP);
                }
            }
        }

        public void realizajogada(Posicao origem, Posicao destino)
        {

            Peca pecasCaptudaras = ExecutarMovimento(origem, destino);

            if (EstaEmXeque(jogadorAtual))
            {
                desfazerMovimento(origem, destino, pecasCaptudaras);
                throw new TabuleiroException("Voçe não pode colocar seu rei em xeque");
            }
            if (EstaEmXeque(adversaria(jogadorAtual)))
            {
                xeque = true;
            }
            else
            {
                xeque = false;
            }

            if (TesteXequeMate(adversaria(jogadorAtual)))
            {
                terminada = true;
            }
            else
            {
                turno++;
                MuldaJogador();
            }

            Peca p = tab.peca(destino);

            //Jogada especial emPassant

            if(p is Peao && (destino.Linha == origem.Linha + 2) || (destino.Linha == origem.Linha - 2)){
                VulneravelEmpassant = p;
            }
            else{
                VulneravelEmpassant = null;
            }
        }

        private void MuldaJogador()
        {
            if (jogadorAtual == Cor.Branca)
            {
                jogadorAtual = Cor.Preta;
            }
            else
            {
                jogadorAtual = Cor.Branca;
            }
        }

        public void ValidarPosicaoDeOrigem(Posicao pos)
        {

            if (tab.peca(pos) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem!!");
            }
            if (jogadorAtual != tab.peca(pos).Cor)
            {
                throw new TabuleiroException("A peça não é sua!");
            }
            if (!tab.peca(pos).ExiseMovimentosPossiveis())
            {
                throw new TabuleiroException("Não a movimentos possíveis para a peça escolhida!!");
            }
        }
        public void ValidarPosicaoDeDestino(Posicao origem, Posicao destino)
        {
            if (!tab.peca(origem).MovimentoPossivel(destino))
            {
                throw new TabuleiroException("Posiçao de destino inválida");
            }
        }

        public bool EstaEmXeque(Cor cor)
        {
            Peca R = rei(cor);
            if (R == null)
            {
                throw new TabuleiroException($"Não tem rei da cor {cor} no tabuleiro");
            }

            foreach (var x in pecasEmJogo(adversaria(cor)))
            {
                bool[,] mat = x.MovimentosPossiveis();
                if (mat[R.Posicao.Linha, R.Posicao.Coluna])
                {
                    return true;
                }
            }
            return false;
        }

        public bool TesteXequeMate(Cor cor)
        {
            if (!EstaEmXeque(cor))
            {
                return false;
            }
            foreach (Peca x in pecasEmJogo(cor))
            {
                bool[,] mat = x.MovimentosPossiveis();
                for (int i = 0; i < tab.Linhas; i++)
                {
                    for (int j = 0; j < tab.Colunas; j++)
                    {
                        if (mat[i, j])
                        {
                            Posicao origem = x.Posicao;
                            Posicao destino = new Posicao(i, j);
                            Peca pecaCapturada = ExecutarMovimento(origem, destino);
                            bool testeXeque = EstaEmXeque(cor);
                            desfazerMovimento(origem, destino, pecaCapturada);
                            if (!testeXeque)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
        public HashSet<Peca> pecasCaptudaras(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in capturads)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            return aux;
        }
        public HashSet<Peca> pecasEmJogo(Cor cor)
        {

            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(pecasCaptudaras(cor));
            return aux;
        }

        public void ColocarNovaPeca(char coluna, int linha, Peca peca)
        {
            tab.ColocarPeca(peca, new PosicaoXadrez(coluna, linha).toPosicao());
            pecas.Add(peca);
        }

        private void ColocarPecas()
        {
            ColocarNovaPeca('a', 1, new Torre(tab, Cor.Branca));
            ColocarNovaPeca('b', 1, new Cavalo(tab, Cor.Branca));
            ColocarNovaPeca('c', 1, new Bispo(tab, Cor.Branca));
            ColocarNovaPeca('d', 1, new Dama(tab, Cor.Branca));
            ColocarNovaPeca('e', 1, new Rei(tab, Cor.Branca, this));
            ColocarNovaPeca('f', 1, new Bispo(tab, Cor.Branca));
            ColocarNovaPeca('g', 1, new Cavalo(tab, Cor.Branca));
            ColocarNovaPeca('h', 1, new Torre(tab, Cor.Branca));
            ColocarNovaPeca('a', 2, new Peao(tab, Cor.Branca, this));
            ColocarNovaPeca('b', 2, new Peao(tab, Cor.Branca, this));
            ColocarNovaPeca('c', 2, new Peao(tab, Cor.Branca, this));
            ColocarNovaPeca('d', 2, new Peao(tab, Cor.Branca, this));
            ColocarNovaPeca('e', 2, new Peao(tab, Cor.Branca, this));
            ColocarNovaPeca('f', 2, new Peao(tab, Cor.Branca, this));
            ColocarNovaPeca('g', 2, new Peao(tab, Cor.Branca, this));
            ColocarNovaPeca('h', 2, new Peao(tab, Cor.Branca, this));

            ColocarNovaPeca('a', 8, new Torre(tab, Cor.Preta));
            ColocarNovaPeca('b', 8, new Cavalo(tab, Cor.Preta));
            ColocarNovaPeca('c', 8, new Bispo(tab, Cor.Preta));
            ColocarNovaPeca('d', 8, new Dama(tab, Cor.Preta));
            ColocarNovaPeca('e', 8, new Rei(tab, Cor.Preta, this));
            ColocarNovaPeca('f', 8, new Bispo(tab, Cor.Preta));
            ColocarNovaPeca('g', 8, new Cavalo(tab, Cor.Preta));
            ColocarNovaPeca('h', 8, new Torre(tab, Cor.Preta));
            ColocarNovaPeca('a', 7, new Peao(tab, Cor.Preta, this));
            ColocarNovaPeca('b', 7, new Peao(tab, Cor.Preta, this));
            ColocarNovaPeca('c', 7, new Peao(tab, Cor.Preta, this));
            ColocarNovaPeca('d', 7, new Peao(tab, Cor.Preta, this));
            ColocarNovaPeca('e', 7, new Peao(tab, Cor.Preta, this));
            ColocarNovaPeca('f', 7, new Peao(tab, Cor.Preta, this));
            ColocarNovaPeca('g', 7, new Peao(tab, Cor.Preta, this));
        }
    }
}