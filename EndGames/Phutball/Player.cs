using System;
using EndGames.Phutball.Search.BoardValues;

namespace EndGames.Phutball
{
    public class Player
    {
        private readonly Func<IFieldsGraph, TargetBorder> _targetBorderAccessor;
        public string Name { get; private set; }

        public bool IsOnTheMove { get; set; }

        public Player(string name, Func<IFieldsGraph,TargetBorder> targetBorderAccessor)
        {
            _targetBorderAccessor = targetBorderAccessor;
            Name = name;
        }

        public TargetBorder GetTargetBorder(IFieldsGraph fieldsGraph)
        {
            return _targetBorderAccessor(fieldsGraph);
        }

       
    }
}