using NUnit.Framework;

namespace Poker.Lib.UnitTest
{
    public class TestsDeck
    {
        Deck testDeck= new Deck();

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void IsDeckCorrectLength()
        {
            Assert.That(testDeck, Has.Exactly(52).Items);
        }
        [Test]
        public void IsDeckWithoutDuplicates()
        {
            Assert.That(testDeck, Is.Unique);
        }


        [Test]
        public void DoesShuffleWork()
        {
            Deck tempDeck=new Deck();

            tempDeck.Shuffle();
            tempDeck.Shuffle();
            tempDeck.Shuffle();

            Assert.That(tempDeck, Is.Not.EqualTo(testDeck));
        }


    }
}