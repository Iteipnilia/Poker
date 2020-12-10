using NUnit.Framework;

namespace Poker.Lib.UnitTest
{
    public class TestsDeck
    {
        Deck testDeck;
        Deck tempDeck;

        [SetUp]
        public void DeckSetup()
        {
            testDeck= new Deck();
            tempDeck= new Deck();
        }

        [Test]
        public void IsDeckCorrectLength()
        {
            Assert.That(testDeck, Has.Exactly(52).Items);
        }

        [Test]
        public void IsDeckWithoutDuplicates()
        {
            CollectionAssert.AllItemsAreUnique(testDeck);
        }


        [Test]
        public void DoesShuffleWork()
        {
            CollectionAssert.AreEqual(tempDeck, testDeck);

            tempDeck.Shuffle();
            tempDeck.Shuffle();
            tempDeck.Shuffle();

            CollectionAssert.AreNotEqual(tempDeck, testDeck);
            CollectionAssert.AreEquivalent(tempDeck, testDeck);
            CollectionAssert.AllItemsAreUnique(tempDeck);
            Assert.That(tempDeck, Has.Exactly(52).Items);
        }

        [Test]
        public void IsCardRemovedFromDeck()
        {
            Card tempCard= testDeck.GetTopCard();

            CollectionAssert.DoesNotContain(testDeck, tempCard);
        }

        


    }
}