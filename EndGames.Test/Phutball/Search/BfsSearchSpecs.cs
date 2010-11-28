using System;
using System.Collections.Generic;
using System.Linq;
using EndGames.Phutball.Search;
using ForTesting;
using NUnit.Framework;

namespace EndGames.Tests.Phutball.Search
{
    public abstract class observations_for_searching_with_bfs : observations_for_sut_of_type<BfsSearch<int>>, ISearchNodeVisitor<int>
    {
        protected TestTree<int> _tree;
        protected List<int> _visted;
        protected List<int> _leaved;

        protected override void Because()
        {
            Sut.Run(_tree);
        }

        protected override void EstablishContext()
        {
            _leaved = new List<int>();
            _visted = new List<int>();
        }

        protected override BfsSearch<int> CreateSut()
        {
            return new BfsSearch<int>(this);
        }

        protected TestTree<int> Tree(int i)
        {
            return new TestTree<int>(i);
        }

        protected TestTree<int> Tree(int i, IEnumerable<TestTree<int>> children)
        {
            return new TestTree<int>(i, children);
        }

        public virtual void OnEnter(int node, ITreeSearchContinuation treeSearchContinuation)
        {
            _visted.Add(node);
        }

        public virtual void OnLeave(int node, ITreeSearchContinuation treeSearchContinuation)
        {
            _leaved.Add(node);
        }
    }

    public class when_searching_with_bfs_with_stops : observations_for_searching_with_bfs
    {
        private const int DONT_ENTER_CHILDREN = 2;
        private const int STOP_ON_THIS = 4;

        protected override void EstablishContext()
        {
            base.EstablishContext();
            _tree = Tree(1,
                         new[]
                             {
                                 Tree(DONT_ENTER_CHILDREN, new[]
                                             {
                                                 Tree(6)
                                             }),
                                 Tree(3, new[]
                                             {
                                                 Tree(STOP_ON_THIS, new[]{Tree(5)})
                                             })
                             });
        }

        [Test]
        public void should_enter_nodes_in_correct_order()
        {
            _visted.ShouldHaveTheSameElementsAs(new[]{1,2,3,4});
        }

        [Test]
        public void should_leave_nodes_in_correct_oreder()
        {
            _leaved.ShouldHaveTheSameElementsAs(new[]{2,4,3,1});
        }

        public override void OnEnter(int node, ITreeSearchContinuation treeSearchContinuation)
        {
            base.OnEnter(node, treeSearchContinuation);
            if(node == STOP_ON_THIS)
            {
                treeSearchContinuation.Stop();
            }
            if(node == DONT_ENTER_CHILDREN)
            {
                treeSearchContinuation.DontEnterChildren();
            }
        }
    }

    public class when_searching_with_bfs : observations_for_searching_with_bfs
    {
        protected override void EstablishContext()
        {
            base.EstablishContext();
            _tree = new TestTree<int>(1,
                new[]{ 
                    Tree(2, 
                        new[]
                            {
                                Tree(3),Tree(4),
                            }), 
                        Tree(5)  });
        }

        [Test]
        public void should_enter_nodes_in_correct_sequence()
        {
            _visted.ShouldHaveTheSameElementsAs(new[] {1, 2, 5, 2, 3, 4});
        }

        [Test]
        public void should_leave_nodes_in_correct_sequence()
        {
            _leaved.ShouldHaveTheSameElementsAs(new[]{2,5,3,4,2,1});
        }
    }

    public class when_searching_with_bfs2 : observations_for_searching_with_bfs
    {
        protected override void EstablishContext()
        {
            base.EstablishContext();
            _tree = Tree(1,
                         new[] {Tree(2,
                                new[]{Tree(3, 
                                    new[]{Tree(4), Tree(6)})}),                              
                             Tree(5)});
        }

        [Test]
        public void should_enter_nodes_in_correct_order()
        {
            _visted.ShouldHaveTheSameElementsAs(new[]{1,2,5,2,3,4,6});
        }

        [Test]
        public void should_leave_nodes_in_correct_order()
        {
            _leaved.ShouldHaveTheSameElementsAs(new[] {2,5,4,6,3,2,1});
        }

    }
}