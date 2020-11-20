namespace Poker
<<<<<<< HEAD
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
=======
{  
    class Card : ICard
    {
        public Card(Suite suite, Rank rank) 
        {
            Suite = suite;
            Rank = rank;
        }
        public Suite Suite { get; set; }

        public Rank Rank { get; set; }
>>>>>>> bc01e700bceb1e64a7c0bf601c5f6f9725713b66
    }
}