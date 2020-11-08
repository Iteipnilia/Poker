namespace Poker
{
    class Player
    {
        private string name;
        private int wins;
        private Hands hand;
        private Card[] discard;

        public Player(string name_)
        {
            name=name_;
            wins=0;
            hand= new Hands();
            discard= new Card[5];

        }

        public Hands Hand
        {
            get { return hand; }
            set { value = hand; }
        }

        public Card[] Discard
        {
            get { return discard; }
            set { value = discard; }
        }

        //public string Name=>name;
        //public int Wins=>wins;

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