using System;
using System.Collections.Generic;
namespace Poker
{
    class Deck
    {

        List<Card> cards = new List<Card>(52);

        public Deck()
        {

            for (int suite = 0; suite < 4; suite++)// Använd en foreach istället??
            {
                for (int rank = 2; rank < 15; rank++)
                {
                    cards[suite * 13 + rank - 2] = new Card((Suite)suite, (Rank)rank);
                }
            }
        }

        public Card GetTopCard()
        {
            Card drawnCard = cards[0];
                cards.RemoveAt(0);
                return drawnCard;
        }

        public void PutBackCard(Card card)
        {
            for (int i = cards.Count;)
            {
                if (cards == null)
                {
                    cards = card;
                }
            }
        }

        public void Shuffle()//Fisher-Yates
        {
            Random random = new Random();
            {  
            int c = cards.Count;  
            while (c > 1)
            {  
                c--;  
                int k = random.Next(c + 1);  
                Card value = cards[k];  
                cards[k] = cards[c];  
                cards[c] = value;  
            }  
        }
    }
}