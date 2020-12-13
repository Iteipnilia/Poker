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
    public class TestsHand
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void IsHandSortedCorrect()
        {
            int i=0;
            int j=0;

            List<Player> player= new List<Player>();
            player.Add(new Player("Zero"));
            player.Add(new Player("One"));
            player.Add(new Player("Two"));

            List<Hands> hands = new List<Hands>();
            hands.Add(new Hands());
            hands.Add(new Hands());
            hands.Add(new Hands());       
            
            // Temporary hands for sorting
            var cardSets = new string[] {
        "♦1♥1♠1♥1♣1", "♦K♥1♠10♥2♣1", "♦A♦8♦2♦K♦5"
    };
            foreach (var cardSet in cardSets.Select(cs => ToCards(cs)))
            {
                foreach (var Card in cardSet)
                {
                    player[i].Hands.AddCardToHand(Card);
                }
                player[i].SortPlayerHand();
                i++;
            }

            // Temporary Hands with expected result
            var handSets = new string[] {
        "♣1♦1♥1♥1♠1", "♣1♥1♥2♠10♦K", "♦2♦5♦8♦K♦A"
    };
            foreach (var handSet in handSets.Select(cs => ToCards(cs)))
            {
                foreach (var Card in handSet)
                {
                    hands[j].AddCardToHand(Card);
                }
                j++;
            }

            CollectionAssert.AreEqual(hands[0].Hand,player[0].Hand);
            CollectionAssert.AreEqual(hands[1].Hand,player[1].Hand);
            CollectionAssert.AreEqual(hands[2].Hand,player[2].Hand);

        }

        [Test]
        public void HandCanBeRoyalStraightFlush()
        {
            var cardSets = new string[] {
        "♥10♥J♥Q♥K♥A", "♠10♠J♠Q♠K♠A", "♦10♦J♦Q♦K♦A", "♣10♣J♣Q♣K♣A"
    };
            foreach (var cardSet in cardSets.Select(cs => ToCards(cs)))
            {
                Player player = new Player("");
                foreach (var Card in cardSet)
                    player.Hands.AddCardToHand(Card);
                player.Hands.Eval();
                Assert.AreEqual(RoyalStraightFlush, player.Hands.HandType);
            }
        }

        [Test]
        public void HandCanBeStraightFlush()
        {
            var cardSets = new string[] {
        "♥A♥2♥3♥4♥5", "♠9♠10♠J♠Q♠K", "♦8♦9♦10♦J♦Q", "♣6♣7♣8♣9♣10"
    };
            foreach (var cardSet in cardSets.Select(cs => ToCards(cs)))
            {
                Player player = new Player("");
                foreach (var card in cardSet)
                    player.Hands.AddCardToHand(card);
                player.Hands.Eval();
                Assert.AreEqual(StraightFlush, player.Hands.HandType);
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
                Player player = new Player("");
                foreach (var card in cardSet)
                    player.Hands.AddCardToHand(card);
                player.Hands.Eval();
                Assert.AreEqual(FourOfAKind, player.Hands.HandType);
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
                Player player = new Player("");
                foreach (var card in cardSet)
                    player.Hands.AddCardToHand(card);
                player.Hands.Eval();
                Assert.AreEqual(FullHouse, player.Hands.HandType);
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
                Player player = new Player("");
                foreach (var card in cardSet)
                    player.Hands.AddCardToHand(card);
                player.Hands.Eval();
                Assert.AreEqual(Flush, player.Hands.HandType);
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
                Player player = new Player("");
                foreach (var card in cardSet)
                    player.Hands.AddCardToHand(card);
                player.Hands.Eval();
                Assert.AreEqual(Straight, player.Hands.HandType);
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
                Player player = new Player("");
                foreach (var card in cardSet)
                    player.Hands.AddCardToHand(card);
                player.Hands.Eval();
                Assert.AreEqual(ThreeOfAKind, player.Hands.HandType);
            }
        }

        [Test]
        public void HandCanBeTwoPair()
        {
            var cardSets = new string[] {
        "♣2♦2♦3♥3♥5", "♠3♥7♠7♠8♠8", "♣10♦J♥J♣Q♦Q", "♣Q♦Q♦K♣A♠A"
    };
            foreach (var cardSet in cardSets.Select(cs => ToCards(cs)))
            {
                Player player = new Player("");
                foreach (var card in cardSet)
                    player.Hands.AddCardToHand(card);
                player.Hands.Eval();
                Assert.AreEqual(TwoPairs, player.Hands.HandType);
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
                Player player = new Player("");
                foreach (var card in cardSet)
                    player.Hands.AddCardToHand(card);
                player.Hands.Eval();
                Assert.AreEqual(Pair, player.Hands.HandType);
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
                Player player = new Player("");
                foreach (var card in cardSet)
                    player.Hands.AddCardToHand(card);
                player.Hands.Eval();
                Assert.AreEqual(HighCard, player.Hands.HandType);
            }
        }


        internal static Card[] ToCards(string text)
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