using ForTesting;
using NUnit.Framework;
using Phutball.Search;
using Phutball.Search.BoardValues;
using Phutball.Search.Visitors;

namespace Phutball.Tests.Search
{
    public abstract class MiniMaxTree : osbservations_for_tree_search<AlphaBetaSearch<int>>
    {
        public static ITree<int> WikiTree()
        {
            return Tree(6,
                         new[]
                             {
                                 Tree(3, new[]
                                             {
                                                 Tree(5, new[]
                                                             {
                                                                 Tree(5, new[]
                                                                             {
                                                                                 Tree(5),
                                                                                 Tree(6)
                                                                             }),
                                                                 Tree(4, new[]
                                                                             {
                                                                                 Tree(7),Tree(4),Tree(5)
                                                                             })
                                                             }), 
                                                 Tree(3, new[]
                                                             {
                                                                 Tree(3, new []
                                                                             {
                                                                                 Tree(3)
                                                                             })
                                                             })
                                             }),
                                 Tree(6, new[]
                                             {
                                                 Tree(6, new[]
                                                             {
                                                                 Tree(6, new[]
                                                                             {
                                                                                 Tree(6)
                                                                             }),
                                                                 Tree(6,new[]
                                                                            {
                                                                                Tree(6),Tree(6)
                                                                            })
                                                             }),
                                                 Tree(7, new[]
                                                             {
                                                                 Tree(7, new[]
                                                                             {
                                                                                 Tree(7)
                                                                             })
                                                             })
                                             }), 
                                 Tree(5, new[]
                                             {
                                                 Tree(5, new[]
                                                             {
                                                                 Tree(5, new[]
                                                                             {
                                                                                 Tree(5)
                                                                             })
                                                             }), 
                                                 Tree(8,new[]
                                                            {
                                                                Tree(8,new[]
                                                                           {
                                                                               Tree(9),Tree(8)
                                                                           }),
                                                                Tree(6, new[]
                                                                            {
                                                                                Tree(6)
                                                                            })
                                                            })
                                             })
                             });
        } 
        
        public static ITree<int> WikiNoValuesTree()
        {
            return Tree(0,
                         new[]
                             {
                                 Tree(0, new[]
                                             {
                                                 Tree(0, new[]
                                                             {
                                                                 Tree(0, new[]
                                                                             {
                                                                                 Tree(5),
                                                                                 Tree(6)
                                                                             }),
                                                                 Tree(0, new[]
                                                                             {
                                                                                 Tree(7),Tree(4),Tree(5)
                                                                             })
                                                             }), 
                                                 Tree(0, new[]
                                                             {
                                                                 Tree(0, new []
                                                                             {
                                                                                 Tree(3)
                                                                             })
                                                             })
                                             }),
                                 Tree(0, new[]
                                             {
                                                 Tree(0, new[]
                                                             {
                                                                 Tree(0, new[]
                                                                             {
                                                                                 Tree(6)
                                                                             }),
                                                                 Tree(0,new[]
                                                                            {
                                                                                Tree(6),Tree(6)
                                                                            })
                                                             }),
                                                 Tree(0, new[]
                                                             {
                                                                 Tree(0, new[]
                                                                             {
                                                                                 Tree(7)
                                                                             })
                                                             })
                                             }), 
                                 Tree(0, new[]
                                             {
                                                 Tree(0, new[]
                                                             {
                                                                 Tree(0, new[]
                                                                             {
                                                                                 Tree(5)
                                                                             })
                                                             }), 
                                                 Tree(0,new[]
                                                            {
                                                                Tree(0,new[]
                                                                           {
                                                                               Tree(9),Tree(8)
                                                                           }),
                                                                Tree(0, new[]
                                                                            {
                                                                                Tree(6)
                                                                            })
                                                            })
                                             })
                             });
        }
    }


    public class when_searching_with_alpha_beta : osbservations_for_tree_search<AlphaBetaSearch<int>>
    {
        private ITree<int> _tree;
        private IntTreeValuer _intTreeValuer = new IntTreeValuer();

        protected override void Because()
        {
            Sut.Run(_tree);
        }

        [Test]
        public void should_enter_nodes_in_correct_order()
        {
            Sut.BestMove.Score.ShouldEqual(6);
        }

        protected override void EstablishContext()
        {
            _tree = MiniMaxTree.WikiNoValuesTree();
        }

        protected override AlphaBetaSearch<int> CreateSut()
        {
            return new AlphaBetaSearch<int>(_intTreeValuer, AlphaBetaOptions.Defaults(), new EmptyNodeVisitor<int>());
        }

    }
    public class IntTreeValuer : IValueOf<int>
    {
        public int GetValue(int valueSubject)
        {
            return valueSubject;
        }
    }
}