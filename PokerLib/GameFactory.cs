namespace Poker.Lib
{
    public static class GameFactory
    {
        public static IPokerGame NewGame(string[] playerNames)
        {
            Game game= new Game(playerNames);
            return game;
        }

        public static IPokerGame LoadGame(string fileName)
        {
            return null;
        }
    }
}