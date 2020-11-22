namespace Poker
{
    class Table
    {
        private Deck deck;
        public Player[] Players => players;
        internal Deck Deck { get => deck; set => deck = value; }
        private Player[] players;
        private ICard[] DiscardedCards;

        public Table(int nrOfPlayers)
        {
            players = new Player[nrOfPlayers];//ÄNDRAD
            DiscardedCards = new ICard[52];
            Deck = new Deck();
        }


        public void AddPlayerToTable(string name)
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i] == null)
                {
                    players[i] = new Player(name);
                }
            }
        }

        // Delar ut ett kort i taget fem gånger till alla spelare
        public void DealTable(IPlayer[] Player)
        {
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < Players.Length; j++)
                {
                    Deck.GetTopCard();
                }
            }
        }

        public void ReplacementCards(Player player, int nrOfCards)
        {
            for (int i = 0; i < nrOfCards; i++)
            {
                 //Player.ReceiveCards(Deck.GetTopCard());
            }
        }

        public void DiscardedCardPile(int playerIndex)
        {
            foreach (var card in players[playerIndex].Discard_)
            {
                for (int i = 0; i < DiscardedCards.Length; i++)
                {
                    if (DiscardedCards[i] == null)
                    {
                        DiscardedCards[i] = card;
                        players[playerIndex].Discard = null;// tar den bort alla kort eller bara ett???
                        break;
                    }
                }
            }
        }

        public void RebuildDeck()
        {
            foreach (ICard card in DiscardedCards)
            {
                Deck.PutBackCard(card);
            }
            foreach (Player player in players)
            {
                foreach (ICard card in player.Hand)
                {
                    Deck.PutBackCard(card);
                }
            }
        }
    }
}