using System.Collections.Generic;
using System.Linq;

namespace EndGames.Phutball.Jumpers
{
    public class StoneJumper : IStoneJumper
    {
        private readonly IFieldsGraph _fieldsGraph;
        private readonly Field _from;
        private readonly Field _to;
        private DirectedJumpersFactory _jumpersFactory;

        public StoneJumper(IFieldsGraph fieldsGraph, Field from, Field to)
        {
            _fieldsGraph = fieldsGraph;
            _from = from;
            _to = to;
            _jumpersFactory= new DirectedJumpersFactory(_fieldsGraph);
        }


        public IJump FindValidJump()
        {
            if (_from.Selected == false)
                return null;

            if (_to.HasStone)
                return null;

            return TryFindingValidJumper();
        }

        private IJump TryFindingValidJumper()
        {
            var jumper = _jumpersFactory.FromTo(_from, _to);
            if(_to.Equals(jumper.EndField))
            {
                return jumper;
            }
            return null;
        }
    }
}