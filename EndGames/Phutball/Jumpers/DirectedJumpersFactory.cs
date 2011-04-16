using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace EndGames.Phutball.Jumpers
{
    public class DirectedJumpersFactory 
    {
        private readonly IFieldsGraph _fieldsGraph;

        public DirectedJumpersFactory(IFieldsGraph fieldsGraph)
        {
            _fieldsGraph = fieldsGraph;
        }

        public IEnumerable<IJump> All(Field from)
        {
            return Directions().Select(direction=> new FieldJump(_fieldsGraph, from, direction));
        }

        public IJump FromTo(Field from, Field to)
        {
            return new FieldJump(_fieldsGraph, from, from.GetDirectionTo(to));
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

        public IEnumerable<Field> AllPlaces(Field whiteField)
        {
            var whiteCords = _fieldsGraph.GetCoordinates(whiteField);
            return Directions().Select(dir=> new StoneMover(dir).Next(whiteCords))
                .Where(newCord=> _fieldsGraph.CanPlaceBlackStone(newCord))
                .Select(validCord=> _fieldsGraph.GetField(validCord));

        }
    }
}