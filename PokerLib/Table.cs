using System.Linq;
using System.Collections.Generic;
namespace Poker
{
    class Table : Player
    {
        public Deck Deck{get;set;}
        public List<IPlayer> Players=> players;
        private List<IPlayer> players;
        private List<Card> discardedCards;
        private const int CardPerPlayer = 5;

        public Table()
        {
            players= new List<IPlayer>();//ÄNDRAD
            discardedCards= new List<Card>();
            Deck = new Deck();
        }
        public void AddPlayerToTable(string name)
        {
            if(name !=null)
            {
                {
                players.Add(new Player{Name = name});
                }
            }
        }
        // Delar ut ett kort i taget fem gånger till alla spelare
        public void DealTable(Player player)
        {
            for(int i=1; i<=CardPerPlayer; i++)
            {
                player.ReceiveCards(Deck.GetTopCard());
            }
        }

        public void ReplacementCards(Player player)
        {
            while (player.Hand.Count() < CardPerPlayer)
            {
                player.ReceiveCards(Deck.GetTopCard());
            }
        }

        public void DiscardedCardPile()
        {
            foreach(Player player in players)
            {
                foreach(Card card in player.Discard)
                {
                    discardedCards.Add(card);
                }
                player.Discard=null;
            }
        }

        public void RebuildDeck()
        {
            foreach(Card card in discardedCards)
            {
                Deck.PutBackCard(card);
            }
            foreach (Player player in players)
            {
                foreach (Card card in player.Hand)
                {
                    Deck.PutBackCard(card);
                }
            }
        }
    }
}