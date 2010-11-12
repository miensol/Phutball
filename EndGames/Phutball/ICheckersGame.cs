using System;
using System.Collections.Generic;

namespace EndGames.Games
{
    public interface ICheckersGame
    {
        void BuildInitialGraphGame();
        IEnumerable<IField> Fields { get; }
    }
}