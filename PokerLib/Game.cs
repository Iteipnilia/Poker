using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using Poker.Lib;
using System.Runtime.CompilerServices;
[assembly:InternalsVisibleTo("Poker.Lib.UnitTest")]

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
        public IPlayer[] Players { get => table.Players.ToArray(); set => Players = table.Players.ToArray(); }
        private List<Player> players = new List<Player>();
        private Table table;

        public Game(string fileName)
        {
            if (File.Exists(fileName))
            {
                table = new Table();
                string json = File.ReadAllText(fileName);
                string[] data = json.Split(' ');
                string[] names = JsonConvert.DeserializeObject<String[]>(data[0]);
                int[] wins = JsonConvert.DeserializeObject<int[]>(data[1]);

                for (int i = 0; i < names.Length; i++)
                {
                    table.AddPlayerToTable(names[i], wins[i]);
                }
            }
        }
 
        public Game(string[] playerNames)
        {
            table = new Table();
            if (playerNames.Length <1)
            {
                table.AddPlayerToTable("player1");
                table.AddPlayerToTable("player2");
            }
            else
            {
                for (int i = 0; i < playerNames.Length && i < 5; i++)
                {
                    table.AddPlayerToTable(playerNames[i]);
                }
                if (playerNames.Length < 2) { table.AddPlayerToTable("player"); }
            }
        }

        public void RunGame()
        {
            while (true)
            {
                NewDeal();
                table.DealTable();
                foreach (Player player in Players)
                {
                    player.SortPlayerHand();
                    SelectCardsToDiscard(player);
                    foreach (Card card in player.Discard)
                    {
                        table.DiscardCard(player, card);
                    }
                    table.ReplacementCards(player, player.Discard.Length);
                    player.SortPlayerHand();
                    RecievedReplacementCards(player);
                    player.Hands.Eval();
                }
                ShowAllHands();
                CompareHands(Players);
                table.RebuildDeck();
            }
        }

        public void CompareHands(IPlayer[] Players)
        {
            // Varje spelares handtype jämförs
            // bästa handen vinner
            // Om två eller fler spelare har samma hand, jämförs rank
            // Om två spelare har samma rank: oavgjort
            List<Player> BestHand = new List<Player>();
            HandType BestHandType = HandType.HighCard;
            foreach (Player player in Players)
            {
                if ((int)player.Hands.HandType > (int)BestHandType)
                {
                    BestHandType = player.Hands.HandType;
                    BestHand.Clear();
                }
                if ((int)player.Hands.HandType >= (int)BestHandType)
                {
                    BestHand.Add(player);
                }
            }
            if (BestHand.Count == 1)
            {
                BestHand[0].Win();
                Winner?.Invoke(BestHand[0]);
            }
            else if (BestHand.Count > 1)
            {
                if (BestHandType == HandType.Pair)
                {
                    BestHand = BestDuplicate(BestHand);
                }
                else if (BestHandType == HandType.HighCard || BestHandType == HandType.Straight ||
                            BestHandType == HandType.Flush || BestHandType == HandType.StraightFlush)
                {
                    BestHand = HighestRankCards(BestHand);
                }
                else if (BestHandType == HandType.TwoPairs)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Rank highestRank = BestHand.Select(player => player.Hands.DuplicateRank[i]).Max();
                        BestHand = BestHand.Where(player => player.Hands.DuplicateRank[i] == highestRank).ToList();
                        if (BestHand.Count == 1) break;
                    }
                    if (BestHand.Count > 1)
                    {
                        BestHand = HighestRankCards(BestHand);
                    }
                }
                else if (BestHandType == HandType.ThreeOfAKind || BestHandType == HandType.FullHouse)
                {
                    BestHand = BestThreeDuplicate(BestHand);
                    if (BestHand.Count > 1)
                    {
                        BestHand = BestDuplicate(BestHand);
                    }
                }
                else if (BestHandType == HandType.FourOfAKind)
                {
                    BestHand = BestFourDuplicate(BestHand);
                }
                else if (BestHandType == HandType.RoyalStraightFlush)
                {
                    Draw?.Invoke(BestHand.ToArray());
                }

                if (BestHand.Count == 1)
                {
                    BestHand[0].Win();
                    Winner?.Invoke(BestHand[0]);
                }
                else
                {
                    Draw?.Invoke(BestHand.ToArray());
                }
            }
        }

        public List<Player> HighestRankCards(List<Player> players)
        {
            for (int i = 0; i < 5; i++)
            {
                Rank highest = players.Select(player => player.Hands.CardRank[i]).Max();
                players = players.Where(player => player.Hands.CardRank[i] == highest).ToList();
                if (players.Count == 1) break;
            }
            return players;
        }

        public List<Player> BestDuplicate(List<Player> players)
        {
            Rank BestDuplicate = players.Select(player => player.Hands.DuplicateRank.First()).Max();
            players = players.Where(player => player.Hands.DuplicateRank.First() == BestDuplicate).ToList();
            if (players.Count > 1)
            {
                players = HighestRankCards(players);
            }
            return players;
        }

        public List<Player> BestThreeDuplicate(List<Player> players)
        {
            Rank BestThreeDuplicate = players.Select(player => player.Hands.ThreeDuplicateRank.First()).Max();
            players = players.Where(player => player.Hands.ThreeDuplicateRank.First() == BestThreeDuplicate).ToList();
            if (players.Count > 1)
            {
                players = HighestRankCards(players);
            }
            return players;
        }
        public List<Player> BestFourDuplicate(List<Player> players)
        {
            Rank BestFourDuplicate = players.Select(player => player.Hands.FourDuplicateRank.First()).Max();
            players = players.Where(player => player.Hands.FourDuplicateRank.First() == BestFourDuplicate).ToList();
            if (players.Count > 1)
            {
                players = HighestRankCards(players);
            }
            return players;
        }

        public void SaveGameAndExit(string fileName)
        {
            string[] names;
            int[] wins;

            names = new string[Players.Length];
            wins = new int[Players.Length];

            for (int i = 0; i < Players.Length; i++)
            {
                names[i] = Players[i].Name;
                wins[i] = Players[i].Wins;
            }

            string json = JsonConvert.SerializeObject(names);
            json += (" " + JsonConvert.SerializeObject(wins));
            File.WriteAllText(fileName, json);
            Environment.Exit(0);
        }

        public void Exit()
        {
            Environment.Exit(0);
        }
    }
}