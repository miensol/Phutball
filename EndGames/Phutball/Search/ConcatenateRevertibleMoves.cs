using System;
using System.Collections;
using System.Collections.Generic;

namespace EndGames.Phutball.Search
{
    public class ConcatenateRevertibleMoves : IEnumerable<IJumpNodeTreeWithFactory>
    {
        private readonly IRevertibleMovesCollection _first;
        private readonly IRevertibleMovesCollection _second;
        private IRevertibleMovesCollection _last;

        public ConcatenateRevertibleMoves(IRevertibleMovesCollection first, IRevertibleMovesCollection second)
        {
            _first = first;
            _second = second;
        }


        public IEnumerator<IJumpNodeTreeWithFactory> GetEnumerator()
        {
            RevertPrevious();
            foreach (var elem in _first)
            {
                RevertPrevious();
                _last = _first;
                yield return elem;
            }
            RevertPrevious();
            foreach (var elem in _second)
            {
                RevertPrevious();
                _last = _second;
                yield return elem;
            }
            RevertPrevious();
        }

        private void RevertPrevious()
        {
             if(_last != null)
             {
                 _last.RevertPrevious();
                 _last = null;
             }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}