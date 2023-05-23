using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    internal class HartenJagenPlayer : IPlayer
    {
        public HartenJagenPlayer (string name)
        {
            Name = name;
        }
        public string Name { get; set; }
        public List<PlayingCard> Cards { get; set; } = new List<PlayingCard>();

        public int Points { get; set; } = 0;
        public CardGame Game {get; set; }

        public PlayingCard Play()
        {
            var random = new Random();
            var allowedCards = Game.AllowedMoves(Cards);
            var cardNumber = random.Next(allowedCards.Count);
            var card = Cards[cardNumber];
            Cards.Remove(card);
            Console.WriteLine($"Player {Name} Played {card.ToString()} ");
            return card;
        }
    }
}
