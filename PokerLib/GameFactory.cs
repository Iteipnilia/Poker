namespace Poker.Lib
{
    public static class GameFactory
    {
        public static IPokerGame NewGame(string[] playerNames)
        {
<<<<<<< HEAD
            Game game= new Game(playerNames);
            return game;
=======
            var Game = new Game(playerNames);
            return Game;
>>>>>>> bc01e700bceb1e64a7c0bf601c5f6f9725713b66
        }

        public static IPokerGame LoadGame(string fileName)
        {
            var LoadGame = new Game(fileName);
            return LoadGame;
        }
    }
}