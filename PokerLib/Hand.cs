using System.Collections.Generic;
using System.Linq;

namespace Poker
{
    class Hands
    {
        public IEnumerable<Card> Cards { get; set; }
        private HandType handType { get; set; }
        private Card[] hand;// = new Card[5];
        bool Contains(Rank Rk) => hand.Where(c => c.Rank == Rk).Any();

        public Hands()
        {
            hand = new Card[5];
        }
        public Card[] Hand
        {
            get { return hand; }
            set { value = hand; }
        }

        public HandType HandType
        {
            get {return handType;}
            set {value = handType;}
        }
        public bool IsPair
        {
            get
            {
                return hand.GroupBy(h => h.Rank)
                .Where(g => g.Count() == 2)
                .Count() == 1;
            }
        }
        public bool IsTwoPair
        {
            get
            {
                return hand.GroupBy(h => h.Rank)
                .Where(g => g.Count() == 2)
                .Count() == 2;
            }
        }
        public bool IsThreeOfAKind
        {
            get
            {
                return hand.GroupBy(h => h.Rank)
                .Where(g => g.Count() == 3)
                .Any();
            }
        }
        public bool IsFourOfAKind
        {
            get
            {
                return hand.GroupBy(h => h.Rank)
                .Where(g => g.Count() == 4)
                .Any();
            }
        }
        public bool IsFlush
        {
            get
            {
                return hand.GroupBy(h => h.Suite).Count() == 1;
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
                var ordered = hand.OrderBy(h => h.Rank).ToArray();
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
        public HandType Eval() //returns the hands value and type
        {
            if (IsRoyalStraightFlush)
                return handType = HandType.RoyalStraightFlush;
            else if (IsStraightFlush)
                return handType = HandType.StraightFlush;
            else if (IsFourOfAKind)
                return handType = HandType.FourOfAKind;
            else if (IsFullHouse)
                return handType = HandType.FullHouse;
            else if (IsFlush)
                return handType = HandType.Flush;
            else if (IsStraight)
                return handType = HandType.Straight;
            else if (IsThreeOfAKind)
                return handType = HandType.ThreeOfAKind;
            else if (IsTwoPair)
                return handType = HandType.TwoPairs;
            else if (IsPair)
                return handType = HandType.Pair;
            else
            {
                return handType = HandType.HighCard;
            }
        }
        public void SortHand() //sorts the hand first by rank and then by suit
        {
            hand = hand.OrderBy(card => card.Rank).ToArray();
            hand = hand.OrderBy(card => card.Suite).ToArray();
        }
        public void RemoveCardFromHand(int index)
        {
            hand[index] = null;
        }
        //Lägg till kort på hand??
        public void AddCardToHand(Card card)
        {
            for (int i = 0; i < 5; i++)
            {
                if (hand[i] == null)
                {
                    hand[i] = card;
                    break;
                }
            }
        }
    }
}