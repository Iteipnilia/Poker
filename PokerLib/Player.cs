using System.Linq;
using System.Collections.Generic;
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
        public HandType HandType { get => hands.HandType; }
        private Hands hands = new Hands();
        public ICard[] Hand { get => (hands.Hand).ToArray(); }

        public Player(string name)
        {
            this.name = name;
            wins = 0;

        }
        public Hands Hands
        {
            get { return hands; }
            set { value = hands; }
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

        public void SortPlayerHand(Hands hand)
        {
            hand.SortHand();
        }

        public void ReceiveCards(Card card)
        {
            hands.AddCardToHand(card);
        }

        public void DiscardCard()
        {
            
        }

        public void Win()
        {
            wins++;
        }
    }
}