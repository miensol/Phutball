namespace EndGames.Phutball
{
    public class BlackStone : IStone
    {
        #region IStone Members

        public bool CanSelect
        {
            get { return false; }
        }

        #endregion
    }
}