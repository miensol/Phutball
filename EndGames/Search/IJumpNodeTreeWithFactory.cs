using System;
using System.Collections.Generic;

namespace Phutball.Search
{
    public interface IJumpNodeTreeWithFactory : IJumpNodeTree
    {
        Func<IJumpNodeTreeWithFactory, IEnumerable<IJumpNodeTreeWithFactory>> ChildFactory { get; }
    }
}