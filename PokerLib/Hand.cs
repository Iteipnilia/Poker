using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Poker
{
    class Hand : Deck, IEnumerable<Card>
    {
        public IEnumerator GetEnumerator() => cards.GetEnumerator();

        IEnumerator<Card> IEnumerable<Card>.GetEnumerator()
        {
            return cards.GetEnumerator();
        }
        public HandType HandType { get; set; }
        public List<Rank> CardRank { get; set; }
        public List<Rank> DuplicateRank { get; set; }
        public List<Rank> ThreeDuplicateRank { get; set; }
        bool Contains(Rank Rk) => cards.Where(c => c.Rank == Rk).Any();

        public Hand()
        {

        }

        public HandType Eval(Hand hand) //returns the hands value and type
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
            HandType = handType;
            return handType;
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
            cards = cards.OrderBy(card => card.Rank).ToList();//stop
            cards = cards.OrderBy(card => card.Suite).ToList();
        }
        public void AddCardToHand(Card card)
        {
            cards.Add(card);
        }
        public void RemoveCard(Card card)
        {
            cards.Remove(card);
        }
    }
}