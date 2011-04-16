using System.Linq;
using EndGames.Phutball;
using EndGames.Phutball.PlayerMoves;
using EndGames.Phutball.Search;
using EndGames.Tests.Phutball.Search;
using NUnit.Framework;
using ForTesting;

namespace EndGames.Tests.Phutball
{
    public class when_traversing_phutball_moves 
    {
        private IFieldsGraph _fieldsGraph;
        private const FieldType Empty = FieldType.Empty;
        private const FieldType Black = FieldType.Black;
        private const FieldType White = FieldType.White;
        

        [Test]
        public void should_generate_moves_properly()
        {
            _fieldsGraph = new TestFieldsGraph(
                new[]
                    {
                        new[] {Empty, Empty, Empty, Empty, Empty},
                        new[] {Empty, Empty, Empty, Empty, Empty},
                        new[] {Empty, Empty, Black, Empty, Empty},
                        new[] {Empty, Empty, Black, Empty, Empty},
                        new[] {Empty, Empty, Empty, Empty, Empty},
                        new[] {Empty, Empty, Black, Empty, Empty},
                        new[] {Empty, Empty, White, Empty, Empty},
                        new[] {Empty, Empty, Empty, Empty, Empty},
                        new[] {Empty, Empty, Empty, Empty, Empty},
                        new[] {Empty, Empty, Empty, Empty, Empty},
                    }
                ).Build();

            var current = new RootedBySelectingWhiteFieldBoardJumpTree(_fieldsGraph);
            var currentMoves =
                current.TraverseDfs(
                    new PerformMovesNodeVisitor(PerformMoves.DontCareAboutPlayerStateChange(_fieldsGraph)))
                    .Skip(1).ToList();

            currentMoves.ShouldHaveCount(2);

        }
    }
}