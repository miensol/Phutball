using System.Linq;

namespace Phutball.Search.BoardValues
{
    public class BlackkStoneToTargetBorderCount : IValueOf<JumpNode>
    {
        private TargetBorder _targetBorder;
        private int _goodBlackFieldsWeight;
        private int _originalDistance;

        public BlackkStoneToTargetBorderCount(IPlayersState playersState, IFieldsGraph actualGraph, int goodBlackFieldsWeight)
        {
            _targetBorder = playersState.CurrentPlayer.GetTargetBorder(actualGraph);
            _originalDistance = _targetBorder.GetDistanceFrom(actualGraph.GetWhiteField());
            _goodBlackFieldsWeight = goodBlackFieldsWeight;
        }

        public int GetValue(JumpNode valueSubject)
        {
            var blackFields = valueSubject.ActualGraph.GetBlackFields();
            if(blackFields.Count() == 0)
            {
                return 0;
            }
            var goodBlackFields = blackFields.Count(field => _targetBorder.GetDistanceFrom(field) < _originalDistance);
            return goodBlackFields*_goodBlackFieldsWeight;
        }
    }
}