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
            while(cards[i] ==null&&i<cards.Length){i++;}

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
<<<<<<< HEAD
        
        //Flytta Shuffle() till Dealer??
        // Ha deck som inparameter??
        // ändra cards i metoden till deck
        public void Shuffle(Deck deck)//Fisher-Yates
        {
            Random random= new Random();

            for (int i = cards.Length - 1; i > 0; i--)
            {
                 int randomIndex = random.Next(0, i + 1);
 
                Card temp = cards[i];// temporärt kort = 1a index
                cards[i] = cards[randomIndex]; // kort på 1a index blir kortet på random index
                cards[randomIndex] = temp; // kort på random index blir kortet från 1a index
            }
        }
=======
>>>>>>> bc01e700bceb1e64a7c0bf601c5f6f9725713b66
    }
}