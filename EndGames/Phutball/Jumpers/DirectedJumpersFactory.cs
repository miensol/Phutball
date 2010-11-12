using System;
using System.Collections.Generic;
using System.Linq;

namespace EndGames.Phutball.Jumpers
{
    public static class DirectedJumpersFactory 
    {
        public static IEnumerable<IJump> All(IFieldsGraph fieldsGraph, Field from)
        {
            return Directions().Select(direction=> GetJumper(direction, fieldsGraph, from));
        }

        private static IEnumerable<Tuple<int, int>> Directions()
        {
            yield return new Tuple<int, int>(-1, 0);
            yield return new Tuple<int, int>(-1, 1);
            yield return new Tuple<int, int>(0, 1);
            yield return new Tuple<int, int>(1, 1);
            yield return new Tuple<int, int>(1, 0);
            yield return new Tuple<int, int>(1, -1);
            yield return new Tuple<int, int>(0, -1);
            yield return new Tuple<int, int>(-1, -1);
        }

        private static IJump GetJumper(Tuple<int, int> direction, IFieldsGraph graph, Field from)
        {
            return new FieldJump(graph, from, direction);
        }    
    }
}