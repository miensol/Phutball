using System.Collections.Generic;
using System.Linq;

namespace EndGames.Phutball.Search
{
    public static class TreeExtensions
    {
        public static IEnumerable<ITree<T>> Flatten<T>(this ITree<T> tree)
        {
            return new[] {tree}.Concat(tree.Children.SelectMany(Flatten));
        }

        public static IEnumerable<ITree<T>> PathFromRoot<T>(this ITree<T> tree)
        {
            var result = new List<ITree<T>>{tree};
            var current = tree.Parent;
            while(current != null)
            {
                result.Add(current);
                current = current.Parent;
            }
            result.Reverse();
            return result;
        }

        public static IEnumerable<ITree<T>> TraverseDfs<T>(this ITree<T> tree, ISearchNodeVisitor<T> visitor)
        {
            visitor.OnEnter(tree, null);
            yield return tree;
            foreach (var child in tree.Children.SelectMany(child=> child.TraverseDfs(visitor)))
            {
                yield return child;
            }
            visitor.OnLeave(tree, null);
        }



        public static IEnumerable<ITree<T>> TraverseWithDfs<T>(this ITree<T> tree)
        {
            var stack = new Stack<ITree<T>>(new[]{tree});
            do
            {
                var current = stack.Pop();
                yield return current;
                var children = current.Children.Reverse().ToList();
                foreach (var child in children)
                {
                    stack.Push(child);
                }
            } while (stack.Any());
        }
    }
}