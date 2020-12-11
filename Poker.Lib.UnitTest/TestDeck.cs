using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

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
            Deck scoopDeck= new Deck();
            CollectionAssert.AreEqual(scoopDeck, tempDeck);
            Card tempCardOne= scoopDeck.GetTopCard();
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
            Deck scoopDeck= new Deck();

            scoopDeck.Cards.Clear();

            foreach(Card card in testDeck)
            {
                scoopDeck.PutBackCard(card);
            }

            CollectionAssert.AreEquivalent(scoopDeck, testDeck);          

        }

        


    }
}