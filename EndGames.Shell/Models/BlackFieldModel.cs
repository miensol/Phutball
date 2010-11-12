using System.Windows;

namespace EndGames.Shell.Models
{
    public class BlackFieldModel : FieldModel
    {
        public const string WhitePawnColor = "White";
        public const string BlackPawnColor = "Black";
        public bool HasPawn { get; set; }
        public string Color { get; set; }

        public Visibility PawnVisible { get{ return HasPawn ? Visibility.Visible : Visibility.Collapsed;}}
    }
}