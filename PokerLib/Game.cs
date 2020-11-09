using System;
using Poker.Lib;

namespace Poker
{
    class Game : IPokerGame
    {
        public event OnNewDeal NewDeal;
        public event OnSelectCardsToDiscard SelectCardsToDiscard;
        public event OnRecievedReplacementCards RecievedReplacementCards;
        public event OnShowAllHands ShowAllHands;
        public event OnWinner Winner;
        public event OnDraw Draw;


        public IPlayer[] Players { get; set;}

        public Game(string[] playerNames)
        {
        
        }
        public Game(string fileName)
        {

        }

        public void RunGame()
        {
            Deck deck = new Deck();
            Dealer dealer = new Dealer();
            dealer.Shuffle();
            NewDeal();
            //dealer.Deal(Table);
        }

        public void LoadGame(string file)
        {

        }

        public void WinnerPlayer(IPlayer winner)
        {

        }

        public void DrawPlayer()
        {

        }

        public void SaveGameAndExit(string fileName)
        {
            throw new NotImplementedException();
        }

        public void Exit()
        {
            Environment.Exit(0);
        }
    }
}