namespace Phutball.Search
{
    public class OneTimeValueThenDefault<TWHat>
    {
        private TWHat _nextValue;
        private readonly TWHat _default;

        public OneTimeValueThenDefault(TWHat valueToRetunOnlyOneTime, TWHat @default)
        {
            _nextValue = valueToRetunOnlyOneTime;
            _default = @default;
        }

        public TWHat GetValue()
        {
            var result = _nextValue;
            _nextValue = _default;
            return result;
        }
    }
}