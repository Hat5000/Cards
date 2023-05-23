using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cards
{
    internal class HartenJagen : CardGame
    {
        public HartenJagen(IPlayer[] players) : base(players, PlayingCard.FullDeck(), 13)
        {

        }
        public HartenJagen() : base(new HartenJagenPlayer[4], PlayingCard.FullDeck(), 13)
        { 
            for(int i = 0; i < 4; i++)
            {
                Players[i] = new HartenJagenPlayer(i.ToString());
            }
        }

        public bool AlreadyPoints = false;

        public override void RoundStart()
        {
            AlreadyPoints = false;
            base.RoundStart();
        }
        public override void SlagRule(PlayingCard[] cards)
        {
            var firstCard = cards[0];
            var uitgekomenKind = firstCard.Kind;
            var highestNumber = firstCard.Number;
            var winningPlayer = 0;
            int totalPoints = 0;
            // determines which player won the slag
            for(int i = 0; i< cards.Length; i++)
            {
                var card = cards[i];
                if(card.Kind == uitgekomenKind && highestNumber < card.Number)
                {
                    highestNumber = card.Number;
                    winningPlayer = i;
                }
                // determines how many points the winning player gets
                if(card.Kind == 1)
                {
                    totalPoints++;
                }
                if(card.Number == 12)
                {
                    totalPoints += 13;
                }
                if(totalPoints != 0)
                {
                    AlreadyPoints = true;
                }
            } 
            var actualPlayer = (PlayersTurn + winningPlayer) % Players.Count();
            PlayersTurn = actualPlayer;
            Players[actualPlayer].Points += totalPoints;
        }

        public override List<PlayingCard> AllowedMoves(List<PlayingCard> cards)
        {
            var uitgekomenCard = CurrentSlag[0];
            if (uitgekomenCard == null)
            {
                if (AlreadyPoints)
                {
                    return cards;
                }
                else
                {
                    return cards.Where(x=>x.Kind!=1).ToList();
                }
            }
            else
            {
                var sameKind = cards.Where(x => x.Kind==uitgekomenCard.Kind).ToList();
                if (sameKind.Count == 0)
                {
                    var cardsThatArePoints = cards.Where(x=>x.Kind==1 || x.Number == 12).ToList();
                    if(cardsThatArePoints.Count == 0)
                    {
                        return cards;
                    }
                    else
                    {
                        return cardsThatArePoints;
                    }
                }
                else
                {
                    return sameKind;
                }
            }
        }
    }
}
