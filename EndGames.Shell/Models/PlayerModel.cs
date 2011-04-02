using Caliburn.PresentationFramework;

namespace EndGames.Shell.Models
{
    public class PlayerModel : PropertyChangedBase
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; 
                NotifyOfPropertyChange(()=> Name);
            }
        }

        private bool _isOnTheMove;
        public bool IsOnTheMove
        {
            get { return _isOnTheMove; }
            set { _isOnTheMove = value; 
                NotifyOfPropertyChange(()=> IsOnTheMove);
            }
        }
    }
}