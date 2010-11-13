using EndGames.Phutball.Search.BoardValues;

namespace EndGames.Phutball.Player
{
    public class Player : IPlayer
    {
        public string Name { get; private set; }

        public Player(string name)
        {
            Name = name;
        }
    }
}