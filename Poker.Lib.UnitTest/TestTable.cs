using NUnit.Framework;

namespace Poker.Lib.UnitTest
{
    class TestsTable
    {
        Table testTable= new Table();
        
        [SetUp]
        public void TableSetUp()
        {
            testTable.AddPlayerToTable("Name1");
            testTable.AddPlayerToTable("Name2");
            testTable.AddPlayerToTable("Name3");
        }
        [Test]
        public void CanAddPlayersToTable()
        {

            Assert.That(testTable.Players, Has.Exactly(3).Items);
            Assert.That(testTable.Players[0].Name, Is.EqualTo("Name1"));
            Assert.That(testTable.Players[1].Name, Is.EqualTo("Name2"));
            Assert.That(testTable.Players[2].Name, Is.EqualTo("Name3"));
            
        }

        [Test]
        public void CanDealTable()
        {
            testTable.DealTable();
            
            Assert.That(testTable.Players[0].Hand, Has.Exactly(5).Items);
            Assert.That(testTable.Players[1].Hand, Has.Exactly(5).Items);
            Assert.That(testTable.Players[2].Hand, Has.Exactly(5).Items);

        }

        
    }
}