using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Poker.Lib.UnitTest
{
    [TestFixture]
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
        public void DoesShuffleWorkAndWithoutFault()
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
        public void TakesCardAtFirstIndexOfList()
        {
            CollectionAssert.AreEqual(testDeck, tempDeck);
            Card tempCardOne= testDeck.GetTopCard();
            Card tempCardTwo= tempDeck.GetTopCard();
            Assert.AreEqual(tempCardOne,tempCardTwo );
        }

        [Test]
        public void IsCardRemovedFromDeck()
        {
            Card tempCard= testDeck.GetTopCard();

            CollectionAssert.DoesNotContain(testDeck, tempCard);
            Assert.That(testDeck, Has.Exactly(51).Items);
        }

        [Test]
        public void CanPutBackCard()
        {
            tempDeck.Cards.Clear();

            foreach(Card card in testDeck)
            {
                tempDeck.PutBackCard(card);
            }

            CollectionAssert.AreEquivalent(tempDeck, testDeck);
            Assert.That(tempDeck, Has.Exactly(52).Items);          

        }
        [Test]
        public void PutBackCardDoesThrowException()
        {
            Card tempCard= new Card(Suite.Hearts, Rank.Five);

            Assert.Throws<ArgumentException>(
            () => testDeck.PutBackCard(tempCard));

            Assert.That(testDeck, Has.Exactly(52).Items);

        }

        [Test]
        public void GetTopCardDoesThrowException()
        {
            testDeck.Cards.Clear();
            Assert.IsEmpty(testDeck);

            Assert.Throws<NullReferenceException>(
            () => testDeck.GetTopCard());
        }

        


    }
}