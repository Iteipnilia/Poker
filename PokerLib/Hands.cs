using System.Collections.Generic;
using System.Linq;

namespace Poker
{
    class Hands
    {
        public HandType HandType { get; set; }
        public List<Card> Hand { get; set; }
        public List<Rank> CardRank { get; private set; }
        public List<Rank> DuplicateRank { get; private set; }
        public List<Rank> ThreeDuplicateRank { get; private set; }
        public List<Rank> FourDuplicateRank { get; private set; }
        bool Contains(Rank Rk) => Hand.Where(c => c.Rank == Rk).Any();

        public Hands()
        {
            Hand = new List<Card>(5);
        }

        public HandType Eval() //returns the hands value and type
        {
            HandType handType;
            if (IsRoyalStraightFlush)
                handType = HandType.RoyalStraightFlush;
            else if (IsStraightFlush)
                handType = HandType.StraightFlush;
            else if (IsFourOfAKind)
                handType = HandType.FourOfAKind;
            else if (IsFullHouse)
                handType = HandType.FullHouse;
            else if (IsFlush)
                handType = HandType.Flush;
            else if (IsStraight)
                handType = HandType.Straight;
            else if (IsThreeOfAKind)
                handType = HandType.ThreeOfAKind;
            else if (IsTwoPair)
                handType = HandType.TwoPairs;
            else if (IsPair)
                handType = HandType.Pair;
            else
            {
                handType = HandType.HighCard;
            }

            CardRank = Hand.Select(card => card.Rank)
                    .OrderBy(r => r).ToList();

            if (handType == HandType.Pair || handType == HandType.TwoPairs)
            {
                DuplicateRank = Hand.GroupBy(card => card.Rank)
                .Where(group => group.Count() == 2)
                .Select(group => group.Key)
                .OrderByDescending(x => x).ToList();
            }
            if (handType == HandType.ThreeOfAKind || handType == HandType.FullHouse)
            {
                ThreeDuplicateRank = Hand.GroupBy(card => card.Rank)
                .Where(group => group.Count() == 3)
                .Select(group => group.Key)
                .OrderByDescending(x => x).ToList();
            }
            if (handType == HandType.FourOfAKind)
            {
                FourDuplicateRank = Hand.GroupBy(card => card.Rank)
                .Where(group => group.Count() == 3)
                .Select(group => group.Key)
                .OrderByDescending(x => x).ToList();
            }
            HandType = handType;
            return handType;
        }

        public bool IsPair
        {
            get
            {
                return Hand.GroupBy(h => h.Rank)
                .Where(g => g.Count() == 2)
                .Count() == 1;
            }
        }
        public bool IsTwoPair
        {
            get
            {
                return Hand.GroupBy(h => h.Rank)
                .Where(g => g.Count() == 2)
                .Count() == 2;
            }
        }

        public bool IsThreeOfAKind
        {
            get
            {
                return Hand.GroupBy(h => h.Rank)
                .Where(g => g.Count() == 3)
                .Any();
            }
        }

        public bool IsFourOfAKind
        {
            get
            {
                return Hand.GroupBy(h => h.Rank)
                .Where(g => g.Count() == 4)
                .Any();
            }
        }

        public bool IsFlush
        {
            get
            {
                return Hand.GroupBy(h => h.Suite).Count() == 1;
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
                var ordered = Hand.OrderBy(h => h.Rank).ToList();//STOP
                var straightStart = (int)ordered.First().Rank;
                for (var i = 1; i < ordered.Count; i++)
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
            Hand = Hand.OrderBy(card => card.Rank).ThenBy(cards => cards.Suite).ToList();
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