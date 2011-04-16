using System;
using System.Collections.Generic;
using EndGames.Phutball.PlayerMoves;

namespace EndGames.Phutball.Search
{
    public interface IJumpNodeTreeWithFactory : IJumpNodeTree
    {
        Func<IJumpNodeTreeWithFactory, IEnumerable<IJumpNodeTreeWithFactory>> ChildFactory { get; }
    }
}