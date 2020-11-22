using System.Linq;
namespace Poker
{

    class Player : IPlayer
    {
        private string name;
        public string Name{get=> name;}
        public int wins;
        public int Wins{get; set;}
        private Card[] discard= new Card[5];
        public ICard[] Discard {get; set;}
        public HandType HandType {get=>hands.HandType;}
        private Hands hands=new Hands();
        public ICard[] Hand{get=>hands.Hand;}

        public Player(string name_)
        {
            this.name = name_;
            wins=0;

        }
        public Hands Hands
        {
            get { return hands; }
            set { value = hands; }
        }

        public Card[] Discard_
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
            hands.SortHand();
        }

        // anropar metod i Hand för att tömma index på hand som ska bort
        // LÄgger till det borttagna kortet i array discard
        public void ReceiveCards(Card card)
        {
            hands.AddCardToHand(card);
        }
        
        public void DiscardCard()
        {            
           //Hand = Hand.Except(Discard).ToArray();          
        }

        public void Win()
        {
            Wins++;
        }
    }
}