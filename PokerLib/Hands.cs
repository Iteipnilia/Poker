using System.Collections.Generic;
using System.Linq;
using System;

namespace Poker
{
    class Hands : Deck, IComparable
    {
        public HandType HandType { get; set; }
        public List<Card> Hand{get;set;}
        public List<Rank> CardRank { get; private set; }
        public List<Rank> DuplicateRank { get; private set; }
        public List<Rank> ThreeDuplicateRank { get; private set; }
        bool Contains(Rank Rk) => cards.Where(c => c.Rank == Rk).Any();

        public Hands()
        {
            Hand = new List<Card>(5);
        }

        public int CompareTo(object other)
        {   
            Hands otherHand=(Hands)other;
            if (this.HandType < otherHand.HandType){return 1;}
            if (this.HandType > otherHand.HandType){return -1;}
            return 0;
        }

        public HandType Eval() //returns the hands value and type
        {
            if (IsRoyalStraightFlush)
                 HandType = HandType.RoyalStraightFlush;
            else if (IsStraightFlush)
                 HandType = HandType.StraightFlush;
            else if (IsFourOfAKind)
                 HandType = HandType.FourOfAKind;
            else if (IsFullHouse)
                 HandType = HandType.FullHouse;
            else if (IsFlush)
                 HandType = HandType.Flush;
            else if (IsStraight)
                 HandType = HandType.Straight;
            else if (IsThreeOfAKind)
                 HandType = HandType.ThreeOfAKind;
            else if (IsTwoPair)
                 HandType = HandType.TwoPairs;
            else if (IsPair)
                 HandType = HandType.Pair;
            else
            {
                 HandType = HandType.HighCard;
            }

            CardRank = cards.Select(card => card.Rank)
                    .OrderBy(r => r).ToList();

            if (HandType == HandType.Pair || HandType == HandType.TwoPairs || HandType == HandType.ThreeOfAKind || HandType == HandType.FourOfAKind)
            {
                DuplicateRank = cards.GroupBy(card => card.Rank)
                .Where(group => group.Count() == 2)
                .Select(group => group.Key)
                .OrderByDescending(x => x).ToList();
            }
            if (HandType == HandType.FullHouse)
            {
                ThreeDuplicateRank = cards.GroupBy(card => card.Rank)
                .Where(group => group.Count() == 3)
                .Select(group => group.Key)
                .OrderByDescending(x => x).ToList();
            }
            return HandType;
        }

        public bool IsPair
        {
            get
            {
                return cards.GroupBy(h => h.Rank)
                .Where(g => g.Count() == 2)
                .Count() == 1;
            }
        }
        public bool IsTwoPair
        {
            get
            {
                return cards.GroupBy(h => h.Rank)
                .Where(g => g.Count() == 2)
                .Count() == 2;
            }
        }

        public bool IsThreeOfAKind
        {
            get
            {
                return cards.GroupBy(h => h.Rank)
                .Where(g => g.Count() == 3)
                .Any();
            }
        }

        public bool IsFourOfAKind
        {
            get
            {
                return cards.GroupBy(h => h.Rank)
                .Where(g => g.Count() == 4)
                .Any();
            }
        }

        public bool IsFlush
        {
            get
            {
                return cards.GroupBy(h => h.Suite).Count() == 1;
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
                var ordered = cards.OrderBy(h => h.Rank).ToArray();//STOP
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
            Hand = Hand.OrderBy(card => card.Rank).ToList();//stop
            Hand = Hand.OrderBy(card => card.Suite).ToList();
        }
        public void AddCardToHand(Card card)
        {
            Hand.Add(card);
        }
        public void RemoveCard(Card card)
        {
            Hand.Remove(card);
        }
    }
}