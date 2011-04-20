using Caliburn.PresentationFramework;

namespace Phutball.Shell.Models
{
    public class ModelWithId : PropertyChangedBase
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; 
                NotifyOfPropertyChange(()=>Id);
            }
        }
    }
}