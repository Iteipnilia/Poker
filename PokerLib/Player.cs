using System.Linq;
using System.Collections.Generic;
using System;
using System.Collections;

namespace Poker
{

    class Player : IPlayer, IEnumerable<Hand>
    {
        public string Name { get; set; }
        string IPlayer.Name => Name;
        public int Wins { get; set; }
        int IPlayer.Wins => Wins;
        public ICard[] Discard { get; set; }
        ICard[] IPlayer.Discard { set { Discard = value; } }
        public Hand Hand {get; set;}
        ICard[] IPlayer.Hand => Hand.ToArray();
        HandType IPlayer.HandType => Hand.HandType;

        public void ReceiveCards(Card card)
        {
            if (Hand == null || Hand.Count() == 0)
            {
                Hand = new Hand();
                Discard = new ICard[0];
            }
            Hand.AddCardToHand(card);
        }

        public void SortPlayerHand(Hand Hand)
        {
            Hand.SortHand();
        }

        public void DiscardCard(Card card)
        {
            Hand.RemoveCard(card);
        }

        public void Win()
        {
            Wins++;
        }

        public override string ToString()
        {
            return $"{Name} {Wins}";
        }

        public IEnumerator GetEnumerator() => Hand.GetEnumerator();

        IEnumerator<Hand> IEnumerable<Hand>.GetEnumerator()
        {
            return (IEnumerator<Hand>)Hand.GetEnumerator();
        }
    }
}