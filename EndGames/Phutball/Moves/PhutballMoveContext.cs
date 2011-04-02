namespace EndGames.Phutball.Moves
{
    public class PhutballMoveContext
    {
        public IPlayersState SwitchPlayer { get; set; }
        public IFieldsUpdater FieldsUpdater { get; set; }
    }
}