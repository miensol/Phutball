using Phutball.Search.BoardValues;

namespace Phutball
{
    public class TargetBorderEnum
    {
        public const string UpperName = "Upper";
        private const string Bottomname = "Bottom";

        public TargetBorderEnum(IFieldsGraph fieldsGraph)
        {
            Upper = new TargetBorder(() => 1, UpperName)
                .OppositeIs(() => Bottom)
                .CountDistanceUsing(new DistanceToUpperBorderCounter(fieldsGraph))
                .ComparePositionsUsing((left,right)=> left < right)
                .EndRowIndexIs(me=> 1)
                .WiningIndexes((me)=> new[]{0,1});

            Bottom = new TargetBorder(() => fieldsGraph.RowCount - 2, Bottomname)
                .OppositeIs(() => Upper)
                .CountDistanceUsing(new DistanceToBottomBorderCounter(fieldsGraph))
                .EndRowIndexIs((me)=> me.RowIndex )
                .ComparePositionsUsing((left, right) => left > right)
                .WiningIndexes((me)=> new[]{me.RowIndex, me.RowIndex + 1});
        }

        public TargetBorder Upper { get; private set; }
        public TargetBorder Bottom { get; private set; }
    }
}