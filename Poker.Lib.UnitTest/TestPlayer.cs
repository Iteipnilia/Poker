using NUnit.Framework;
using System.Collections.Generic;
using static Poker.Suite;
using static Poker.Rank;

namespace Poker.Lib.UnitTest
{
    [TestFixture]
    public class TestsPlayer
    {
        Player player;
        
        [SetUp]
        public void PlayerSetup()
        {
            player= new Player("name");
        }

        [Test]
        public void PlayerHasAName()
        {
            Assert.That(player.Name, Is.EqualTo("name"));
        }

        [Test]
        public void PlayerHasWins()
        {
            Player Player = new Player ("name", 7);
            Player Player2 = new Player ("name2", 2);

            Player.Win();

            Assert.That(8, Is.EqualTo(Player.Wins));
            Assert.That(2, Is.EqualTo(Player2.Wins));
        }

        [Test]
        public void PlayerHasANameAndWins()
        {
            Player Player1 = new Player ("name1", 7);
            Player Player2 = new Player ("name2", 2);
            Player Player3 = new Player ("name3", 0);

            Assert.That(Player1.Name, Is.EqualTo("name1"));
            Assert.That(Player1.Wins, Is.EqualTo(7));
            Assert.That(Player2.Name, Is.EqualTo("name2"));
            Assert.That(Player2.Wins, Is.EqualTo(2));
            Assert.That(Player3.Name, Is.EqualTo("name3"));
            Assert.That(Player3.Wins, Is.EqualTo(0));
        }

        [Test]
        public void PlayerCanReceiveCards()
        {
            Card testCard= new Card(Suite.Clubs,Rank.Five);

            player.ReceiveCards(testCard);

            Assert.That(player.Hand, Contains.Item(testCard));

        }

        [Test]
        public void PlayerWinsIncreaseWhenCalled()
        {
            player.Win();
            Assert.That(player.Wins, Is.EqualTo(1));

            player.Win();
            Assert.That(player.Wins, Is.EqualTo(2));
        }

        [Test]
        public void SortPlayerHandIsCalled()
        {
            // Non-sorted list
            List<Card> tempHand = new List<Card>{ 
                (Clubs, Six), (Diamonds, Four), (Hearts, Five), 
                (Spades, Three), (Clubs, Two) };

            foreach(Card card in tempHand)
            {
                player.ReceiveCards(card);
            }

            CollectionAssert.AreEqual(tempHand, player.Hand);

                player.SortPlayerHand();

            CollectionAssert.AreNotEqual(tempHand, player.Hand);
             
        }

        [Test]
        public void PlayerGetsAHandType()
        {
            List<Card> tempHand = new List<Card>{ 
                (Clubs, Two), (Diamonds, Three), (Hearts, Four), 
                (Spades, Five), (Clubs, Six) };

            foreach(Card card in tempHand)
            {
                player.ReceiveCards(card);
            }

            player.Hands.Eval();
            Assert.NotNull(player.Hands.HandType);
            Assert.AreEqual(HandType.Straight, player.Hands.HandType);
            Assert.AreEqual(player.HandType, player.Hands.HandType);
        }
    }
}