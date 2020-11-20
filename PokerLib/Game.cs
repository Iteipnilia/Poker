using System;
using Poker.Lib;
<<<<<<< HEAD
=======

>>>>>>> bc01e700bceb1e64a7c0bf601c5f6f9725713b66
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
<<<<<<< HEAD
        public IPlayer[] Players { get=>table.Players;}
        private Table table;
        public Game(string[] playerNames)
=======


        public IPlayer[] Players { get; set;}

        public Game(string[] playerNames)
        {
        
        }
        public Game(string fileName)
>>>>>>> bc01e700bceb1e64a7c0bf601c5f6f9725713b66
        {
            
        }
        public void RunGame()
        {
            
            //table= new Table();
            //deck.Shuffle();
            NewDeal();
            //dealer.Deal(Table);
        }
<<<<<<< HEAD
=======

        public void RunGame()
        {
            Deck deck = new Deck();
            Dealer dealer = new Dealer();
            dealer.Shuffle();
            NewDeal();
            //dealer.Deal(Table);
        }

>>>>>>> bc01e700bceb1e64a7c0bf601c5f6f9725713b66
        public void LoadGame(string file)
        {
        }
<<<<<<< HEAD
=======

>>>>>>> bc01e700bceb1e64a7c0bf601c5f6f9725713b66
        public void WinnerPlayer(IPlayer winner)
        {
        }
        public void DrawPlayer()
        {
        }
<<<<<<< HEAD
        public void SaveGameAndExit(string fileName)
        {
            throw new NotImplementedException();
        }
        public void Exit()
        {
=======

        public void SaveGameAndExit(string fileName)
        {
            throw new NotImplementedException();
        }

        public void Exit()
        {
>>>>>>> bc01e700bceb1e64a7c0bf601c5f6f9725713b66
            Environment.Exit(0);
        }
    }
}