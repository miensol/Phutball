using System.Windows;

namespace Phutball.Shell.Models
{
    public class FieldModel : ModelWithId
    {
        private Visibility _hasStone = Visibility.Collapsed;
        public Visibility HasStone
        {
            get { return _hasStone; }
            set { _hasStone = value; 
                NotifyOfPropertyChange(()=> HasStone);
            }
        }

        private IStoneModel _stone;
        public IStoneModel Stone
        {
            get { return _stone; }
            set { _stone = value;
                NotifyOfPropertyChange(()=> Stone);
            }
        }

        private Visibility _selected;
        public Visibility Selected
        {
            get { return _selected; }
            set { _selected = value; 
                NotifyOfPropertyChange(()=> Selected);
            }
        }
        
        private LinesModel _lines;
        public LinesModel Lines
        {
            get { return _lines; }
            set { _lines = value; 
                NotifyOfPropertyChange(()=> Lines);
            }
        }
    }

    public class LinesModel
    {
        public Visibility Up { get; set; }
        public Visibility Down { get; set; }
        public Visibility Left { get; set; }
        public Visibility Right { get; set; }
    }
}