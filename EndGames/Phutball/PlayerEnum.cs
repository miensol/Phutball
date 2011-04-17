using System;
using EndGames.Utils;
using EndGames.Phutball.Search.BoardValues;

namespace EndGames.Phutball
{
    public class PlayerEnum : EnumOf<Player>
    {
        public static Player First()
        {
            return new Player("First", (graph) => graph.Borders().Bottom); 
        }
        public static Player Second()
        {
            return new Player("Second", (graph) => graph.Borders().Upper);
        }

        public static Player Computer()
        {
            var computer = new Player("Computer", (graph) => graph.Borders().Upper);
            computer.IsComputer();
            return computer;
        }
    }
}