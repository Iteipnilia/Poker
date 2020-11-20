<<<<<<< HEAD
namespace Poker
{
    class Player: IPlayer
=======
using Poker;

class Player : IPlayer
>>>>>>> bc01e700bceb1e64a7c0bf601c5f6f9725713b66
    {
        private string name;
        public string Name{get=> name;}
        private int wins;
<<<<<<< HEAD
        public int Wins{get=> wins;}
        private Card[] discard= new Card[5];
        public ICard[] Discard {set => Discard = discard;}
        public HandType HandType {get=>hand.HandType;}
        private Hands hand{get; set;}
        public ICard[] Hand{get=> Hands.Hand;}

        public Player(string name_)
=======
        private Hands hand;
        private Card[] discard;
        public HandType HandType {get;}
        public Player(string name)
>>>>>>> bc01e700bceb1e64a7c0bf601c5f6f9725713b66
        {
            this.name = name;
            wins=0;
            hand= new Hands();
<<<<<<< HEAD
            //discard= new Card[5];

        }
        public Hands Hands
=======
            discard= new Card[5];
        }
        public Hands Hand
>>>>>>> bc01e700bceb1e64a7c0bf601c5f6f9725713b66
        {
            get { return hand; }
            set { value = hand; }
        }
<<<<<<< HEAD

        public Card[] Discard_
=======
        public Card[] Discard
>>>>>>> bc01e700bceb1e64a7c0bf601c5f6f9725713b66
        {
            get { return discard; }
            set { value = discard; }
        }
        public string Name=>name;
        public int Wins=>wins;

        ICard[] IPlayer.Hand => throw new System.NotImplementedException();

<<<<<<< HEAD
        public void DetermineHandType(Hands hand)
        {
            hand.Eval();
        }
=======
        ICard[] IPlayer.Discard { set => throw new System.NotImplementedException(); }
>>>>>>> bc01e700bceb1e64a7c0bf601c5f6f9725713b66

        // anropar metod i Hand för att tömma index på hand som ska bort
        // LÄgger till det borttagna kortet i array discard
        public void DiscardCard(int index)
<<<<<<< HEAD
        {            
           discard[index] = hand.Hand[index];//======!!!!!!!!!!!!!!!!!==========
           Hands.RemoveCardFromHand(index);
=======
        {
           discard[index] = hand.RemoveCardFromHand(index);
>>>>>>> bc01e700bceb1e64a7c0bf601c5f6f9725713b66
        }
        public void ReceiveCards(Card card)
        {
            hand.AddCardToHand(card);
        }
    }