using NUnit.Framework;
using Poker;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static Poker.Suite;
using static Poker.Rank;
using static Poker.HandType;

namespace Poker.Lib.UnitTest
{
    public class GameFactoryTest
    {
        [Test]
        public void CanCreateNewGame()
        {
            IPokerGame game;
            string[] playerNames = new string[2] { "Test1", "Test2" };

            game = GameFactory.NewGame(playerNames);

            Assert.AreEqual(playerNames[0], game.Players[0].Name);
            Assert.AreEqual(playerNames[1], game.Players[1].Name);
            Assert.AreEqual(playerNames[0], "Test1");
            Assert.AreEqual(playerNames[1], "Test2");
            
        }

        [Test]
        public void CanCreateLoadGame()
        {
        
        }
    }
}