using System;
using Phutball.Search;

namespace Phutball.Shell.Presenters
{
    public class MoveStrategyButtonModel
    {
        public Func<IMoveFinders, IMoveFindingStartegy> ChooseStrategy { get; set; }
        public string StrategyName { get; set; }
        public string ToolTip { get; set; }
        public bool ShowToolTip { get; set; }
    }
}