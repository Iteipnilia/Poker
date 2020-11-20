using System;
using System.IO;
using System.Xml.Serialization;
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

        public LoadGame(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Player));
            FileStream fs = new FileStream(fileName, FileMode.Open);
        }

        public Game(string[] playerNames)
        {
            Table.AddPlayerToTable();
        }

        public void RunGame()
        {
            Deck deck = new Deck();
            Dealer dealer = new Dealer();
            Table table = new Table();
            Hands Hand = new Hands();
            dealer.Shuffle();
            NewDeal();
            table.DealTable();
            foreach (Player player in Players)
            {
                Hand.SortHand();
                Hand.Eval();
                SelectCardsToDiscard(player);
                player.DiscardCard(index);
                player.ReceiveCards(dealer.GiveCard());
                RecievedReplacementCards(player);
                Hand.SortHand();
                Hand.Eval();
            }
            ShowAllHands();
            table.CompareHands();
            //winner/draw samt ta tillbaka korten till leken kvar
            
        }
        public void WinnerPlayer(IPlayer winner)
        {
        }
        public void DrawPlayer()
        {
        }

        public void SaveGameAndExit(string fileName)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Player)); 
            TextWriter Writer = new StreamWriter(fileName);
            Writer.Close();
        }

        public void Exit()
        {
            Environment.Exit(0);
        }
    }
}