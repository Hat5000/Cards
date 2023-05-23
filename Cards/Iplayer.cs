using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    internal interface IPlayer
    {
        public CardGame Game { get; set; }
        string Name { get; set; }
        List<PlayingCard> Cards { get; set; }
        public PlayingCard Play();
        public int Points { get; set; }
    }
}
