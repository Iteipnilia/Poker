using System;

namespace Poker
{
    class Table
    {
<<<<<<< HEAD
        private Deck deck;
        private Player[] players;
        private Card[] playersDiscardedCards;
        private int numberOfPlayers;

        public Table(int numberOfPlayers_)// number of players som inparameter???
        {
            deck=new Deck();
            players= new Player[numberOfPlayers_];
            playersDiscardedCards= new Card[52];
            numberOfPlayers=numberOfPlayers_;
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
=======
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
>>>>>>> bc01e700bceb1e64a7c0bf601c5f6f9725713b66
            }
        }
    }
}