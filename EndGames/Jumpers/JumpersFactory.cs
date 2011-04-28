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

        public static readonly Tuple<int, int>[] All = new[]
                                                                      {
                                                                          N,
                                                                          NE,
                                                                          NW,
                                                                          W,
                                                                          E,
                                                                          SW,
                                                                          SE,
                                                                          S
                                                                      };
    }


    public class JumpersFactory 
    {
        private readonly IFieldsGraph _fieldsGraph;


        public JumpersFactory(IFieldsGraph fieldsGraph)
        {
            _fieldsGraph = fieldsGraph;
        }
        

//        public IEnumerable<IJump> AllJumps(Field from)
//        {
//            return AllDirections.ToList().Shuffle().Select(direction=> new FieldJump(_fieldsGraph, from, direction));
//        }
//        
//        public IEnumerable<IJump> AllJumps(Field from)
//        {
//            return AllDirectionsRandom.Select(direction => new FieldJump(_fieldsGraph, from, direction));
//        }
        
        public IEnumerable<IJump> AllJumps(Field from)
        {
            return Direction.All.Select(direction => new FieldJump(_fieldsGraph, from, direction));
        }

        public IJump FromTo(Field from, Field to)
        {
            return new FieldJump(_fieldsGraph, from, from.GetDirectionTo(to));
        }

        public static IEnumerable<Tuple<int, int>> Directions()
        {
            return Direction.All;
        }
        
    }
}