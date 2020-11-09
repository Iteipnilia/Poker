using System;

namespace Poker
{
    class Table
    {
        private Dealer dealer;
        private Player[] players;
        private Card[] playersDiscardedCards;
        private int numberOfPlayers;

        public Table(int numberOfPlayers_)// number of players som inparameter???
        {
            dealer=new Dealer();
            players= new Player[numberOfPlayers_];
            playersDiscardedCards= new Card[52];
            numberOfPlayers=numberOfPlayers_;
        }
        
        public void AddPlayerToTable(string name)
        {
            for(int i=0; i<numberOfPlayers; i++)
            {
                if(players[i]==null)
                {
                    players[i]=new Player(name);
                    break;
                }
            }
        }

        public Hands ShowHands(Player player)
        {
            return player.Hand;
        }

        // Delar ut ett kort i taget fem gånger till alla spelare
        public void DealTable(Player player)
        {
            for(int i=0; i<5; i++)
            {
                for(int j=0; j<numberOfPlayers; numberOfPlayers++)
                {
                    player.ReceiveCards(dealer.GiveCard());
                }
            }
        }

        public void ReplacementCards(Player player, int nrOfCards)
        {
            for(int i=0;i<nrOfCards; i++)
            {
                player.ReceiveCards(dealer.GiveCard());
            }
        }
        public void CompareHands()
        {
            // Varje spelares handtype jämförs
            // bästa handen vinner
            // Om två eller fler spelare har samma hand, jämförs rank
            // Om två spelare har samma rank: oavgjort
        }

        public void DiscardedCardPile(int playerIndex)
        {
            foreach(var card in players[playerIndex].Discard)
            {
                for(int i=0; i<playersDiscardedCards.Length; i++)
                {
                    if(playersDiscardedCards[i]==null)
                    {
                        playersDiscardedCards[i]=card;
                        players[playerIndex].Discard=null;// tar den bort alla kort eller bara ett???
                        break;
                    }
                }             
            }
        }

        public void DealerCollectCards()
        {
            foreach(Card card in playersDiscardedCards)
            {
                dealer.PutBackCardsInDeck(card);
            }
        }
    }
}