namespace Phutball
{
    public class BlackStone : IStone
    {
        public bool CanSelect
        {
            get { return false; }
        }
    }
}