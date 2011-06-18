namespace Phutball.Search.BoardValues
{
    public interface IValueOf<in TWHat>
    {
        int GetValue(TWHat valueSubject);
    }
}