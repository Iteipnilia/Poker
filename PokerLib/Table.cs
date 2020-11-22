namespace Poker
{
    class Table
    {
        private Deck deck{get;set;}

        public Player[] Players=> players;

        internal Deck Deck { get => deck; set => deck = value; }

        private Player[] players;
        private Card[] DiscardedCards;

        public Table(int nrOfPlayers)
        {
            players= new Player[nrOfPlayers];//ÄNDRAD
            DiscardedCards= new Card[52];
            deck=new Deck();
        }
        public void AddPlayerToTable(string name)
        {
            for(int i=0; i<players.Length; i++)
            {
                if(players[i]==null)
                {
                    players[i]=new Player(name);
                    //break;
                }
            }
        }
        

        // Delar ut ett kort i taget fem gånger till alla spelare
        public void DealTable()
        {
            for(int i=0; i<5; i++)
            {
                for(int j=0; j<Players.Length; j++)
                {
                    players[j].ReceiveCards(Deck.GetTopCard());//funkar ej
                }
            }
        }

        public void ReplacementCards(Player player, int nrOfCards)
        {
            for(int i=0;i<nrOfCards; i++)
            {
                player.ReceiveCards(Deck.GetTopCard());
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