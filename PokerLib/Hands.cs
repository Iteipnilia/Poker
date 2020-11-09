using System.Collections.Generic;
using System.Linq;

namespace Poker
{
    class Hands
    {
        public IEnumerable<Card> Cards { get; set; }
        public HandType HandType { get; set; }
        public Card[] Hand { get; set; }
        bool Contains(Rank Rk) => Cards.Where(c => c.Rank == Rk).Any();

        public Hands()
        {
            Hand = new Card[5];
        }

        public HandType Eval() //returns the hands value and type
        {
            if (IsRoyalStraightFlush)
                return HandType = HandType.RoyalStraightFlush;
            else if (IsStraightFlush)
                return HandType = HandType.StraightFlush;
            else if (IsFourOfAKind)
                return HandType = HandType.FourOfAKind;
            else if (IsFullHouse)
                return HandType = HandType.FullHouse;
            else if (IsFlush)
                return HandType = HandType.Flush;
            else if (IsStraight)
                return HandType = HandType.Straight;
            else if (IsThreeOfAKind)
                return HandType = HandType.ThreeOfAKind;
            else if (IsTwoPair)
                return HandType = HandType.TwoPairs;
            else if (IsPair)
                return HandType = HandType.Pair;
            else
            {
                return HandType = HandType.HighCard;
            }
        }

        public bool IsPair
        {
            get
            {
                return Cards.GroupBy(h => h.Rank)
                .Where(g => g.Count() == 2)
                .Count() == 1;
            }
        }
        public bool IsTwoPair
        {
            get
            {
                return Cards.GroupBy(h => h.Rank)
                .Where(g => g.Count() == 2)
                .Count() == 2;
            }
        }

        public bool IsThreeOfAKind
        {
            get
            {
                return Cards.GroupBy(h => h.Rank)
                .Where(g => g.Count() == 3)
                .Any();
            }
        }

        public bool IsFourOfAKind
        {
            get
            {
                return Cards.GroupBy(h => h.Rank)
                .Where(g => g.Count() == 4)
                .Any();
            }
        }

        public bool IsFlush
        {
            get
            {
                return Cards.GroupBy(h => h.Suite).Count() == 1;
            }
        }

        public bool IsFullHouse
        {
            get
            {
                return IsPair && IsThreeOfAKind;
            }
        }

        public bool IsStraight
        {
            get
            {
                var ordered = Cards.OrderBy(h => h.Rank).ToArray();
                var straightStart = (int)ordered.First().Rank;
                for (var i = 1; i < ordered.Length; i++)
                {
                    if ((int)ordered[i].Rank != straightStart + i)
                        return false;
                }
                return true;
            }

        }

        public bool IsStraightFlush
        {
            get
            {
                return IsStraight && IsFlush;
            }
        }

        public bool IsRoyalStraightFlush
        {
            get
            {
                return IsStraight && IsFlush && Contains(Rank.Ace) && Contains(Rank.King);
            }
        }

        public void SortHand() //sorts the hand first by rank and then by suit
        {
            Hand = Hand.OrderBy(card => card.Rank).ToArray();
            Hand = Hand.OrderBy(card => card.Suite).ToArray();
        }

        public Card RemoveCardFromHand(int index)
        {
            Hand[index] = null;
            return Hand[index];
        }
        //Lägg till kort på hand??
        public void AddCardToHand(Card card)
        {
            for (int i = 0; i < 5; i++)
            {
                if (Hand[i] == null)
                {
                    Hand[i] = card;
                    break;
                }
            }
        }
    }
}