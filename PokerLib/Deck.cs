namespace Poker
{
    class Deck
    {
        
        private Card[] cards;

        public Deck()
        {
            cards= new Card[52];

            for (int suite = 0; suite < 4;  suite++)// Använd en foreach istället??
            {
                for (int rank = 2; rank < 15; rank++)
                {
                    cards[suite * 13 + rank - 2]  = new Card((Suite)suite,(Rank)rank);
                }
            }
        }

        public Card GetTopCard()
        {
            int i=0;
            Card drawnCard;
            do
            {
               i++;
            }
            while(cards[i] ==null);
            
            drawnCard= cards[i];
            cards[i]=null; // blir den tom???
            
            return drawnCard;
        }

        public void PutBackCard(Card card)
        {
            for(int i=0; i< cards.Length; i++)
            {
                if(cards[i]==null)
                {
                    cards[i]=card;
                }
            }
        }
    }
}