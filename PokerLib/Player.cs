using System.Collections.Generic;
using Newtonsoft.Json;

namespace Poker
{
    class Player : IPlayer
    {
        private string name{get; set;}
        public string Name { get => name; }
        private int wins;
        public int Wins { get=>wins; set=>wins=value; }
        private List<Card> discard = new List<Card>();

        [JsonIgnore]
        public ICard[] Discard { get; set; }
        [JsonIgnore]
        public HandType HandType { get => hand.HandType; }
        private Hands hand = new Hands();
        [JsonIgnore]
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

        public List<Card> Discard_
        {
            get { return discard; }
            set { value = discard; }
        }

        public void SortPlayerHand()
        {
            Hands.SortHand();
        }

        public void ReceiveCards(Card card)
        {
            Hands.AddCardToHand(card);
        }

        //=======!!!!!!!!!!!!!!=========
        //Tog bort dicarded card och la i table

        public void Win()
        {
            wins++;
        }
    }
}