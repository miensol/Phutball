using System;
using System.Collections.Generic;
using System.Text;

namespace EndGames.Phutball.Moves
{
    public class CompositeMove : IPhutballMove
    {
        private readonly List<IPhutballMove> _moves = new List<IPhutballMove>();

        public CompositeMove(IEnumerable<IPhutballMove> moves)
        {
            _moves.AddRange(moves);
        }

        public CompositeMove(params IPhutballMove[] moves)
        {
            _moves.AddRange(moves);
        }

        public void Perform(PhutballMoveContext context)
        {
            _moves.Each(move => context.PerformMoves.Perform(move));
        }

        public void Undo(PhutballMoveContext context)
        {
            _moves.Each(move => context.PerformMoves.Undo(move));
        }

        public IEnumerable<IPhutballMove> GetMoves()
        {
            return _moves;
        }

        public override string ToString()
        {
            var sb = new StringBuilder("CompisteMove {0} \n".ToFormat(_moves.Count));
            _moves.Each(move => sb.Append(move.ToString()).Append(Environment.NewLine));
            return sb.ToString();
        }
    }
}