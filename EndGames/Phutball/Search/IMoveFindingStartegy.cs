using EndGames.Phutball.Moves;

namespace EndGames.Phutball.Search
{
    public interface IMoveFindingStartegy
    {
        IMove<IFieldsGraph> Search(IFieldsGraph fieldsGraph);
    }
}