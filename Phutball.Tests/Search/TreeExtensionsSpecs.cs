using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using ForTesting;
using Phutball.Search;
using Phutball.Search.Visitors;

namespace Phutball.Tests.Search
{
    public class observations_for_tree
    {
        protected TestTree<int> _tree;

        protected TestTree<int> Tree(int node, params TestTree<int>[] children)
        {
            return new TestTree<int>(node, children);
        }

        [SetUp]
        public void BuildContext()
        {
            _tree = null;
        }
    }

    public class when_flattening_tree : observations_for_tree
    {
        [Test]
        public void should_return_all_nodes_as_list()
        {
            _tree = Tree(1, Tree(2), Tree(3, Tree(4)));
            _tree.Flatten().Select(t=> t.Node).ShouldHaveTheSameElementsAs(1,2,3,4);
        }

        [Test]
        public void should_evaluate_nodes_lazyly()
        {
            _tree = Tree(1, null);
            var node = _tree.Flatten().First();
            Catch.Exception(() => _tree.Flatten().Last()).ShouldBeOfType<NullReferenceException>();
        }
    }

    public class when_getting_path_from_root : observations_for_tree
    {
        private TestTree<int> _tree;

        [SetUp]
        public void BuildContext()
        {
            _tree = Tree(1, Tree(2), Tree(3, Tree(4)));
        }

        [Test]
        public void should_return_all_nodes_from_root_for_leafs()
        {
            _tree.Children.First().PathFromRoot().Select(t=> t.Node).ShouldHaveTheSameElementsAs(1,2);
        }

        [Test]
        public void should_return_all_nodes_from_root_to_deep_leaf()
        {
            _tree.Children.Last().Children.First().PathFromRoot().Select(t=> t.Node).ShouldHaveTheSameElementsAs(1,3,4);
        }

        [Test]
        public void should_return_root_not_for_root_node()
        {
            _tree.PathFromRoot().Single().Node.ShouldEqual(1);
        }
    }

    public class when_traversing_with_dfs : observations_for_tree
    {
        private IEnumerable<int> _traversed;

        [Test]
        public void should_return_nodes_in_correct_order()
        {
            _tree = Tree(1, Tree(2), Tree(3, Tree(4), Tree(5)));
            _traversed = _tree.TraverseWithDfs().Select(n => n.Node);            
            _traversed.ShouldHaveTheSameElementsAs(1,2,3,4,5);
        }

        [Test]
        public void should_evaluate_tree_lazyly()
        {
            _tree = Tree(1, null);
            var traverLazyly = _tree.TraverseWithDfs();
            Catch.Exception(() => traverLazyly.ToList()).ShouldBeOfType<NullReferenceException>();
        }

        [Test]
        public void should_be_able_to_skip_nodes()
        {
            _tree = Tree(1, Tree(2));
            _tree.TraverseWithDfs().Skip(1).ShouldHaveCount(1);
        }

    }


    public class when_traversing_dfs : observations_for_tree, ISearchNodeVisitor<int>
    {
        private List<int> _entered;
        private List<int> _leaved;

        [SetUp]
        public void BuildContext()
        {
            _entered = new List<int>();
            _leaved = new List<int>();
        }

        [Test]
        public void should_return_nodes_in_correct_order()
        {
            _tree = Tree(1, Tree(2, Tree(3), Tree(4)), Tree(5));
            _tree.TraverseDfs(new EmptyNodeVisitor<int>()).Select(t=> t.Node).ShouldHaveTheSameElementsAs(1,2,3,4,5);
        }

        [Test]
        public void should_enter_nodes_in_correct_orderd()
        {
            _tree = Tree(1, Tree(2, Tree(3), Tree(4)), Tree(5));
            _tree.TraverseDfs(this).ToList();
            _entered.ShouldHaveTheSameElementsAs(1,2,3,4,5);
        }
        
        [Test]
        public void should_leave_nodes_in_correct_orderd()
        {
            _tree = Tree(1, Tree(2, Tree(3), Tree(4)), Tree(5));
            _tree.TraverseDfs(this).ToList();
            _leaved.ShouldHaveTheSameElementsAs(3,4,2,5,1);
        }

        [Test]
        public void should_traverse_properly_even_when_skipping()
        {
            _tree = Tree(1, 
                            Tree(2), 
                            Tree(3, 
                                Tree(4, 
                                    Tree(5)), 
                                Tree(6)));
            _tree.TraverseDfs(this).Skip(1).ToList();
            _entered.ShouldHaveTheSameElementsAs(1,2,3,4,5,6);
            _leaved.ShouldHaveTheSameElementsAs(2,5,4,6,3,1);
        }

        [Test]
        public void should_evalute_nodes_lazyly()
        {
            _tree = Tree(1, Tree(2, null, null));
            _tree.TraverseDfs(this).Take(2).ToList();
            _entered.ShouldHaveTheSameElementsAs(1,2); 
        }

        [Test]
        public void should_enter_nodes_up_to_max_depth()
        {
            _tree = Tree(1, Tree(2, Tree(3)), Tree(4));
            _tree.TraverseDfs(this, 2).ToList();
            _entered.ShouldHaveTheSameElementsAs(1,2,4);

            foreach (var node in _tree.TraverseDfs(new EmptyNodeVisitor<int>()))
            {
                // logika dotycz¹ca wêz³a
            }
        }

        [Test]
        public void should_leave_nodes_up_to_max_depth()
        {
            _tree = Tree(1, Tree(2, Tree(3)), Tree(4));
            _tree.TraverseDfs(this, 2).ToList();
            _leaved.ShouldHaveTheSameElementsAs(2,4,1);
        }


        public void OnEnter(ITree<int> node, ITreeSearchContinuation treeSearchContinuation)
        {
            _entered.Add(node.Node);
        }

        public void OnLeave(ITree<int> node, ITreeSearchContinuation treeSearchContinuation)
        {
            _leaved.Add(node.Node);
        }
    }
}