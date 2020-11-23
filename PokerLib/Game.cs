using System;
using System.IO;
using System.Linq;
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
        public IPlayer[] Players { get=>table.Players.ToArray(); set=>Players=table.Players.ToArray();}//ÄNDRAD
        private Table table;//=new Table();//ÄNDRAD

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

        public Game(string[] playerNames)//ÄNDRAD
        {
            table = new Table();
            foreach(string name in playerNames)
            {
                table.AddPlayerToTable(name);
            }
        }

        public void RunGame()
        {
            while (true)
            {
                NewDeal();
                foreach (Player player in Players)
                {
                    table.DealTable(player);
                    player.SortPlayerHand(player.Hand);
                    SelectCardsToDiscard(player);
                    foreach (Card card in player.Discard)
                    {
                        player.DiscardCard(card);
                        player.ReceiveCards(table.Deck.GetTopCard());
                    }
                    RecievedReplacementCards(player);
                    player.SortPlayerHand(player.Hand);
                    player.Hand.Eval();
                }
                ShowAllHands();
                CompareHands();
                table.RebuildDeck();
            } 
        }

        public void CompareHands()
        {
            // Varje spelares handtype jämförs
            // bästa handen vinner
            // Om två eller fler spelare har samma hand, jämförs rank
            // Om två spelare har samma rank: oavgjort

            List<Player> BestHand = new List<Player>();
            HandType BestHandType = HandType.HighCard;
            
            foreach(Player player in Players)
            {
                if ((int)player.Hand.HandType > (int)BestHandType)
                {
                    BestHandType = player.Hand.HandType;
                    BestHand.Clear();
                }
                if ((int)player.Hand.HandType >= (int)BestHandType)
                {
                    BestHand.Add(player);
                }
            }
            if (BestHand.Count == 1)
            {
                BestHand[0].Win();
                Winner(BestHand[0]);
            }
        
            if (BestHand.Count > 1)
            {
                if (BestHandType == HandType.Pair || BestHandType == HandType.ThreeOfAKind || BestHandType == HandType.FourOfAKind)
                {
                    BestHand = BestDuplicate(BestHand);
                }
                else if (BestHandType == HandType.HighCard || BestHandType == HandType.Straight || BestHandType == HandType.Flush || BestHandType == HandType.StraightFlush)
                {
                    BestHand = HighestRankCards(BestHand);
                }
                else if (BestHandType == HandType.TwoPairs)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Rank highestRank = BestHand.Select(player => player.Hand.DuplicateRank[i]).Max();
                        BestHand = BestHand.Where(player => player.Hand.DuplicateRank[i] == highestRank).ToList();
                        if (BestHand.Count == 1) break;
                    }
                    if (BestHand.Count > 1)
                    {
                        BestHand = HighestRankCards(BestHand);
                    }
                }
                 else if (BestHandType == HandType.FullHouse)
                {
                    BestHand = BestThreeDuplicate(BestHand);
                    if (BestHand.Count > 1)
                    {
                        BestHand = BestDuplicate(BestHand);
                    }
                }
                else if (BestHandType == HandType.RoyalStraightFlush)
                {
                    Draw(BestHand.ToArray());
                }
                foreach (Player player in Players)
                {
                    if (BestHand.Count == 1)
                    {
                        BestHand[0].Win();
                        Winner(BestHand[0]);
                    }
                    else
                    {
                        Draw(BestHand.ToArray());
                    }
                }
            }
        }

        private List<Player> HighestRankCards(List<Player> Players)
        {
            for (int i = 0; i < 5; i++)
            {
                Rank highest = Players.Select(player => player.Hand.CardRank[i]).Max();
                Players = Players.Where(player => player.Hand.CardRank[i] == highest).ToList();
                if (Players.Count == 1) break;
            }
            return Players;
        }

        private List<Player> BestDuplicate(List<Player> players)
        {
            Rank BestDuplicate = players.Select(player => player.Hand.DuplicateRank.First()).Max();
            players = players.Where(player => player.Hand.DuplicateRank.First() == BestDuplicate).ToList();
            if (players.Count > 1)
            {
                players = HighestRankCards(players);
            }
            return players;
        }

        private List<Player> BestThreeDuplicate(List<Player> players)
        {
            Rank BestThreeDuplicate = players.Select(player => player.Hand.ThreeDuplicateRank.First()).Max();
            players = players.Where(player => player.Hand.ThreeDuplicateRank.First() == BestThreeDuplicate).ToList();
            if (players.Count > 1)
            {
                players = HighestRankCards(players);
            }
            return players;
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