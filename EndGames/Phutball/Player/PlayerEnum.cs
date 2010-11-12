using EndGames.Phutball.Search.BoardValues;
using EndGames.Utils;

namespace EndGames.Phutball.Player
{
    public class PlayerEnum : EnumOf<IPlayer>
    {
        public static readonly IPlayer First = new Player("First", TargetBorderEnum.Upper);
        public static readonly IPlayer Second = new Player("Second", TargetBorderEnum.Bottom);
    }
}