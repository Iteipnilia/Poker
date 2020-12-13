using NUnit.Framework;
using Poker;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static Poker.Suite;
using static Poker.Rank;
using static Poker.HandType;

namespace Poker.Lib.UnitTest
{
    public class GameTest
    {
        private Game game;

        [SetUp]
        public void Setup()
        {
            game = new Game(new string[2] { "Test1", "Test2" });
        }

        [Test]
        public void GameCanSaveToFile()
        {

        }

        [Test]
        public void GameCanLoadFile()
        {
        }

        [Test]
        public void GameExitAfterSave()
        {
        }

        [Test]
        public void FindBestCard()
        {
            Player player1 = new Player("Test1", 0);
            Player player2 = new Player("Test2", 0);
            IPlayer[] Players = { player1, player2 };
            List<IPlayer> players = new List<IPlayer> { player1, player2 };
            Card[] hand1 = TestsHand.ToCards("♣4♥J♠Q♥K♥A");
            Card[] hand2 = TestsHand.ToCards("♥4♣7♥8♠9♥Q");
            foreach (Card cards in hand1)
            {
                player1.ReceiveCards(cards);
            }
            foreach (Card cards in hand2)
            {
                player2.ReceiveCards(cards);
            }

            foreach (Player player in players)
            {
                player.Hands.Eval();
            }
            game.CompareHands(Players);
            for (int i = 0; i < 5; i++)
            {
                Assert.GreaterOrEqual(player1.Hands.CardRank[i], player2.Hands.CardRank[i]);
            }
            Assert.Greater(player1.Wins, player2.Wins);
        }

        [Test]
        public void FindBestPair()
        {
            Player player1 = new Player("Test1", 0);
            Player player2 = new Player("Test2", 0);
            IPlayer[] Players = { player1, player2 };
            List<Player> players = new List<Player> { player1, player2 };
            Card[] hand1 = TestsHand.ToCards("♣3♥9♠10♠A♥A");
            Card[] hand2 = TestsHand.ToCards("♥4♣7♥8♠Q♥Q");
            foreach (Card cards in hand1)
            {
                player1.ReceiveCards(cards);
            }
            foreach (Card cards in hand2)
            {
                player2.ReceiveCards(cards);
            }

            foreach (Player player in players)
            {
                player.Hands.Eval();
            }
            game.CompareHands(Players);
            game.BestDuplicate(players);
            for (int i = 0; i < 1; i++)
            {
                Assert.Greater(player1.Hands.DuplicateRank[i], player2.Hands.DuplicateRank[i]);
            }
            Assert.Greater(player1.Wins, player2.Wins);
        }

        [Test]
        public void FindBestThreeOfAKind()
        {
            Player player1 = new Player("Test1", 0);
            Player player2 = new Player("Test2", 0);
            IPlayer[] Players = { player1, player2 };
            List<Player> players = new List<Player> { player1, player2 };
            Card[] hand1 = TestsHand.ToCards("♣7♥9♣A♠A♥A");
            Card[] hand2 = TestsHand.ToCards("♥4♣7♣Q♠Q♥Q");
            foreach (Card cards in hand1)
            {
                player1.ReceiveCards(cards);
            }
            foreach (Card cards in hand2)
            {
                player2.ReceiveCards(cards);
            }
            foreach (Player player in players)
            {
                player.Hands.Eval();
            }
            game.CompareHands(Players);
            game.BestThreeDuplicate(players);
            for (int i = 0; i < 1; i++)
            {
            Assert.Greater(player1.Hands.ThreeDuplicateRank[i], player2.Hands.ThreeDuplicateRank[i]);
            }
        }

        [Test]
        public void IfTiedHighCard()
        {
            Player player1 = new Player("Test1", 0);
            Player player2 = new Player("Test2", 0);
            IPlayer[] Players = { player1, player2 };
            List<Player> players = new List<Player> { player1, player2 };
            Card[] hand1 = TestsHand.ToCards("♣4♥J♠Q♥K♥A");
            Card[] hand2 = TestsHand.ToCards("♥4♣7♥8♠K♠A");
            foreach (Card cards in hand1)
            {
                player1.ReceiveCards(cards);
            }
            foreach (Card cards in hand2)
            {
                player2.ReceiveCards(cards);
            }

            foreach (Player player in players)
            {
                player.Hands.Eval();
            }
            game.CompareHands(Players);
            game.HighestRankCards(players);
            for (int i = 0; i < 5; i++)
            {
                Assert.GreaterOrEqual(player1.Hands.CardRank[i], player2.Hands.CardRank[i]);
            }
            Assert.Greater(player1.Wins, player2.Wins);
        }

        [Test]
        public void IfTiedPair()
        {
            Player player1 = new Player("Test1", 0);
            Player player2 = new Player("Test2", 0);
            IPlayer[] Players = { player1, player2 };
            List<Player> players = new List<Player> { player1, player2 };
            Card[] hand1 = TestsHand.ToCards("♣3♥9♠10♠A♥A");
            Card[] hand2 = TestsHand.ToCards("♥2♣7♥8♣A♦A");
            foreach (Card cards in hand1)
            {
                player1.ReceiveCards(cards);
            }
            foreach (Card cards in hand2)
            {
                player2.ReceiveCards(cards);
            }

            foreach (Player player in players)
            {
                player.Hands.Eval();
            }

            game.BestDuplicate(players);
            for (int i = 0; i < 1; i++)
            {
                Assert.GreaterOrEqual(player1.Hands.DuplicateRank[i], player2.Hands.DuplicateRank[i]);
                Assert.GreaterOrEqual(player1.Hands.CardRank[i], player2.Hands.CardRank[i]);
            }
            game.CompareHands(Players);
            Assert.Greater(player1.Wins, player2.Wins);
        }

        [Test]
        public void IfTiedTwoPairs()
        {
            Player player1 = new Player("Test1", 0);
            Player player2 = new Player("Test2", 0);
            IPlayer[] Players = { player1, player2 };
            List<Player> players = new List<Player> { player1, player2 };
            Card[] hand1 = TestsHand.ToCards("♣3♥9♠9♠A♥A");
            Card[] hand2 = TestsHand.ToCards("♥4♣9♦9♣A♦A");
            foreach (Card cards in hand1)
            {
                player1.ReceiveCards(cards);
            }
            foreach (Card cards in hand2)
            {
                player2.ReceiveCards(cards);
            }

            foreach (Player player in players)
            {
                player.Hands.Eval();
            }

            game.BestDuplicate(players);
            for (int i = 0; i < 1; i++)
            {
                Assert.GreaterOrEqual(player2.Hands.DuplicateRank[i], player1.Hands.DuplicateRank[i]);
                Assert.GreaterOrEqual(player2.Hands.CardRank[i], player1.Hands.CardRank[i]);
            }
            game.CompareHands(Players);
            Assert.Greater(player2.Wins, player1.Wins);
        }

        [Test]
        public void IfOnlyHighPairTiedTwoPairs()
        {
            Player player1 = new Player("Test1", 0);
            Player player2 = new Player("Test2", 0);
            IPlayer[] Players = { player1, player2 };
            List<Player> players = new List<Player> { player1, player2 };
            Card[] hand1 = TestsHand.ToCards("♣3♥10♠10♠A♥A");
            Card[] hand2 = TestsHand.ToCards("♥4♣9♦9♣A♦A");
            foreach (Card cards in hand1)
            {
                player1.ReceiveCards(cards);
            }
            foreach (Card cards in hand2)
            {
                player2.ReceiveCards(cards);
            }

            foreach (Player player in players)
            {
                player.Hands.Eval();
            }

            game.BestDuplicate(players);
            for (int i = 0; i < 1; i++)
            {
                Assert.GreaterOrEqual(player1.Hands.DuplicateRank[i], player2.Hands.DuplicateRank[i]);
            }
            game.CompareHands(Players);
            Assert.Greater(player1.Wins, player2.Wins);
        }

        [Test]
        public void IfMoreThanOneStraight()
        {
            Player player1 = new Player("Test1", 0);
            Player player2 = new Player("Test2", 0);
            IPlayer[] Players = { player1, player2 };
            List<Player> players = new List<Player> { player1, player2 };
            Card[] hand1 = TestsHand.ToCards("♣9♥10♦J♠Q♥K");
            Card[] hand2 = TestsHand.ToCards("♥3♣4♦5♣6♠7");
            foreach (Card cards in hand1)
            {
                player1.ReceiveCards(cards);
            }
            foreach (Card cards in hand2)
            {
                player2.ReceiveCards(cards);
            }
            foreach (Player player in players)
            {
                player.Hands.Eval();
            }
            game.CompareHands(Players);
            if (player1.Hands.HandType.Equals(player2.Hands.HandType))
            {
                for (int i = 0; i < 5; i++)
                {
                    Assert.Greater(player1.Hands.CardRank[i], player2.Hands.CardRank[i]);
                }
                Assert.Greater(player1.Wins, player2.Wins);
            }
        }

        [Test]
        public void IfTiedStraight()
        {
            Player player1 = new Player("Test1", 0);
            Player player2 = new Player("Test2", 0);
            IPlayer[] Players = { player1, player2 };
            List<Player> players = new List<Player> { player1, player2 };
            Card[] hand1 = TestsHand.ToCards("♣9♥10♦J♠Q♥K");
            Card[] hand2 = TestsHand.ToCards("♥9♣10♠J♣Q♠K");
            foreach (Card cards in hand1)
            {
                player1.ReceiveCards(cards);
            }
            foreach (Card cards in hand2)
            {
                player2.ReceiveCards(cards);
            }
            foreach (Player player in players)
            {
                player.Hands.Eval();
            }
            game.CompareHands(Players);
            if (player1.Hands.HandType.Equals(player2.Hands.HandType))
            {
                for (int i = 0; i < 5; i++)
                {
                    Assert.AreEqual(player1.Hands.CardRank[i], player2.Hands.CardRank[i]);
                }
                Assert.AreEqual(player1.Wins, player2.Wins);
            }
        }

        [Test]
        public void IfMoreThanOneFlush()
        {
            Player player1 = new Player("Test1", 0);
            Player player2 = new Player("Test2", 0);
            IPlayer[] Players = { player1, player2 };
            List<Player> players = new List<Player> { player1, player2 };
            Card[] hand1 = TestsHand.ToCards("♥4♥10♥J♥Q♥K");
            Card[] hand2 = TestsHand.ToCards("♠2♠3♠J♠Q♠K");
            foreach (Card cards in hand1)
            {
                player1.ReceiveCards(cards);
            }
            foreach (Card cards in hand2)
            {
                player2.ReceiveCards(cards);
            }
            foreach (Player player in players)
            {
                player.Hands.Eval();
            }
            game.CompareHands(Players);
            if (player1.Hands.HandType.Equals(player2.Hands.HandType))
            {
                for (int i = 0; i < 5; i++)
                {
                    Assert.GreaterOrEqual(player1.Hands.CardRank[i], player2.Hands.CardRank[i]);
                }
                Assert.Greater(player1.Wins, player2.Wins);
            }
        }

        [Test]
        public void IfTiedFlush()
        {
            Player player1 = new Player("Test1", 0);
            Player player2 = new Player("Test2", 0);
            IPlayer[] Players = { player1, player2 };
            List<Player> players = new List<Player> { player1, player2 };
            Card[] hand1 = TestsHand.ToCards("♥4♥10♥J♥Q♥K");
            Card[] hand2 = TestsHand.ToCards("♠4♠10♠J♠Q♠K");
            foreach (Card cards in hand1)
            {
                player1.ReceiveCards(cards);
            }
            foreach (Card cards in hand2)
            {
                player2.ReceiveCards(cards);
            }
            foreach (Player player in players)
            {
                player.Hands.Eval();
            }
            game.CompareHands(Players);
            if (player1.Hands.HandType.Equals(player2.Hands.HandType))
            {
                for (int i = 0; i < 5; i++)
                {
                    Assert.AreEqual(player1.Hands.CardRank[i], player2.Hands.CardRank[i]);
                }
                Assert.AreEqual(player1.Wins, player2.Wins);
            }
        }

        [Test]
        public void IfMoreThanOneFullHouse()
        {
            Player player1 = new Player("Test1", 0);
            Player player2 = new Player("Test2", 0);
            IPlayer[] Players = { player1, player2 };
            List<Player> players = new List<Player> { player1, player2 };
            Card[] hand1 = TestsHand.ToCards("♣10♥10♦A♠A♥A");
            Card[] hand2 = TestsHand.ToCards("♥J♣J♦K♣K♠K");
            foreach (Card cards in hand1)
            {
                player1.ReceiveCards(cards);
            }
            foreach (Card cards in hand2)
            {
                player2.ReceiveCards(cards);
            }
            foreach (Player player in players)
            {
                player.Hands.Eval();
            }
            if (player1.Hands.HandType.Equals(player2.Hands.HandType))
            {
                game.CompareHands(Players);
                for (int i = 0; i < 1; i++)
            {
                Assert.Greater(player1.Hands.ThreeDuplicateRank[i], player2.Hands.ThreeDuplicateRank[i]);
            }
                Assert.Greater(player1.Wins, player2.Wins);
            }
        }

        [Test]
        public void IfMoreThanOneFourOfAKind()
        {
            Player player1 = new Player("Test1", 0);
            Player player2 = new Player("Test2", 0);
            IPlayer[] Players = { player1, player2 };
            List<Player> players = new List<Player> { player1, player2 };
            Card[] hand1 = TestsHand.ToCards("♣10♣A♦A♠A♥A");
            Card[] hand2 = TestsHand.ToCards("♥J♥K♦K♣K♠K");
            foreach (Card cards in hand1)
            {
                player1.ReceiveCards(cards);
            }
            foreach (Card cards in hand2)
            {
                player2.ReceiveCards(cards);
            }
            foreach (Player player in players)
            {
                player.Hands.Eval();
            }
            if (player1.Hands.HandType.Equals(player2.Hands.HandType))
            {
                game.CompareHands(Players);
                for (int i = 0; i < 1; i++)
                {
                   Assert.Greater(player1.Hands.FourDuplicateRank[i], player2.Hands.FourDuplicateRank[i]);
                }
                Assert.Greater(player1.Wins, player2.Wins);
            }
        }

        [Test]
        public void IfMoreThanOneStraightFlush()
        {
            Player player1 = new Player("Test1", 0);
            Player player2 = new Player("Test2", 0);
            IPlayer[] Players = { player1, player2 };
            List<Player> players = new List<Player> { player1, player2 };
            Card[] hand1 = TestsHand.ToCards("♥7♥8♥9♥10♥J");
            Card[] hand2 = TestsHand.ToCards("♠9♠10♠J♠Q♠K");
            foreach (Card cards in hand1)
            {
                player1.ReceiveCards(cards);
            }
            foreach (Card cards in hand2)
            {
                player2.ReceiveCards(cards);
            }
            foreach (Player player in players)
            {
                player.Hands.Eval();
            }
            game.CompareHands(Players);
            if (player1.Hands.HandType.Equals(player2.Hands.HandType))
            {
                for (int i = 0; i < 5; i++)
                {
                    Assert.Greater(player2.Hands.CardRank[i], player1.Hands.CardRank[i]);
                }
                Assert.Greater(player2.Wins, player1.Wins);
            }
        }

        [Test]
        public void IfTiedStraightFlush()
        {
            Player player1 = new Player("Test1", 0);
            Player player2 = new Player("Test2", 0);
            IPlayer[] Players = { player1, player2 };
            List<Player> players = new List<Player> { player1, player2 };
            Card[] hand1 = TestsHand.ToCards("♥9♥10♥J♥Q♥K");
            Card[] hand2 = TestsHand.ToCards("♠9♠10♠J♠Q♠K");
            foreach (Card cards in hand1)
            {
                player1.ReceiveCards(cards);
            }
            foreach (Card cards in hand2)
            {
                player2.ReceiveCards(cards);
            }
            foreach (Player player in players)
            {
                player.Hands.Eval();
            }
            game.CompareHands(Players);
            if (player1.Hands.HandType.Equals(player2.Hands.HandType))
            {
                for (int i = 0; i < 5; i++)
                {
                    Assert.AreEqual(player2.Hands.CardRank[i], player1.Hands.CardRank[i]);
                }
                Assert.AreEqual(player2.Wins, player1.Wins);
            }
        }

        [Test]
        public void IfTiedRoyalStraightFlush()
        {
            Player player1 = new Player("Test1", 0);
            Player player2 = new Player("Test2", 0);
            IPlayer[] Players = { player1, player2 };
            List<Player> players = new List<Player> { player1, player2 };
            Card[] hand1 = TestsHand.ToCards("♥10♥J♥Q♥K♥A");
            Card[] hand2 = TestsHand.ToCards("♠10♠J♠Q♠K♠A");
            foreach (Card cards in hand1)
            {
                player1.ReceiveCards(cards);
            }
            foreach (Card cards in hand2)
            {
                player2.ReceiveCards(cards);
            }
            foreach (Player player in players)
            {
                player.Hands.Eval();
            }
            game.CompareHands(Players);
            if (player1.Hands.HandType.Equals(player2.Hands.HandType))
            {
                for (int i = 0; i < 5; i++)
                {
                    Assert.AreEqual(player2.Hands.CardRank[i], player1.Hands.CardRank[i]);
                }
                Assert.AreEqual(player2.Wins, player1.Wins);
            }
        }

        [Test]
        public void GameKnowsRanksOfHandTypesAndWinner()
        {
            Game Game = new Game(new string[5] { "Test1", "Test2", "Test3", "Test4", "Test5" });
            Player player1 = new Player("Test1", 0);
            Player player2 = new Player("Test2", 0);
            Player player3 = new Player("Test3", 0);
            Player player4 = new Player("Test4", 0);
            Player player5 = new Player("Test5", 0);
            IPlayer[] Players = { player1, player2, player3, player4, player5 };
            List<Player> players = new List<Player> { player1, player2, player3, player4, player5 };
            Card[] hand1 = TestsHand.ToCards("♥10♥J♥Q♥K♥A");
            Card[] hand2 = TestsHand.ToCards("♠9♠10♠J♠Q♠K");
            Card[] hand3 = TestsHand.ToCards("♠2♣5♦5♦5♠5");
            Card[] hand4 = TestsHand.ToCards("♠3♦3♦6♠6♥6");
            Card[] hand5 = TestsHand.ToCards("♥2♠4♠7♠8♠A");
            foreach (Card cards in hand1)
            {
                player1.ReceiveCards(cards);
            }
            foreach (Card cards in hand2)
            {
                player2.ReceiveCards(cards);
            }
            foreach (Card cards in hand3)
            {
                player3.ReceiveCards(cards);
            }
            foreach (Card cards in hand4)
            {
                player4.ReceiveCards(cards);
            }
            foreach (Card cards in hand5)
            {
                player5.ReceiveCards(cards);
            }
            foreach (Player player in players)
            {
                player.Hands.Eval();
            }

            Game.CompareHands(Players);
            Assert.Greater(player1.Hands.HandType, player2.Hands.HandType);
            Assert.Greater(player2.Hands.HandType, player3.Hands.HandType);
            Assert.Greater(player3.Hands.HandType, player4.Hands.HandType);
            Assert.Greater(player4.Hands.HandType, player5.Hands.HandType);
            
            Assert.Greater(player1.Wins, player2.Wins);
            Assert.Greater(player1.Wins, player3.Wins);
            Assert.Greater(player1.Wins, player4.Wins);
            Assert.Greater(player1.Wins, player5.Wins);

            Assert.AreEqual(player2.Wins, player3.Wins);
            Assert.AreEqual(player3.Wins, player4.Wins);
            Assert.AreEqual(player4.Wins, player5.Wins);
        }
    }
}