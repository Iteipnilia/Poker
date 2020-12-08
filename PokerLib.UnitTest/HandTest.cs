using NUnit.Framework;
using Poker;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static Poker.Suite;
using static Poker.Rank;

namespace PokerLib.UnitTest
{
    public class HandTests
    {

        [SetUp]
        public void Setup()
        {
        }
        
        [Test]
        public void HandCanBeRoyalStraightFlush()
        {
            var cardSets = new string[] {
        "♥10♥J♥Q♥K♥A", "♠10♠J♠Q♠K♠A", "♦10♦J♦Q♦K♦A", "♣10♣J♣Q♣K♣A"
    };
            foreach (var cardSet in cardSets.Select(cs => ToCards(cs)))
            {
                Hands Hand = new Hands();
                foreach (var card in cardSet)
                    Hand.AddCardToHand(card);
                Assert.AreEqual(HandType.RoyalStraightFlush, Hand.HandType);
            }
        }
        
        [Test]
        public void HandCanBeStraightFlush()
        {
            var cardSets = new string[] {
        "♥A♥2♥3♥4♥5", "♠10♠J♠Q♠K♠A", "♦8♦9♦10♦J♦Q", "♣6♣7♣8♣9♣10"
    };
            foreach (var cardSet in cardSets.Select(cs => ToCards(cs)))
            {
                Hands Hand = new Hands();
                foreach (var card in cardSet)
                    Hand.AddCardToHand(card);
                Assert.AreEqual(HandType.StraightFlush, Hand.HandType);
            }
        }
        
        [Test]
        public void HandCanBeFourOfAKind()
        {
            var cardSets = new string[] {
        "♣2♦2♥2♠2♥5", "♦J♥A♦A♣A♠A"
    };
            foreach (var cardSet in cardSets.Select(cs => ToCards(cs)))
            {
                Hands Hand = new Hands();
                foreach (var card in cardSet)
                    Hand.AddCardToHand(card);
                Assert.AreEqual(HandType.FourOfAKind, Hand.HandType);
            }
        }

        [Test]
        public void HandCanBeFullHouse()
        {
            var cardSets = new string[] {
        "♣2♦2♥2♣5♥5", "♣Q♦Q♦A♣A♠A"
    };
            foreach (var cardSet in cardSets.Select(cs => ToCards(cs)))
            {
                Hands Hand = new Hands();
                foreach (var card in cardSet)
                    Hand.AddCardToHand(card);
                Assert.AreEqual(HandType.FullHouse, Hand.HandType);
            }
        }

        [Test]
        public void HandCanBeFlush()
        {
            var cardSets = new string[] {
        "♥3♥4♥5♥7♥9", "♠7♠8♠9♠Q♠A", "♦6♦J♦Q♦K♦A", "♣2♣4♣5♣9♣10"
    };
            foreach (var cardSet in cardSets.Select(cs => ToCards(cs)))
            {
                Hands Hand = new Hands();
                foreach (var card in cardSet)
                    Hand.AddCardToHand(card);
                Assert.AreEqual(HandType.Flush, Hand.HandType);
            }
        }

        [Test]
        public void HandCanBeStraight()
        {
            var cardSets = new string[] {
        "♠A♦2♥3♥4♥5", "♥6♠7♠8♠9♣10", "♣10♦J♦Q♦K♠A"
    };
            foreach (var cardSet in cardSets.Select(cs => ToCards(cs)))
            {
                Hands Hand = new Hands();
                foreach (var card in cardSet)
                    Hand.AddCardToHand(card);
                Assert.AreEqual(HandType.Straight, Hand.HandType);
            }
        }
        
        [Test]
        public void HandCanBeThreeOfAKind()
        {
            var cardSets = new string[] {
        "♣2♦2♥2♥4♥5", "♣9♣J♦J♥J♣Q", "♦J♦Q♦A♣A♠A"
    };
            foreach (var cardSet in cardSets.Select(cs => ToCards(cs)))
            {
                Hands Hand = new Hands();
                foreach (var card in cardSet)
                    Hand.AddCardToHand(card);
                Assert.AreEqual(HandType.ThreeOfAKind, Hand.HandType);
            }
        }
        
        [Test]
        public void HandCanBeTwoPair()
        {
            var cardSets = new string[] {
        "♣2♦2♦3♥3♥5", "♠3♥7♠7♠8♠9", "♣10♦J♥J♣Q♦Q", "♣Q♦Q♦K♣A♠A"
    };
            foreach (var cardSet in cardSets.Select(cs => ToCards(cs)))
            {
                Hands Hand = new Hands();
                foreach (var card in cardSet)
                    Hand.AddCardToHand(card);
                Assert.AreEqual(HandType.TwoPairs, Hand.HandType);
            }
        }

        [Test]
        public void HandCanBePair()
        {
            var cardSets = new string[] {
        "♣2♦2♥3♥4♥5", "♠3♥7♠7♠8♠9", "♣9♣10♦J♥J♣Q", "♦J♦Q♦K♣A♠A"
    };
            foreach (var cardSet in cardSets.Select(cs => ToCards(cs)))
            {
                Player player = new Player("TestSpelare");
                Hands Hand = new Hands();
                foreach (var card in cardSet)
                    Hand.AddCardToHand(card);
                player.Hands.Eval();
                Assert.AreEqual(HandType.Pair, Hand.HandType);
            }
        }

        [Test]
        public void HandCanBeHighCard()
        {
            var cardSets = new string[] {
        "♣2♥3♥4♥5♦9", "♠3♥4♠7♠8♠9", "♥4♣9♣10♦J♣Q", "♣6♦J♦Q♦K♠A"
    };
            foreach (var cardSet in cardSets.Select(cs => ToCards(cs)))
            {
                Hands Hand = new Hands();
                foreach (var card in cardSet)
                    Hand.AddCardToHand(card);
                Assert.AreEqual(HandType.HighCard, Hand.HandType);
            }
        }


        static Card[] ToCards(string text)
        {
            List<Card> cards = new List<Card>();
            int i = 0;
            while (i < text.Length)
            {
                Suite suite = (text[i]) switch
                {
                    '♣' => Clubs,
                    '♦' => Diamonds,
                    '♥' => Hearts,
                    '♠' => Spades,
                    _ => throw new NotImplementedException(),
                };
                var rankString = text.Substring(i + 1);
                var rankFunc = new Dictionary<string, Func<string, Rank>>() {
            {@"^J",  _ => Jack}, {@"^Q", _ => Queen}, {@"^K", _ => King},
            {@"^A", _ => Ace}, { @"^\d+", str => (Rank)int.Parse(str) }
        };
                var func = rankFunc.Where(func => Regex.IsMatch(rankString, func.Key)).First();
                cards.Add((suite, func.Value(Regex.Match(rankString, func.Key).Value)));
                i += Regex.IsMatch(rankString, @"^\d\d") ? 3 : 2;
            }
            return cards.ToArray();
        }
    }
}