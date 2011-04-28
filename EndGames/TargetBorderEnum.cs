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
//                .ChoosePlacesForBlackStoneUsing(all => all.Take(5).Concat(new[]
//                                                                              {
//                                                                                  Direction.N.Multiply(2).Add(
//                                                                                      Direction.W),
//                                                                                  Direction.N.Multiply(2).Add(
//                                                                                      Direction.E),
//                                                                              }))
                .ComparePositionsUsing((left,right)=> left < right)
                .EndRowIndexIs(me=> 1);

            Bottom = new TargetBorder(() => fieldsGraph.RowCount - 2, Bottomname)
                .OppositeIs(() => Upper)
                .CountDistanceUsing(new DistanceToBottomBorderCounter(fieldsGraph))
//                .ChoosePlacesForBlackStoneUsing(all => all.Reverse().Take(5).Concat(new[]
//                                                                                        {
//                                                                                            Direction.S.Multiply(2).Add(
//                                                                                                Direction.E),
//                                                                                            Direction.S.Multiply(2).Add(
//                                                                                                Direction.W),
//                                                                                        }))
                .EndRowIndexIs((me)=> me.RowIndex )
                .ComparePositionsUsing((left, right) => left > right);
        }

        public TargetBorder Upper { get; private set; }
        public TargetBorder Bottom { get; private set; }
    }
}