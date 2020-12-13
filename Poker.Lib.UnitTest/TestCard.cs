using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static Poker.Suite;
using static Poker.Rank;
using static Poker.HandType;

namespace Poker.Lib.UnitTest
{
    public class CardTest
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void CardToStringTest()
        {
            Card card1 = new Card(Suite.Clubs, Rank.Ten);
            Card card2 = new Card(Suite.Diamonds, Rank.Jack);
            Card card3 = new Card(Suite.Spades, Rank.Queen);
            Card card4 = new Card(Suite.Clubs, Rank.King);
            Card card5 = new Card(Suite.Hearts, Rank.Ace);

            string expected1 = "♣10";
            string expected2 = "♦J";
            string expected3 = "♠Q";
            string expected4 = "♣K";
            string expected5 = "♥A";

            Assert.AreEqual(expected1, card1.ToString());
            Assert.AreEqual(expected2, card2.ToString());
            Assert.AreEqual(expected3, card3.ToString());
            Assert.AreEqual(expected4, card4.ToString());
            Assert.AreEqual(expected5, card5.ToString());
        }
    }
}