using System.Linq;
using System.Collections.Generic;
using System;

namespace Poker
{

    class Player : IPlayer
    {
        private string name;
        public string Name { get => name; }
        private int wins;
        public int Wins { get; set; }
        private List<Card> discard = new List<Card>();
        public ICard[] Discard { get; set; }
        public HandType HandType { get => hand.HandType; }
        private Hands hand = new Hands();
        public ICard[] Hand { get => (hand.Hand).ToArray(); }

        public Player(string name)
        {
            this.name = name;
            wins = 0;

        }
        public Hands Hands
        {
            get { return hand; }
            set { value = hand; }
        }

        public List<Card> Discard_
        {
            get { return discard; }
            set { value = discard; }
        }

        public void DetermineHandType(Hands hand)
        {
            hand.Eval();
        }

        public void SortPlayerHand()
        {
            Hands.SortHand();
        }

        public void ReceiveCards(Card card)
        {
            Hands.AddCardToHand(card);
        }

        public void DiscardCard(Card card)
        {
            Hands.RemoveCard(card);
        }

        public void Win()
        {
            wins++;
        }
    }
}