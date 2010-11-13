using System;
using System.Collections.Generic;
using EndGames.Phutball.Search;
using ForTesting;
using NUnit.Framework;

namespace EndGames.Tests.Phutball.Search
{
    public class TestTree<TNode> : ITree<TNode>
    {
        public TestTree(TNode node, IEnumerable<TestTree<TNode>> children)
        {
            Node = node;
            Children = children;
        }

        public TestTree(TNode node)
            : this(node, new List<TestTree<TNode>>())
        {
        }

        public TNode Node { get; private set; }

        public IEnumerable<ITree<TNode>> Children { get; private set; }
    }

    public class when_running_dfs : observations_for_sut_of_type<DfsSearch<int>>, IDfsSearchStartegy<int>
    {
        private TestTree<int> _testTree;
        private List<int> _nested;
        private List<int> _unnested;
        private const int STOP_ON_THIS_NODE = 4;
        private const int DONT_GO_TO_CHILDREN = 3;

        protected override void Because()
        {
            Sut.Run(_testTree);
        }

        protected override DfsSearch<int> CreateSut()
        {
            return new DfsSearch<int>(this);
        }

        protected override void EstablishContext()
        {
            _testTree = new TestTree<int>(1, 
                new[]
                    {
                        new TestTree<int>(2), 
                        new TestTree<int>(DONT_GO_TO_CHILDREN, 
                            new[]
                                {
                                    new TestTree<int>(991)
                                }),
                        new TestTree<int>(STOP_ON_THIS_NODE), 
                        new TestTree<int>(5) 
                    });
            _unnested = new List<int>();
            _nested = new List<int>();
        }

        [Test]
        public void should_nest_elemenst_in_proper_order()
        {
            _nested.ShouldHaveTheSameElementsAs(1,2,DONT_GO_TO_CHILDREN, STOP_ON_THIS_NODE);
        }

        [Test]
        public void should_unnest_elements_in_proper_order()
        {
            _unnested.ShouldHaveTheSameElementsAs(2,DONT_GO_TO_CHILDREN,STOP_ON_THIS_NODE, 1);
        }

        public void OnEnter(int node, IDfsContinuation dfsContinuation)
        {
            if(ShouldStop(node))
            {
                dfsContinuation.Stop();
            }
            if (ShouldNotEnterChildren(node))
            {
                dfsContinuation.DontEnterChildren();
            }
            _nested.Add(node);
        }

        public void OnLeave(int node, IDfsContinuation dfsContinuation)
        {            
            _unnested.Add(node);
        }

        public bool ShouldStop(int node)
        {
            return node == STOP_ON_THIS_NODE;
        }

        public bool ShouldNotEnterChildren(int node)
        {
            return node == DONT_GO_TO_CHILDREN;
        }
    }
}