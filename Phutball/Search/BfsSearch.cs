﻿using System.Collections.Generic;
using System.Linq;

namespace Phutball.Search
{
    public class BfsSearch<TNode> : ITreeSearch<TNode>, ITreeSearchContinuation
    {
        private readonly IAddRemoveCollection<ITree<TNode>> _toVisit;
        private readonly ISearchNodeVisitor<TNode> _nodeVisitor;
        private bool _stopSearch;
        private bool _dontEnterChildren;
        private ITree<TNode> _lastEntered;

        public BfsSearch(ISearchNodeVisitor<TNode> nodeVisitor):this(nodeVisitor,  new QueueAddRemoveCollection<ITree<TNode>>())
        {
        }

        public BfsSearch(ISearchNodeVisitor<TNode> nodeVisitor, IAddRemoveCollection<ITree<TNode>> toVisit)
        {
            _nodeVisitor = nodeVisitor;
            _toVisit = toVisit;
        }

        public void Run<TTree>(TTree tree) where TTree : ITree<TNode>
        {
            //_toVisit.Enqueue(tree);
            _toVisit.Put(tree);
            while(_toVisit.Any() && _stopSearch == false)
            {
                //var current = _toVisit.Dequeue();
                var current = _toVisit.Pull();
                var node = current.Node;
                Enter(current);
                FollowChildrenIfNeeded(current);
            }
            while(_lastEntered != default(ITree<TNode>))
            {
                _nodeVisitor.OnLeave(_lastEntered, this);
                _lastEntered = _lastEntered.Parent;
            }
        }

        private void FollowChildrenIfNeeded(ITree<TNode> current)
        {
            if(_dontEnterChildren == false && _stopSearch == false)
            {
                //current.Children.Each(node => _toVisit.Enqueue(node));                    
                current.Children.Each(node => _toVisit.Put(node));                    
            } else
            {
                _dontEnterChildren = false;
            }
        }

        private void Enter(ITree<TNode> current)
        {
            if (IsChildOfLastEnteredNode(current))
            {
                _nodeVisitor.OnEnter(current, this);
            }
            else
            {
                TraverseTreeTo(current);
            }
            _lastEntered = current;    
        }

        private bool IsChildOfLastEnteredNode(ITree<TNode> current)
        {
            // TODO: this is hacky, _lastEntered may by decorator of BoardJumpTree, but may also be null
            return _lastEntered == current.Parent || (_lastEntered != null && _lastEntered.Equals(current.Parent));
        }

        private void TraverseTreeTo(ITree<TNode> current)
        {
            var toEnter = new Stack<ITree<TNode>>();
            var actual = current;
            var last = _lastEntered;
            while (actual != last)
            {
                if(actual != null)
                {
                    toEnter.Push(actual);
                    actual = actual.Parent;    
                }                
                if(actual != last)
                {
                    if(last != null)
                    {
                        _nodeVisitor.OnLeave(last, this);
                        last = last.Parent;    
                    }
                    
                }
            }
            while (toEnter.Any())
            {
                _nodeVisitor.OnEnter(toEnter.Pop(), this);
            }
        }

        public void Stop()
        {
            _stopSearch = true;
        }

        public void DontEnterChildren()
        {
            _dontEnterChildren = true;
        }

        public void DontEnterNeighbours()
        {
        }
    }
}