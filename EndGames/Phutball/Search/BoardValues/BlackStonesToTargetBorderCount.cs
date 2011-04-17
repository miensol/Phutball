using System.Linq;

namespace EndGames.Phutball.Search.BoardValues
{
    public class BlackStonesToTargetBorderCount : IValueOf<JumpNode>
    {
        private TargetBorder _targetBorder;
        private int _goodBlackFieldsWeight;

        public BlackStonesToTargetBorderCount(IPlayersState playersState, IFieldsGraph actualGraph, int goodBlackFieldsWeight)
        {
            _targetBorder = playersState.CurrentPlayer.GetTargetBorder(actualGraph);
            _goodBlackFieldsWeight = goodBlackFieldsWeight;
        }

        public int GetValue(JumpNode valueSubject)
        {
            var whiteField = valueSubject.ActualGraph.GetWhiteField();
            var whiteDistance = _targetBorder.GetDistanceFrom(whiteField);


            var blackFields = valueSubject.ActualGraph.GetBlackFields();
            var goodBlackFields = blackFields.Count(field => _targetBorder.GetDistanceFrom(field) < whiteDistance);
            return goodBlackFields*_goodBlackFieldsWeight;
        }
    }
}