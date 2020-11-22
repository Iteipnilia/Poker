using System;
using System.Collections.Generic;
namespace Poker
{
    class Deck
    {

        private List<Card> cards;

        public Deck()
        {
            cards = new List<Card>(52);

            foreach(Suite s in Enum.GetValues(typeof(Suite)))
            {
                foreach(Rank r in Enum.GetValues(typeof(Rank)))
                {
                    cards.Add(new Card((Suite)s, (Rank)r));
                }
            }
        }

        public Card GetTopCard()
        {
            Card drawnCard;

            drawnCard = cards[0];
            cards.RemoveAt(0); // blir den tom???

            return drawnCard;
        }

        public void PutBackCard(Card card)
        {
            for (int i = 0; i < cards.Count; i++)
            {
                if (cards[i] == null)
                {
                    cards[i] = card;
                }
            }
        }

        public void Shuffle()//Fisher-Yates
        {
            Random random = new Random();

            for (int i = cards.Count - 1; i > 0; i--)
            {
                int randomIndex = random.Next(0, i + 1);

                Card temp = cards[i];// temporärt kort = 1a index
                cards[i] = cards[randomIndex]; // kort på 1a index blir kortet på random index
                cards[randomIndex] = temp; // kort på random index blir kortet från 1a index
            }
        }
    }
}