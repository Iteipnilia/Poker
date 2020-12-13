using NUnit.Framework;
using Poker;
using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
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
            IPokerGame game;
            Table table = new Table();
            string fileName = "savedGame.txt";
            string[] names = new string[2] { "Test1", "Test2" };
            int[] wins = new int[2] { 7, 3 };

            string json = JsonConvert.SerializeObject(names);
            json += (" " + JsonConvert.SerializeObject(wins));
            File.WriteAllText(fileName, json);

            string Json = File.ReadAllText(fileName);
            string[] data = Json.Split(' ');
            string[] Names = JsonConvert.DeserializeObject<String[]>(data[0]);
            int[] Wins = JsonConvert.DeserializeObject<int[]>(data[1]);

            for (int i = 0; i < names.Length; i++)
                {
                    table.AddPlayerToTable(names[i], wins[i]);
                }


            game = GameFactory.LoadGame(fileName);

            Assert.AreEqual(game.Players[0].Name, "Test1");
            Assert.AreEqual(game.Players[1].Name, "Test2");
            Assert.AreEqual(game.Players[0].Wins, 7);
            Assert.AreEqual(game.Players[1].Wins, 3);
        
        }
    }
}