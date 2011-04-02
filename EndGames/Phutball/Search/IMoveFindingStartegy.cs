using EndGames.Phutball.Moves;

namespace EndGames.Phutball.Search
{
    public interface IMoveFindingStartegy
    {
        IPhutballMove Search(IFieldsGraph fieldsGraph);
    }
}