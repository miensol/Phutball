using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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
            _moves.AsEnumerable().Reverse().Each(move => context.PerformMoves.Undo(move));
        }

        public IEnumerable<IPhutballMove> GetMoves()
        {
            return _moves;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            _moves.Each(move => sb.AppendFormat("{0} \n", move));
            return sb.ToString();
        }
    }
}