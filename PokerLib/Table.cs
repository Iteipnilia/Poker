namespace Poker
{
    class Table
    {
        private Deck deck;

        public Player[] Players=> players;

        internal Deck Deck { get => deck; set => deck = value; }

        private Player[] players;
        private Card[] DiscardedCards;

        public Table()
        {
            players= new Player[Players.Length];
            DiscardedCards= new Card[52];
        }
        

        // Delar ut ett kort i taget fem gånger till alla spelare
        public void DealTable(IPlayer[] Player)
        {
            for(int i=0; i<5; i++)
            {
                for(int j=0; j<Players.Length; j++)
                {
                    Deck.GetTopCard();
                }
            }
        }

        public void ReplacementCards(Player player, int nrOfCards)
        {
            for(int i=0;i<nrOfCards; i++)
            {
                Deck.GetTopCard();
            }
        }

        public void DiscardedCardPile(int playerIndex)
        {
            foreach(var card in players[playerIndex].Discard_)
            {
                for(int i=0; i<DiscardedCards.Length; i++)
                {
                    if(DiscardedCards[i]==null)
                    {
                        DiscardedCards[i]=card;
                        players[playerIndex].Discard=null;// tar den bort alla kort eller bara ett???
                        break;
                    }
                }             
            }
        }

        public void RebuildDeck()
        {
            foreach(Card card in DiscardedCards)
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