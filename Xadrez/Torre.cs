using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tabuleiro;

namespace Xadrez
{
    public class Torre : Peca
    {
        public Torre(Tabuleiro tab, Cor cor)
        : base(tab, cor)
        {
        }

        public override string ToString()
        {
            return "T";
        }
    }
}