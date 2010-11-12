using Microsoft.Practices.ServiceLocation;

namespace EndGames.Phutball
{
    public class PhutballOptions : IPhutballOptions
    {
        public int RowCount
        {
            get { return 19; }
        }

        public int ColumnCount
        {
            get { return 15; }
        }

        public static IPhutballOptions Current  
        {
            get { return ServiceLocator.Current.GetInstance<IPhutballOptions>(); }
        }

    }
}