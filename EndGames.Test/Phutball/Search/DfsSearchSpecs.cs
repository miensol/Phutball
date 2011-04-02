using System.Collections.Generic;
using System.Linq;
using EndGames.Phutball.Search;
using ForTesting;
using NUnit.Framework;

namespace EndGames.Tests.Phutball.Search
{
    public class when_running_dfs : observations_for_sut_of_type<DfsSearch<int>>, ISearchNodeVisitor<int>
    {
        private TestTree<int> _testTree;
        private List<int> _nested;
        private List<int> _unnested;
        private readonly int STOP_ON_THIS_NODE = (4);
        private readonly int DONT_GO_TO_CHILDREN = (3);

        private TestTree<int> Tree(int testNode)
        {
            return new TestTree<int>(testNode);
        }

        private TestTree<int> Tree(int i, IEnumerable<TestTree<int>> children)
        {
            return new TestTree<int>(i, children);
        }

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
            _testTree = Tree(1, 
                new[]
                    {
                        Tree(2),
                        Tree(DONT_GO_TO_CHILDREN, 
                            new[]
                            {
                                Tree(991)
                            }),
                        Tree(STOP_ON_THIS_NODE), 
                        Tree(5)
                    });
            _unnested = new List<int>();
            _nested = new List<int>();
        }

        [Test]
        public void should_nest_elemenst_in_proper_order()
        {
            _nested.ShouldHaveTheSameElementsAs(new[] {1, 2, DONT_GO_TO_CHILDREN, STOP_ON_THIS_NODE}.Select(i => i));
        }

        [Test]
        public void should_unnest_elements_in_proper_order()
        {
            _unnested.ShouldHaveTheSameElementsAs(new[]{2,DONT_GO_TO_CHILDREN,STOP_ON_THIS_NODE, 1}.Select(i => i));
        }

        public void OnEnter(ITree<int> node, ITreeSearchContinuation treeSearchContinuation)
        {
            if(ShouldStop(node.Node))
            {
                treeSearchContinuation.Stop();
            }
            if (ShouldNotEnterChildren(node.Node))
            {
                treeSearchContinuation.DontEnterChildren();
            }
            _nested.Add(node.Node);
        }

        public void OnLeave(ITree<int> node, ITreeSearchContinuation treeSearchContinuation)
        {            
            _unnested.Add(node.Node);
        }

        public bool ShouldStop(int node)
        {
            return node.Equals(STOP_ON_THIS_NODE);
        }

        public bool ShouldNotEnterChildren(int node)
        {
            return node.Equals(DONT_GO_TO_CHILDREN);
        }
    }
}