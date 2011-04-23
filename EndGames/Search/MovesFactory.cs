namespace Phutball.Search
{
    public class MovesFactory
    {
        public RootedBySelectingWhiteFieldBoardJumpTree GetMovesTree(IFieldsGraph graphCopy)
        {
            return new RootedBySelectingWhiteFieldBoardJumpTree(graphCopy);
        }
    }
}