using System;

namespace Poker
{
    class Table
    {
        private Deck deck;
        private Player[] players;
        private Card[] playersDiscardedCards;
        private int numberOfPlayers;

        public Table()// number of players som inparameter???
        {
            deck=new Deck();
            players= new Player[numberOfPlayers_];
            playersDiscardedCards= new Card[52];
        }

        public Player[] Players=> players;
        
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

        public Hands ShowHands(Player player)// ta bort inparameter
        {
            return player.Hands;
        }

        // Delar ut ett kort i taget fem gånger till alla spelare
        public void DealTable(Player player)
        {
            for(int i=0; i<5; i++)
            {
                for(int j=0; j<numberOfPlayers; numberOfPlayers++)
                {
                    player.ReceiveCards(deck.GetTopCard());
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
        public void CompareHands()
        {
            // Varje spelares handtype jämförs
            // bästa handen vinner
            // Om två eller fler spelare har samma hand, jämförs rank
            // Om två spelare har samma rank: oavgjort
            
            foreach(Player player in players)
            {
                player.DetermineHandType(player.Hands);
            }
            
            HandType tempHand=0;
            for(int i=0; i<numberOfPlayers; i++)
            {
                if(players[i].HandType>tempHand)
                {
                    tempHand=players[i].HandType;
                }
            }
        }

        public void DiscardedCardPile(int playerIndex)
        {
            foreach(var card in players[playerIndex].Discard_)
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

        public void CollectCardsFromTable()
        {
            foreach(Card card in playersDiscardedCards)
            {
                deck.PutBackCard(card);
            }
        }
    }
}