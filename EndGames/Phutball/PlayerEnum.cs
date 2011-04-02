using EndGames.Utils;
using EndGames.Phutball.Search.BoardValues;

namespace EndGames.Phutball
{
    public class PlayerEnum : EnumOf<Player>
    {
        public static readonly Player First = new Player("First", (graph)=> graph.Borders().Bottom);
        public static readonly Player Second = new Player("Second",(graph)=> graph.Borders().Upper);
    }
}