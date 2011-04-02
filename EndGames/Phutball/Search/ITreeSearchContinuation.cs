namespace EndGames.Phutball.Search
{
    public interface ITreeSearchContinuation
    {
        void Stop();
        void DontEnterChildren();
        void DontEnterNeighbours();
    }
}