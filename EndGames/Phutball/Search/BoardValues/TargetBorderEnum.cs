using EndGames.Utils;

namespace EndGames.Phutball.Search.BoardValues
{
    public class TargetBorderEnum : EnumOf<TargetBorder>
    {
        public static readonly TargetBorder Upper = new TargetBorder(() => 1)
            .OppositeIs(() => Bottom)
            .CountDistanceUsing(new DistanceToUpperBorderCounter());
        public static readonly TargetBorder Bottom  = new TargetBorder(()=> PhutballOptions.Current.RowCount - 2)
            .OppositeIs(()=> Upper)
            .CountDistanceUsing(new DistanceToBottomBorderCounter());
    }
}