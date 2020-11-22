using System.Collections.Generic;
namespace Poker
{
    class Table
    {
        private Deck deck{get;set;}
        public List<Player> Players=> players;
        private List<Player> players;
        internal Deck Deck { get => deck; set => deck = value; }
        private List<Card> discardedCards;

        public Table()
        {
            players= new List<Player>();//ÄNDRAD
            discardedCards= new List<Card>();
            deck=new Deck();
            deck.Shuffle();
        }
        public void AddPlayerToTable(string name)
        {
            if(name !=null)
            {
                players.Add(new Player(name));
            }
        }
        

        // Delar ut ett kort i taget fem gånger till alla spelare
        public void DealTable()
        {
            for(int i=0; i<5; i++)
            {
                foreach(Player player in players)
                {
                    player.ReceiveCards(Deck.GetTopCard());
                }
            }
        }

        public void ReplacementCards(Player player, int nrOfCards)
        {
            for(int i=0;i<nrOfCards; i++)
            {
                player.ReceiveCards(deck.GetTopCard());
            }
        }

        public void DiscardedCardPile()
        {
            foreach(Player player in players)
            {
                foreach(Card card in player.Discard_)
                {
                    discardedCards.Add(card);
                    player.Discard_.RemoveAt(0);
                }
            }
        }

        public void RebuildDeck()
        {
            foreach(Card card in discardedCards)
            {
                deck.PutBackCard(card);
            }
            foreach (Player player in players)
            {
                foreach (Card card in player.Hand)
                {
                    deck.PutBackCard(card);
                }
            }
        }
    }
}