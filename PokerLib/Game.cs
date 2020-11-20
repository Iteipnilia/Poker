using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using Poker.Lib;
namespace Poker
{
    class Game : IPokerGame
    {
        public event OnNewDeal NewDeal;
        public event OnSelectCardsToDiscard SelectCardsToDiscard;
        public event OnRecievedReplacementCards RecievedReplacementCards;
        public event OnShowAllHands ShowAllHands;
        public event OnWinner Winner;
        public event OnDraw Draw;

        public IPlayer[] Players { get; set;}

        public Game(string fileName)
        {
            if (File.Exists(fileName))
            {
                string json = File.ReadAllText(fileName);
                List<Player> players = JsonConvert.DeserializeObject<List<Player>>(json);
                this.Players = new Player[players.Count];
                this.Players = players.ToArray();
            }
        }

        public Game(string[] playerNames)
        {
            Table.AddPlayerToTable(playerNames);
        }

        public void RunGame()
        {
            Deck deck = new Deck();
            Table table = new Table();
            Hands Hand = new Hands();
            deck.Shuffle(deck);
            NewDeal();
            foreach (Player player in Players)
            {
                table.DealTable(player);
                Hand.SortHand();
                Hand.Eval();
                SelectCardsToDiscard(player);
                player.DiscardCard(cards);
                player.ReceiveCards(card);
                RecievedReplacementCards(player);
                Hand.SortHand();
                Hand.Eval();
            }
            ShowAllHands();
            table.CompareHands();
            List<Player> win = Table.Result();
                if (win.Count == 1)
                {
                    if (Winner != null)
                    {
                        Winner(win[0]);
                        win[0].wins += 1;
                    }
                }
                else
                {
                    if (Draw != null)
                    {
                        Draw(win.ToArray());
                    }
                }    
            table.RebuildDeck();
            
        }
        public void SaveGameAndExit(string fileName)
        {
            string json = JsonConvert.SerializeObject(Players);
            File.WriteAllText(fileName, json);
            Environment.Exit(0);
        }

        public void Exit()
        {
            Environment.Exit(0);
        }
    }
}