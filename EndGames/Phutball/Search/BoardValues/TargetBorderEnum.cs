namespace EndGames.Phutball.Search.BoardValues
{
    public class TargetBorderEnum
    {
        public TargetBorderEnum(IFieldsGraph fieldsGraph)
        {
            Upper =  new TargetBorder(() => 1, "Upper")
                .OppositeIs(() => Bottom)
                .CountDistanceUsing(new DistanceToUpperBorderCounter(fieldsGraph));
            Bottom = new TargetBorder(() => fieldsGraph.RowCount - 2, "Bottom")
                .OppositeIs(()=> Upper)
                .CountDistanceUsing(new DistanceToBottomBorderCounter(fieldsGraph));
        }

        public TargetBorder Upper { get; private set; }
        public TargetBorder Bottom { get; private set; }
    }
}