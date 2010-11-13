using System;
using System.Collections;

namespace EndGames.Phutball.Jumpers
{
    public class FieldJumpIterator : IJumpDirectly
    {
        private readonly IFieldsGraph _fieldsGraph;
        private readonly Field _start;
        private readonly StoneMover _stoneMover;

        public FieldJumpIterator(Field start, IFieldsGraph fieldsGraph, StoneMover stoneMover)
        {
            _start = start;
            Current = _start;
            _fieldsGraph = fieldsGraph;
            _stoneMover = stoneMover;
        }


        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (ReachedEmptyField())
                return false;

            Tuple<int, int> nextCoordinates = GetNextCoordinates();
            if (InvalidPlaceForWhiteStone(nextCoordinates))
                return false;

            Field nextField = _fieldsGraph.GetField(nextCoordinates);
            if (ThereIsNoStoneToJumpOver(nextField))
                return false;

            PerformMove(nextField);

            return true;
        }

        public void Reset()
        {
            Current = _start;
        }

        public Field Current { get; private set; }

        object IEnumerator.Current
        {
            get { return Current; }
        }


        private bool ThereIsNoStoneToJumpOver(Field nextField)
        {
            return Current == _start && nextField.HasStone == false;
        }

        private void PerformMove(Field nextField)
        {
            Current = nextField;
        }

        private Tuple<int, int> GetNextCoordinates()
        {
            Tuple<int, int> currrentCoordinates = _fieldsGraph.GetCoordinates(Current);
            return _stoneMover.Next(currrentCoordinates);
        }

        private bool ReachedEmptyField()
        {
            return Current.HasStone == false;
        }

        private bool InvalidPlaceForWhiteStone(Tuple<int, int> cords)
        {
            return _fieldsGraph.IsValidPlaceForWhiteField(cords) == false;
        }
    }
}