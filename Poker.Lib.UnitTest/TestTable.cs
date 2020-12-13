using NUnit.Framework;
using System;


namespace Poker.Lib.UnitTest
{
    [TestFixture]
    class TestsTable
    {
        Table testTable;
        
        [SetUp]
        public void TableSetUp()
        {
            testTable= new Table();
            testTable.AddPlayerToTable("Name1");
            testTable.AddPlayerToTable("Name2");
            testTable.AddPlayerToTable("Name3");

            testTable.DealTable();
        }
        [Test]
        public void CanAddPlayersToTable()
        {

            Assert.That(testTable.Players, Has.Exactly(3).Items);
            Assert.That(testTable.Players[0].Name, Is.EqualTo("Name1"));
            Assert.That(testTable.Players[1].Name, Is.EqualTo("Name2"));
            Assert.That(testTable.Players[2].Name, Is.EqualTo("Name3"));

            Assert.IsInstanceOf<Player>(testTable.Players[0]);            
        }

        [Test]
        public void CanAddPlayersWithWinsToTable()
        {
            Table TestTable= new Table();
            TestTable.AddPlayerToTable("Name1", 1);
            TestTable.AddPlayerToTable("Name2", 0);
            TestTable.AddPlayerToTable("Name3", 5);

            Assert.That(TestTable.Players, Has.Exactly(3).Items);
            Assert.That(TestTable.Players[0].Name, Is.EqualTo("Name1"));
            Assert.That(TestTable.Players[0].Wins, Is.EqualTo(1));
            Assert.That(TestTable.Players[1].Name, Is.EqualTo("Name2"));
            Assert.That(TestTable.Players[1].Wins, Is.EqualTo(0));
            Assert.That(TestTable.Players[2].Name, Is.EqualTo("Name3"));
            Assert.That(TestTable.Players[2].Wins, Is.EqualTo(5));

            Assert.IsInstanceOf<Player>(TestTable.Players[0]);            
        }

        [Test]
        public void CantAddMoreThanFivePlayers()
        {
            testTable.AddPlayerToTable("");
            testTable.AddPlayerToTable("");
            testTable.AddPlayerToTable("");
            testTable.AddPlayerToTable("");

            Assert.That(testTable.Players, Has.Exactly(5).Items);
        }

        [Test, Sequential]
        public void CanDealTable([Values(0, 1, 2)] int playerIndex)
        {            
            Assert.That(testTable.Players[playerIndex].Hand, Has.Exactly(5).Items);
        }

        [Test]
        public void CanDiscardCards()
        {
            Table tempTable= new Table();
            Card tempCard= new Card(Suite.Clubs,Rank.Five);
            tempTable.AddPlayerToTable("");
            tempTable.Players[0].Hands.Hand.Add(tempCard);
            
            Assert.That(tempTable.Players[0].Hand, Has.Exactly(1).Items);
            Assert.AreEqual(tempTable.Players[0].Hand[0], tempCard);

            tempTable.DiscardCard(tempTable.Players[0], tempCard);

            Assert.IsEmpty(tempTable.Players[0].Hand);

            Assert.That(tempTable.DiscardedCards, Has.Exactly(1).Items);
            Assert.AreEqual(tempTable.DiscardedCards[0], tempCard);
        }

        [Test]
        public void CanReplaceCards()
        {
            Table tempTable= new Table();
            tempTable.AddPlayerToTable("");
            
            tempTable.ReplacementCards(tempTable.Players[0],5);

            Assert.That(tempTable.Players[0].Hand, Has.Exactly(5).Items);
            CollectionAssert.AllItemsAreUnique(tempTable.Players[0].Hand);

        }

        [Test]
        public void CanRebuildDeck()
        {
            Deck tempDeck= new Deck();
            foreach(Player player in testTable.Players)
            {
                foreach(Card card in player.Hand)
                {
                    testTable.DiscardCard(player, card);
                }
            }
            testTable.DealTable();

            testTable.RebuildDeck();

            CollectionAssert.AreEquivalent(tempDeck, testTable.Deck);      
        } 

        [Test]
        public void CantRebuildDeckWithoutCards()
        {
            Table tempTable= new Table();
            tempTable.Deck.Cards.Clear();
            
            Assert.IsEmpty(tempTable.Deck.Cards);
            
            Assert.Throws<ArgumentException>(
            () => tempTable.RebuildDeck());
            
        }       
    }
}