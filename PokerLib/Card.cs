namespace Poker
{
    class Card: ICard
    {
        private Suite suite;//{ get; set; }
        public Suite Suite {get=> suite;}
        private Rank rank;//{ get; set; }
        public Rank Rank {get=> rank;}

        public Card(Suite suite, Rank rank)
        {
            this.suite=suite;
            this.rank=rank;
        } 
    }
}