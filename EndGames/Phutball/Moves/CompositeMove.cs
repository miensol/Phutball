using System;
using System.Collections.Generic;
using System.Text;
using EndGames.Phutball.PlayerMoves;

namespace EndGames.Phutball.Moves
{
    public class CompositeMove : IPhutballMove
    {
        private readonly List<IPhutballMove> _moves = new List<IPhutballMove>();

        public CompositeMove(IEnumerable<IPhutballMove> moves)
        {
            _moves.AddRange(moves);
        }

        public void Perform(PhutballMoveContext board)
        {
            _moves.Each(move => move.Perform(board));
        }

        public void Undo(PhutballMoveContext board)
        {
            _moves.Each(move => move.Undo(board));
        }

        public IEnumerable<IPhutballMove> GetMoves()
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