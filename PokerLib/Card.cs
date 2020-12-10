using System;
namespace Poker
{
    class Card : ICard
    {
        private Suite suite{ get; set; }
        public Suite Suite { get => suite; }
        private Rank rank{ get; set; }
        public Rank Rank { get => rank; }

        public Card(Suite suite, Rank rank)
        {
            this.suite = suite;
            this.rank = rank;
        }

        public override bool Equals(Object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Card c = (Card)obj;
                return (Suite == c.Suite) && (Rank == c.Rank);
            }
        }

        public static bool operator ==(Card lhs, Card rhs)
        {
            // Check for null on left side.
            if (Object.ReferenceEquals(lhs, null))
            {
                if (Object.ReferenceEquals(rhs, null))
                {
                    // null == null = true.
                    return true;
                }
                // Only the left side is null.
                return false;
            }
            // Equals handles case of null on right side.
            return lhs.Equals(rhs);
        }
        public static bool operator !=(Card lhs, Card rhs)
        {
            return !(lhs == rhs);
        }
    }
}