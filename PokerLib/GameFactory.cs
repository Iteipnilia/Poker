namespace Poker.Lib
{
    public static class GameFactory
    {
        public static IPokerGame NewGame(string[] playerNames)
        {
            var Game = new Game(playerNames);
            return Game;
        }

        public static IPokerGame LoadGame(string fileName)
        {
            var LoadGame = new Game(fileName);
            return LoadGame;
        }
    }
}