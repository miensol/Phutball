namespace EndGames.Phutball.Moves
{
    public interface IPhutballMove : IMove<PhutballMoveContext>
    {
        bool CollectToPlayerSwitch(CompositeMove resultMove);
    }
}