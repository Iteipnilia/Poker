using System.Linq;
using System.Collections.Generic;
using System;

namespace Poker
{

    class Player : IPlayer
    {
        private string name;
        public string Name { get => name; }
        public int wins;
        public int Wins { get; set; }
        private List<Card> discard = new List<Card>();
        public ICard[] Discard { get; set; }
        public HandType HandType { get => Hand.HandType; }
        public Hand Hand {get; set;}
        ICard[] IPlayer.Hand => Hand.ToArray();

        public Player(string name_)
        {
            this.name = name_;
            wins = 0;

        }
        public Hand Hands
        {
            get { return Hand; }
            set { value = Hand; }
        }

        public List<Card> Discard_
        {
            get { return discard; }
            set { value = discard; }
        }

        public void SortPlayerHand(Hand Hand)
        {
            Hand.SortHand();
        }

        public void ReceiveCards(Card card)
        {
            Hand.AddCardToHand(card);
        }

        public void DiscardCard(Card card)
        {
            Hands.RemoveCard(card);
        }

        public void Win()
        {
            Wins++;
        }
    }
}