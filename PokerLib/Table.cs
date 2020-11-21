using System;
using System.Collections.Generic;
using System.Linq;

namespace Poker
{
    class Table
    {
        private Deck deck;
        public Player[] Players=> players;
        private Player[] players;
        private Card[] playersDiscardedCards;

        public Table()
        {
            players= new Player[Players.Length];
            playersDiscardedCards= new Card[52];
        }
        

        // Delar ut ett kort i taget fem gånger till alla spelare
        public void DealTable(Player player)
        {
            for(int i=0; i<5; i++)
            {
                for(int j=0; j<Players.Length; j++)
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

        public void RebuildDeck()
        {
            foreach(Card card in playersDiscardedCards)
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