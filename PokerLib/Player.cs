namespace Poker
{
    class Player: IPlayer
    {
        private string name;
        public string Name{get=> name;}
        private int wins;
        public int Wins{get=> wins;}
        private Card[] discard= new Card[5];
        public ICard[] Discard {set => Discard = discard;}
        public HandType HandType {get=>hand.HandType;}
        private Hands hand{get; set;}
        public ICard[] Hand{get=> Hands.Hand;}

        public Player(string name_)
        {
            name=name_;
            wins=0;
            hand= new Hands();
            //discard= new Card[5];

        }
        public Hands Hands
        {
            get { return hand; }
            set { value = hand; }
        }

        public Card[] Discard_
        {
            get { return discard; }
            set { value = discard; }
        }

        // anropar metod i Hand för att tömma index på hand som ska bort
        // LÄgger till det borttagna kortet i array discard
        public void DiscardCard(int index)
        {            
           discard[index] = hand.RemoveCardFromHand(index);
        }

        public void ReceiveCards(Card card)
        {
            hand.AddCardToHand(card);
        }
    }
}