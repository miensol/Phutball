using EndGames.Utils;

namespace EndGames.Phutball.Search.BoardValues
{
    public class TargetBorderEnum : EnumOf<TargetBorder>
    {
        public static readonly TargetBorder Upper = new TargetBorder(() => 1, (rowIndex,actualIndex)=> actualIndex - rowIndex);
        public static readonly TargetBorder Bottom  = new TargetBorder(()=> PhutballOptions.Current.RowCount - 2, (rowIndex,actualIndex)=> rowIndex - actualIndex);
    }
}