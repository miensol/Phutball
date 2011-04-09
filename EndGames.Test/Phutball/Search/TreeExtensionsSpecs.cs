using System;
using System.Collections.Generic;
using System.Linq;
using EndGames.Phutball.Search;
using NUnit.Framework;
using ForTesting;

namespace EndGames.Tests.Phutball.Search
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

    }
}