namespace Phutball.Search
{
    public interface IMoveFindingStartegy
    {
        PhutballMoveScore Search(IFieldsGraph fieldsGraph);
    }
}