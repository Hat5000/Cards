using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Cards
{
    internal abstract class CardGame
    {
        public CardGame(IPlayer[] players, PlayingCard[] cards, int numberCardsPerPLayer) 
        {
            Players = players;
            Cards = cards;
            NumberCardsPerPlayer = numberCardsPerPLayer;
            foreach (var player in Players)
            {
                player.Game = this;
            }
        }
        public IPlayer[] Players { get; set; }
        public PlayingCard[] Cards { get; set; }
        public PlayingCard[]? UnusedCards { get; set; } 
        public int NumberCardsPerPlayer = 0;
        public int PlayersTurn = 0;
        public PlayingCard[][] PreviousSlagen;
        public PlayingCard[] CurrentSlag;
        private int RoundNumber = 1;
        public abstract void SlagRule(PlayingCard[] cards);
        public abstract List<PlayingCard> AllowedMoves(List<PlayingCard> cards);
        public virtual void RoundStart()
        {
            Console.WriteLine($"Round {RoundNumber} has started best of luck to you");
        }
        public virtual void RoundEnd()
        {
            Console.WriteLine("\nThe round has ended");
            for(int i = 1;  i <= Players.Length; i++) 
            {
                Console.WriteLine($"Player {i} has {Players[i- 1].Points} points");
            }
        }

        public virtual void Shuffle()
        {
            var random = new Random();
            var deck = Cards.ToList();
            PlayingCard randomCard;
            for(int i = 0; i < NumberCardsPerPlayer; i++)
            {
                foreach(var player in Players)
                {
                    int index = random.Next(deck.Count);
                    randomCard = deck[index];
                    deck.RemoveAt(index);
                    player.Cards.Add(randomCard);
                }
            }
            UnusedCards = deck.ToArray();
        }
        public void PlayGame()
        {
            bool playing = true;
            //everything that happens in a slag
            while(playing)
            {
                // shuffles the cards
                Shuffle();
                RoundStart();
                PreviousSlagen = new PlayingCard[NumberCardsPerPlayer][];
                for (int slagCounter = 0; slagCounter < NumberCardsPerPlayer; slagCounter++)
                {
                    //every player plays 1 card
                    CurrentSlag = new PlayingCard[NumberCardsPerPlayer];
                    Console.WriteLine($"\nSlag {slagCounter + 1} starts: ");
                    for (int i = 0; i < Players.Length; i++)
                    {
                        CurrentSlag[i] = Players[(PlayersTurn + i) % Players.Length].Play();
                    }
                    //Determines what happens after the slag
                    SlagRule(CurrentSlag);
                    PreviousSlagen[slagCounter] = CurrentSlag;
                }
                RoundNumber++;
                RoundEnd();
                Console.WriteLine("\nIf you wanne keep playing press enter");    ;
                if(Console.ReadKey(true).Key != ConsoleKey.Enter)
                {
                    playing = false;
                }
           }
        }
    }
}
