using EndGames.Phutball.Search.BoardValues;

namespace EndGames.Phutball.Player
{
    public class Player : IPlayer
    {
        public string Name { get; private set; }
        public TargetBorder TargetBorder { get; set; }

        public TargetBorder Target { get; private set; }

        public Player(string name, TargetBorder targetBorder)
        {
            Name = name;
            TargetBorder = targetBorder;
        }
    }
}