using System.Collections.Generic;

namespace Poker
{
    class Table
    {
        private Deck deck{get;set;}
        private List<Player> players;
        public List<Player> Players{get=> players; set =>players=value;}
        private List<Card> discardedCards;


        public Table()
        {
            players= new List<Player>();
            discardedCards= new List<Card>();
            deck=new Deck();
            deck.Shuffle();
        }
        public void AddPlayerToTable(string name)
        {
            if (name != null)
            {
                players.Add(new Player(name));
            }
        }
        public void AddPlayerToTable(string name, int wins)
        {
            if (name != null)
            {
                players.Add(new Player(name, wins));
            }
        }

        // Delar ut ett kort i taget fem gånger till alla spelare
        public void DealTable()
        {
            for (int i = 0; i < 5; i++)
            {
                foreach (Player player in players)
                {
                    player.ReceiveCards(deck.GetTopCard());
                }
            }
        }

        public void ReplacementCards(Player player, int nrOfCards)
        {
            for (int i = 0; i < nrOfCards; i++)
            {
                player.ReceiveCards(deck.GetTopCard());
            }
        }

        public void DiscardCard(Player player, Card card)
        {
            player.Hands.RemoveCard(card);
            discardedCards.Add(card);
        }

        public void RebuildDeck()
        {
            foreach (Player player in players)
            {
                foreach (Card card in player.Hand)
                {
                    DiscardCard(player,card);
                }
            }
            foreach (Card card in discardedCards)
            {
                deck.PutBackCard(card);
            }
            discardedCards.Clear();         
        }
    }
}