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
        public IPlayer[] Players { get=>table.Players;}
        private Table table;
        public Game(string[] playerNames)
        {
            
        }
        public void RunGame()
        {
            
            //table= new Table();
            //deck.Shuffle();
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