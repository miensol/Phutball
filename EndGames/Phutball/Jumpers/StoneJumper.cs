using System;
using System.Collections.Generic;
using System.Linq;

namespace EndGames.Phutball.Jumpers
{
    public class StoneJumper : IStoneJumper
    {
        private readonly IFieldsGraph _fieldsGraph;
        private readonly Field _from;
        private readonly Field _to;

        public StoneJumper(IFieldsGraph fieldsGraph, Field from, Field to)
        {
            _fieldsGraph = fieldsGraph;
            _from = from;
            _to = to;
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
            IEnumerable<IJump> jumpers = DirectedJumpersFactory.All(_fieldsGraph, _from);
            return jumpers.FirstOrDefault(CanJumpToDestination());
        }

        private Func<IJump, bool> CanJumpToDestination()
        {
            return jumper => jumper.EndField == _to;
        }

    }
}