using EndGames.Phutball.Search;
using EndGames.Phutball.Search.BoardValues;
using ForTesting;
using NUnit.Framework;

namespace EndGames.Tests.Phutball.Search
{
    public abstract class AndOrTrees : osbservations_for_tree_search<AndOrSearch<int>>
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


    public class when_searching_with_and_or : osbservations_for_tree_search<AndOrSearch<int>>
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
            _tree = AndOrTrees.WikiNoValuesTree();
        }

        protected override AndOrSearch<int> CreateSut()
        {
            return new AndOrSearch<int>(_intTreeValuer, 5);
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