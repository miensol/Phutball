using EndGames.Utils;

namespace EndGames.Phutball.Player
{
    public class PlayerEnum : EnumOf<IPlayer>
    {
        public static readonly IPlayer First = new Player("First");
        public static readonly IPlayer Second = new Player("Second");
    }
}