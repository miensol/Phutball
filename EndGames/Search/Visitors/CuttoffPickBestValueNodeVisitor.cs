using System;
using System.Collections.Generic;
using System.Linq;
using Phutball.PlayerMoves;
using Phutball.Search.BoardValues;

namespace Phutball.Search.Visitors
{
    public class CuttoffPickBestValueNodeVisitor : ISearchNodeVisitor<JumpNode>, IFieldsUpdater
    {
        public int CuttoffsCount { get; set; }
        public bool CuttoffToTargetBorder { get; set; }
        private readonly TargetBorder _targetBorder;
        private readonly IFieldsGraph _fieldsGraph;
        public PickBestValueNodeVisitor PickBestValue { get; private set; }

        public IPerformMoves MovesPerformer { get; private set; }

        private int _bestWhiteStonePostion;
        private HashSet<Field> _blackFields;
        private Dictionary<int, HashSet<Field>> _blackFieldsByRowIndex;
        private readonly int _targetBorderRowEndIndex;


        public CuttoffPickBestValueNodeVisitor(TargetBorder targetBorder, IFieldsGraph fieldsGraph, IPlayersState playersState )
        {
            _targetBorder = targetBorder;
            _fieldsGraph = fieldsGraph;
            MovesPerformer = new PerformMoves(this, playersState);
            PickBestValue = new PickBestValueNodeVisitor(targetBorder, fieldsGraph, MovesPerformer);
            InitliazeBlackBuckets(fieldsGraph);
            PickBestValue.MaxUpdated += OnBestPositionUpdated;
            _targetBorderRowEndIndex = _targetBorder.EndRowIndex;
        }

        private void OnBestPositionUpdated()
        {
            _bestWhiteStonePostion = PickBestValue.CurrentMaxNode.ActualGraph.GetWhiteField().RowIndex;
        }

        private void InitliazeBlackBuckets(IFieldsGraph fieldsGraph)
        {
            _bestWhiteStonePostion = fieldsGraph.GetWhiteField().RowIndex;
            var blackFields = fieldsGraph.GetBlackFields();
            _blackFields = blackFields.ToHashSet();
            _blackFieldsByRowIndex = _fieldsGraph.RowCount.Times((row) => new {row, fields = new HashSet<Field>()})
                .ToDictionary(item=> item.row, item=> item.fields);
            blackFields.Each(field => _blackFieldsByRowIndex[field.RowIndex].Add(field));
        }

        public void OnEnter(ITree<JumpNode> node, ITreeSearchContinuation treeSearchContinuation)
        {
            PickBestValue.OnEnter(node, treeSearchContinuation);
            var whiteField = node.Node.ActualGraph.GetWhiteField();
            var bestAlready = _bestWhiteStonePostion;
            if(CuttoffToBestPosition(treeSearchContinuation, bestAlready, whiteField) == false)
            {
                if(CuttoffToTargetBorder)
                {
                    CuttoffToBestPosition(treeSearchContinuation, _targetBorderRowEndIndex, whiteField);   
                }                
            }

        }

        private bool CuttoffToBestPosition(ITreeSearchContinuation treeSearchContinuation, int targetRowIndex, Field whiteField)
        {
            if(_targetBorder.IsLeftCloserThanRigth(targetRowIndex, whiteField.RowIndex))
            {
                var minRow = Math.Min(targetRowIndex, whiteField.RowIndex);
                var maxRow = Math.Max(targetRowIndex, whiteField.RowIndex);
                int blackFieldsForJump = 0;
                int nulifyOddDistance = (maxRow - minRow + 1)%2;
                if(targetRowIndex < whiteField.RowIndex)
                {
                    blackFieldsForJump = CountBlackFieldsBetween(minRow + nulifyOddDistance, maxRow - 1);    
                }else
                {
                    blackFieldsForJump = CountBlackFieldsBetween(minRow + 1, maxRow - nulifyOddDistance);
                }
                
                var mininmalRequired = (maxRow - minRow - 1)/2 + 1;
                if(mininmalRequired > blackFieldsForJump)
                {
                    treeSearchContinuation.DontEnterChildren();
                    CuttoffsCount++;
                    return true;
                }
            }
            return false;
        }

        private int CountBlackFieldsBetween(int minRow, int maxRow)
        {
            var blackCount = 0;
            for(var index= minRow;index<=maxRow; ++index)
            {
                blackCount += _blackFieldsByRowIndex[index].Any() ? 1 : 0;
            }
            return blackCount;
        }

        public void OnLeave(ITree<JumpNode> node, ITreeSearchContinuation treeSearchContinuation)
        {
            PickBestValue.OnLeave(node, treeSearchContinuation);
        }

        public void UpdateFields(params Field[] field)
        {
            _fieldsGraph.UpdateFields(field);
            UpdateBlackFieldsBuckets(field);
        }

        private void UpdateBlackFieldsBuckets(Field[] field)
        {
            for (int index = 0; index < field.Length; index++)
            {
                var current = field[index];
                if(_blackFields.Remove(current))
                {
                    _blackFieldsByRowIndex[current.RowIndex].Remove(current);
                }
                if(current.HasBlackStone)
                {
                    _blackFields.Add(current);
                    _blackFieldsByRowIndex[current.RowIndex].Add(current);
                }
            }
        }

        public Field GetWhiteField()
        {
            return _fieldsGraph.GetWhiteField();
        }
    }
}