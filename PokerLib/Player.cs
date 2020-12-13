
namespace Poker
{
    class Player : IPlayer
    {
        private string name { get; set; }
        public string Name { get => name; }
        private int wins;
        public int Wins { get=>wins; set=>wins=value; }
        public ICard[] Discard { get; set; }
        public HandType HandType { get => hand.HandType; }
        private Hands hand = new Hands();
        public ICard[] Hand { get => (hand.Hand).ToArray(); }

        public Player(string name)
        {
            this.name = name;
            wins = 0;
        }
        public Player(string name, int wins)
        {
            this.name = name;
            this.wins = wins;
        }

        public Hands Hands
        {
            get { return hand; }
            set { value = hand; }
        }

        public void SortPlayerHand()
        {
            Hands.SortHand();
        }

        public void ReceiveCards(Card card)
        {
            Hands.AddCardToHand(card);
        }

        public void Win()
        {
            wins++;
        }
    }
}