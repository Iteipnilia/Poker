using System;

namespace Poker
{
class Dealer
    {
        private Deck deck;
        private Card[] cards;
        public Dealer()
        {
            deck= new Deck();
        }
        public void PutBackCardsInDeck(Card card)
        {
            deck.PutBackCard(card);
        }
        public Card GiveCard()
        {
           return deck.GetTopCard();
        }
    public void Shuffle()//Fisher-Yates
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
    }    
}