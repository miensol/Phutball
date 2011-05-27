using System;

namespace Phutball.Search
{
    [AttributeUsage(AttributeTargets.Method)]
    public class StrategyDesctiptionAttribute : Attribute
    {
        public StrategyDesctiptionAttribute(string tooltip)
        {
            Tooltip = tooltip;
        }

        public string Tooltip { get; set; }
    }
}