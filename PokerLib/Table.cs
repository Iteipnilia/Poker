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

        //=====================================================
        // ADD PLAYER: First method, Adds a new Player object 
        // to table<List> if the name is not null and the list
        // contains of less than five objects.
        //
        // Second method, also takes number of wins and is 
        // called when resuming a saved game.
        //=====================================================
        public void AddPlayerToTable(string name)
        {
            if (name != null && players.Count < 5)
            {
                players.Add(new Player(name));
            }
        }
        public void AddPlayerToTable(string name, int wins)
        {
            if (name != null && players.Count < 5)
            {
                players.Add(new Player(name, wins));
            }
        }

        //=======================================================
        // DEAL TABLE: Deals one card at the time, five times to
        // each player in the list of players.
        //=======================================================
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

        //===========================================
        // REPLACEMENTCARDS: Gives a specific player,
        // a specific number of new cards, depending 
        // on how many the player has discarded.
        //===========================================
        public void ReplacementCards(Player player, int nrOfCards)
        {
            for (int i = 0; i < nrOfCards; i++)
            {
                player.ReceiveCards(deck.GetTopCard());
            }
        }

        //====================================================
        // DISCARD CARD: Removes choosen card from
        // a players hand and adds it to discardedCard<List>,
        // wich holds all players discarded cards
        //====================================================
        public void DiscardCard(Player player, Card card)
        {
            player.Hands.RemoveCard(card);
            discardedCards.Add(card);
        }

        //==========================================================
        // REBUILD DECK: Collects all cards from each players hand,
        // adds the cards to discardedCards<List>, via DiscardCard()
        // Then puts every card from discardedCards<List> into Deck.
        // And finally clears discardedCards<List> of all its objects
        //===========================================================
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