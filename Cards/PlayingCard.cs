using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    internal class PlayingCard 
    {
        public PlayingCard() { }
        public PlayingCard(int kind, int number)
        {
            Kind = kind;
            Number = number;
        }
        public int Kind { get; set; }
        public int Number { get; set; }

        public override string ToString()
        {
            string number = Number.ToString();
            switch (Number)
            {
                case 1: number = "Ace";
                    break;
                case 11: number = "Jack";
                    break;
                case 12: number = "Queen";
                    break;
                case 13: number = "King";
                    break;
            }
            string kind = "hearts";
            switch (Kind)
            {
                case 2: kind = "suits";
                    break;
                case 3: kind = "clovers";
                    break;
                case 4: kind = "spades";
                    break;
            }
            return $"{number} of {kind}";
        }

        /// <summary>
        /// Gives back a normal full deck of cards
        /// </summary>
        /// <returns>
        /// 52 cards
        /// </returns>
        public static PlayingCard[] FullDeck()
        {
            var result = new PlayingCard[52];
            for(int i = 1; i <= 4; i++) 
            {
                for(int j = 1; j <= 13; j++)
                {
                    result[(i - 1) * 13 + j - 1] = new PlayingCard(i, j);
                }
            }
            return result; 
        }

    }
}
