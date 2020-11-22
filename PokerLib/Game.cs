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

        public IPlayer[] Players { get => table.Players; set => Players = table.Players; }//ÄNDRAD
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

        public Game(string[] playerNames)
        {
            table = new Table(playerNames.Length);
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < playerNames.Length; j++)
                {
                    table.AddPlayerToTable(playerNames[j]);
                }
            }
        }

        public void RunGame()
        {
            while (true)
            {
                Deck deck = new Deck();
                Hands Hand = new Hands();
                deck.Shuffle(deck);
                NewDeal();
                table.DealTable(Players);
                foreach (Player player in Players)
                {
                    Hand.SortHand();
                    Hand.Eval();
                    SelectCardsToDiscard(player);
                    player.DiscardCard();
                    player.ReceiveCards(deck.GetTopCard());
                    RecievedReplacementCards(player);
                    Hand.SortHand();
                    Hand.Eval();
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

            List<Player> BestHandTypeTied = new List<Player>();
            HandType BestHand = HandType.HighCard;

            foreach (Player player in Players)
            {
                player.DetermineHandType(player.Hands);
                player.Hands.CompareTo(player.HandType);
            }
            Array.Sort(Players);
            if (Players[0].HandType > Players[1].HandType)
            {
                //Players[0] = this.Player;
                //this.Player.Win();
                Winner(Players[0]);
            }
            else
            {
                foreach (Player player in Players)
                {
                    if (player.HandType == Players[0].HandType)
                    {
                        BestHandTypeTied.Add(player);
                    }
                }
            }
            if (BestHandTypeTied.Count > 1)
            {
                if (BestHand == HandType.Pair || BestHand == HandType.ThreeOfAKind || BestHand == HandType.FourOfAKind)
                {
                    BestHandTypeTied = BestDuplicate(BestHandTypeTied);
                }
                else if (BestHand == HandType.HighCard || BestHand == HandType.Straight || BestHand == HandType.Flush || BestHand == HandType.StraightFlush)
                {
                    BestHandTypeTied = HighestRankCards(BestHandTypeTied);
                }
                else if (BestHand == HandType.TwoPairs)
                {
                    for (int i = 0; i < 2; i++)
                    {
                        Rank highestRank = BestHandTypeTied.Select(player => player.Hands.DuplicateRank[i]).Max();
                        BestHandTypeTied = BestHandTypeTied.Where(player => player.Hands.DuplicateRank[i] == highestRank).ToList();
                        if (BestHandTypeTied.Count == 1) break;
                    }
                    if (BestHandTypeTied.Count > 1)
                    {
                        BestHandTypeTied = HighestRankCards(BestHandTypeTied);
                    }
                }
                else if (BestHand == HandType.FullHouse)
                {
                    BestHandTypeTied = BestThreeDuplicate(BestHandTypeTied);
                    if (BestHandTypeTied.Count > 1)
                    {
                        BestHandTypeTied = BestDuplicate(BestHandTypeTied);
                    }
                }
                else if (BestHand == HandType.RoyalStraightFlush)
                {
                    Draw(BestHandTypeTied.ToArray());
                }
                foreach (Player player in Players)
                {
                    if (BestHandTypeTied.Count == 1)
                    {
                        BestHandTypeTied[0].Win();
                        Winner(BestHandTypeTied[0]);
                    }
                    else
                    {
                        Draw(BestHandTypeTied.ToArray());
                    }
                }
            }
        }

        private List<Player> HighestRankCards(List<Player> players)
        {
            for (int i = 0; i < 5; i++)
            {
                Rank highest = players.Select(player => player.Hands.CardRank[i]).Max();
                players = players.Where(player => player.Hands.CardRank[i] == highest).ToList();
                if (players.Count == 1) break;
            }
            return players;
        }

        private List<Player> BestDuplicate(List<Player> players)
        {
            Rank BestDuplicate = players.Select(player => player.Hands.DuplicateRank.First()).Max();
            players = players.Where(player => player.Hands.DuplicateRank.First() == BestDuplicate).ToList();
            if (players.Count > 1)
            {
                players = HighestRankCards(players);
            }
            return players;
        }

        private List<Player> BestThreeDuplicate(List<Player> players)
        {
            Rank BestThreeDuplicate = players.Select(player => player.Hands.ThreeDuplicateRank.First()).Max();
            players = players.Where(player => player.Hands.ThreeDuplicateRank.First() == BestThreeDuplicate).ToList();
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