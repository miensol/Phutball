using System.Linq;

namespace Phutball.Search.BoardValues
{
    public class BlackStoneToTargetBorderCount : IValueOf<JumpNode>
    {
        private TargetBorder _targetBorder;
        private int _goodBlackFieldsWeight;
        private int _originalDistance;

        public BlackStoneToTargetBorderCount(IPlayersState playersState, IFieldsGraph actualGraph, int goodBlackFieldsWeight)
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
    
    public class AverageBlackStoneToTargetBorderDistance : IValueOf<JumpNode>
    {
        private TargetBorder _targetBorder;
        private int _averageDistanceWeight;
        private int _originalDistance;

        public AverageBlackStoneToTargetBorderDistance(IPlayersState playersState, IFieldsGraph actualGraph, int averageDistanceWeight)
        {
            _targetBorder = playersState.CurrentPlayer.GetTargetBorder(actualGraph);
            _originalDistance = _targetBorder.GetDistanceFrom(actualGraph.GetWhiteField());
            _averageDistanceWeight = averageDistanceWeight;
        }

        public int GetValue(JumpNode valueSubject)
        {
            var blackFields = valueSubject.ActualGraph.GetBlackFields();
            if(blackFields.Count() == 0)
            {
                return 0;
            }
            var averageDistance = blackFields.Where(field => _targetBorder.GetDistanceFrom(field) < _originalDistance).Average(field=> _targetBorder.GetDistanceFrom(field));
            return (int) (averageDistance*_averageDistanceWeight);
        }
    }
}