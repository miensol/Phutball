using Caliburn.PresentationFramework;
using EndGames.Shell.Models;

namespace EndGames.Shell.Mapping
{
    public class PlayerOnBoardModel : PropertyChangedBase
    {

        private PlayerModel _player;
        public PlayerModel Player
        {
            get { return _player; }
            set { _player = value; 
                NotifyOfPropertyChange(()=> Player);
            }
        }

        private string _timeOnMoves;

        public string TimeOnMoves
        {
            get { return _timeOnMoves; }
            set { _timeOnMoves = value; 
                NotifyOfPropertyChange(()=> TimeOnMoves);
            }
        }
    }
}