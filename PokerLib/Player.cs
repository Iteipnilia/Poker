using System.Linq;
namespace Poker
{

    class Player : IPlayer
    {
        private string name;
        public string Name { get => name; }
        public int wins;
        public int Wins { get; set; }
        private ICard[] discard = new ICard[5];
        public ICard[] Discard { get; set; }
        public HandType HandType { get; set; }
        public ICard[] Hand { get; set; }

        public Player(string name_)
        {
            this.name = name_;
            wins = 0;
            Hand = new ICard[5];

        }
        public Hands Hands
        {
            get { return Hands; }
            set { value = Hands; }
        }

        public ICard[] Discard_
        {
            get { return discard; }
            set { value = discard; }
        }

        public void DetermineHandType(Hands hand)
        {
            hand.Eval();
        }

        // anropar metod i Hand för att tömma index på hand som ska bort
        // LÄgger till det borttagna kortet i array discard
        public void ReceiveCards(ICard card)
        {
            Hands.AddCardToHand(card);
        }

        public void DiscardCard()
        {
            Hand = Hand.Except(Discard).ToArray();
        }

        public void Win()
        {
            Wins++;
        }
    }
}