using System;
using System.Collections.Generic;
using System.Linq;

namespace Phutball.Jumpers
{
    public static class Direction
    {
        public static readonly Tuple<int, int> N = new Tuple<int, int>(-1, 0);
        public static readonly Tuple<int, int> NE = new Tuple<int, int>(-1, 1);
        public static readonly Tuple<int, int> E = new Tuple<int, int>(0, 1);
        public static readonly Tuple<int, int> SE = new Tuple<int, int>(1, 1);
        public static readonly Tuple<int, int> S = new Tuple<int, int>(1, 0);
        public static readonly Tuple<int, int> SW = new Tuple<int, int>(1, -1);
        public static readonly Tuple<int, int> W = new Tuple<int, int>(0, -1);
        public static readonly Tuple<int, int> NW = new Tuple<int, int>(-1, -1);
    }


    public class JumpersFactory 
    {
        private readonly IFieldsGraph _fieldsGraph;
        private readonly int _stoneRadius;

        private static readonly Tuple<int, int>[] AllDirections = new[]
                                                            {
                                                                Direction.N,
                                                                Direction.NE,
                                                                Direction.NW,
                                                                Direction.W,
                                                                Direction.E,
                                                                Direction.SW,
                                                                Direction.SE,
                                                                Direction.S
                                                            };

        public JumpersFactory(IFieldsGraph fieldsGraph, int stoneRadius)
        {
            _fieldsGraph = fieldsGraph;
            _stoneRadius = stoneRadius;
        }

        public JumpersFactory(IFieldsGraph fieldsGraph):this(fieldsGraph, 1)
        {
        }

        public IEnumerable<IJump> AllJumps(Field from)
        {
            return AllDirections.ToList().Shuffle().Select(direction=> new FieldJump(_fieldsGraph, from, direction));
        }

        public IJump FromTo(Field from, Field to)
        {
            return new FieldJump(_fieldsGraph, from, from.GetDirectionTo(to));
        }

        public static IEnumerable<Tuple<int, int>> Directions()
        {
            return AllDirections;
        }

        public IEnumerable<Field> AllPlaces(Field whiteField)
        {
            return ChoosValidPlacesForBlackField(whiteField, Directions());
        }

        private IEnumerable<Field> ChoosValidPlacesForBlackField(Field whiteField, IEnumerable<Tuple<int, int>> directions)
        {
            var whiteCords = _fieldsGraph.GetCoordinates(whiteField);
            return directions.Shuffle()
                .SelectMany(dir=> Enumerable.Range(1,_stoneRadius).Select(radius=> dir.Multiply(radius)))
                .Distinct()
                .Select(dir=> new StoneMover(dir).Next(whiteCords))
                .Where(dir=> _fieldsGraph.CanPlaceBlackStone(dir))
                .Select(validCord=> _fieldsGraph.GetField(validCord));
        }

        public IEnumerable<Field> PlacesForBlack(IEnumerable<Tuple<int, int>> places, Field whiteField)
        {
            return ChoosValidPlacesForBlackField(whiteField, places);
        }
    }
}