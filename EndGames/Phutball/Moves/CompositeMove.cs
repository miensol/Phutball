using System;
using System.Collections.Generic;
using System.Text;

namespace EndGames.Phutball.Moves
{
    public class CompositeMove<TBoard> : IMove<TBoard>
    {
        private readonly List<IMove<TBoard>> _moves = new List<IMove<TBoard>>();

        public CompositeMove(IEnumerable<IMove<TBoard>> moves)
        {
            _moves.AddRange<IMove<TBoard>>(moves);
        }

        public void Perform(TBoard board)
        {
            _moves.Each(move => move.Perform(board));
        }

        public void Undo(TBoard board)
        {
            _moves.Each(move => move.Undo(board));
        }

        public void Add(params IMove<TBoard>[] move)
        {
            _moves.AddRange(move);
        }

        public IEnumerable<IMove<TBoard>> GetMoves()
        {
            return _moves;
        }

        public override string ToString()
        {
            var sb = new StringBuilder("CompisteMove {0}\n".ToFormat(_moves.Count));
            _moves.Each(move => sb.Append(move.ToString()).Append(Environment.NewLine));
            return sb.ToString();
        }
    }
}