using System;
using EndGames.Phutball;

namespace EndGames.Tests.Phutball.Search
{
    public static class TestGraphs
    {
        private const FieldType Empty = FieldType.Empty;
        private const FieldType Black = FieldType.Black;
        private const FieldType White = FieldType.White;
        

        public static TestFieldsGraph BlackStonToJumpToWinningBorder()
        {
            return new TestFieldsGraph(
                new[]
                    {
                        new[]{ Empty, Empty, Empty, Empty, Empty },
                        new[]{ Empty, Empty, Empty, Empty, Empty },
                        new[]{ Empty, Empty, Black, Empty, Empty },
                        new[]{ Empty, Empty, White, Empty, Empty },
                        new[]{ Empty, Empty, Empty, Empty, Empty },
                        new[]{ Empty, Empty, Empty, Empty, Empty },
                        new[]{ Empty, Empty, Empty, Empty, Empty },
                    }
                );
        }

        public static TestFieldsGraph BlackStoneToJumpToLoosingBorder()
        {
            return new TestFieldsGraph(
                new[]
                    {
                        new[]{ Empty, Empty, Empty, Empty, Empty },
                        new[]{ Empty, Empty, Empty, Empty, Empty },
                        new[]{ Empty, Empty, Empty, Empty, Empty },
                        new[]{ Empty, Empty, White, Empty, Empty },
                        new[]{ Empty, Empty, Black, Empty, Empty },
                        new[]{ Empty, Empty, Empty, Empty, Empty },
                        new[]{ Empty, Empty, Empty, Empty, Empty },
                    }
                );
        }

        public static TestFieldsGraph OneBackwardJumpToFindWinningPath()
        {
            return new TestFieldsGraph(
                new[]
                    {
                        new[]{ Empty, Empty, Empty, Empty, Empty },
                        new[]{ Empty, Empty, Empty, Empty, Empty },
                        new[]{ Empty, Black, Empty, Empty, Empty },
                        new[]{ Empty, Black, Empty, White, Empty },
                        new[]{ Empty, Black, Black, Empty, Empty },
                        new[]{ Empty, Empty, Empty, Empty, Empty },
                        new[]{ Empty, Empty, Empty, Empty, Empty },
                        new[]{ Empty, Empty, Empty, Empty, Empty },
                        new[]{ Empty, Empty, Empty, Empty, Empty },
                    }
                );
        } 
        
        
        public static TestFieldsGraph TwoBackWardJumpsToImprovePosition()
        {
            return new TestFieldsGraph(
                new[]
                    {
                        new[]{ Empty, Empty, Empty, Empty, Empty, Empty },
                        new[]{ Empty, Empty, Empty, Empty, Empty, Empty },
                        new[]{ Empty, Empty, Empty, Empty, Empty, Empty },
                        new[]{ Empty, Black, Empty, White, Empty, Empty },
                        new[]{ Empty, Black, Empty, Black, Empty, Empty },
                        new[]{ Empty, Black, Empty, Empty, Empty, Empty },
                        new[]{ Empty, Black, Black, Empty, Empty, Empty },
                        new[]{ Empty, Empty, Empty, Empty, Empty, Empty },
                        new[]{ Empty, Empty, Empty, Empty, Empty, Empty },
                        new[]{ Empty, Empty, Empty, Empty, Empty, Empty },
                        new[]{ Empty, Empty, Empty, Empty, Empty, Empty },
                    }
                );
        } 
        
        public static TestFieldsGraph TwoWaysToJumpBackwardsOneWillLose()
        {
            return new TestFieldsGraph(
                new[]
                    {
                        new[]{ Empty, Empty, Empty, Empty, Empty, Empty },
                        new[]{ Empty, Empty, Empty, Empty, Empty, Empty },
                        new[]{ Empty, Empty, Empty, Empty, Empty, Empty },
                        new[]{ Empty, Black, Empty, White, Empty, Empty },
                        new[]{ Empty, Black, Empty, Black, Empty, Empty },
                        new[]{ Empty, Black, Empty, Empty, Empty, Empty },
                        new[]{ Empty, Black, Black, Black, Empty, Empty },
                        new[]{ Empty, Empty, Empty, Black, Empty, Empty },
                        new[]{ Empty, Empty, Empty, Black, Empty, Empty },
                        new[]{ Empty, Empty, Empty, Empty, Empty, Empty },
                        new[]{ Empty, Empty, Empty, Empty, Empty, Empty },
                    }
                );
        } 
        
        public static TestFieldsGraph TwoWaysToJumpBackwardsOneWins()
        {
            return new TestFieldsGraph(
                new[]
                    {
                        new[]{ Empty, Empty, Empty, Empty, Empty, Empty, Empty },
                        new[]{ Empty, Empty, Empty, Empty, Empty, Black, Empty },
                        new[]{ Empty, Empty, Empty, Empty, Empty, Black, Empty },
                        new[]{ Empty, Black, Empty, White, Empty, Black, Empty },
                        new[]{ Empty, Black, Empty, Black, Empty, Black, Empty },
                        new[]{ Empty, Black, Empty, Empty, Black, Empty, Empty },
                        new[]{ Empty, Black, Black, Empty, Empty, Empty, Empty },
                        new[]{ Empty, Empty, Empty, Black, Empty, Empty, Empty },
                        new[]{ Empty, Empty, Empty, Black, Empty, Empty, Empty },
                        new[]{ Empty, Empty, Empty, Empty, Empty, Empty, Empty },
                        new[]{ Empty, Empty, Empty, Empty, Empty, Empty, Empty },
                    }
                );
        }
        
        public static TestFieldsGraph WinningWayPassingThoughLossingField()
        {
            return new TestFieldsGraph(
                new[]
                    {
                        new[]{ Empty, Empty, Empty, Empty, Empty, Empty },
                        new[]{ Empty, Empty, Empty, Empty, Empty, Black },
                        new[]{ Empty, Black, Empty, Empty, Empty, Black },
                        new[]{ Empty, Black, Empty, White, Empty, Black },
                        new[]{ Empty, Black, Empty, Black, Empty, Black },
                        new[]{ Empty, Black, Empty, Black, Black, Empty },
                        new[]{ Empty, Black, Empty, Empty, Empty, Empty },
                        new[]{ Empty, Empty, Empty, Black, Empty, Empty },
                        new[]{ Empty, Black, Empty, Black, Empty, Empty },
                        new[]{ Empty, Empty, Black, Empty, Empty, Empty },
                        new[]{ Empty, Empty, Empty, Empty, Empty, Empty },
                    }
                );
        } 
        
        public static TestFieldsGraph ContinueSearchAfterImprovingPosition()
        {
            return new TestFieldsGraph(
                new[]
                    {
                        new[]{ Empty, Empty, Empty, Empty, Empty, Empty, Empty },
                        new[]{ Empty, Empty, Empty, Empty, Empty, Black, Empty },
                        new[]{ Empty, Empty, Black, Empty, Empty, Empty, Empty },
                        new[]{ Empty, Black, Empty, Empty, Black, Black, Empty },
                        new[]{ Empty, Black, Empty, Empty, Empty, Empty, Empty },
                        new[]{ Empty, Empty, Empty, Empty, Empty, Empty, Empty },
                        new[]{ Empty, Empty, Black, Empty, Empty, Empty, Empty },
                        new[]{ Empty, Empty, Empty, Black, Empty, Empty, Empty },
                        new[]{ Empty, Empty, Empty, Empty, White, Empty, Empty },
                        new[]{ Empty, Empty, Empty, Empty, Empty, Empty, Empty },
                        new[]{ Empty, Empty, Empty, Empty, Empty, Empty, Empty },
                    }
                );
        }

        public static IFieldsGraph Random(int rowCount, int columnCount, double blackProbability)
        {
            return new RandomGraphBuilder(rowCount, columnCount, blackProbability).Build();
        }
    }
}