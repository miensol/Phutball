using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Caliburn.PresentationFramework;

namespace EndGames.Shell.Models
{
    public class CheckersBoardModel : PropertyChangedBase
    {
        public CheckersBoardModel()
        {
           
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value;
                NotifyOfPropertyChange(()=> Title);
            }
        }

        private BindableCollection<FieldModel> _fields;

        public BindableCollection<FieldModel> Fields
        {
            get { return _fields; }
            set { _fields = value; NotifyOfPropertyChange(()=> Fields);}
        }
    }

    public class FieldModel : PropertyChangedBase
    {
        public int Id { get; set; }
    }

    public class WhiteFieldModel : FieldModel {}
    public class BlackFieldModel : FieldModel
    {
        public const string WhitePawnColor = "White";
        public const string BlackPawnColor = "Black";
        public bool HasPawn { get; set; }
        public string Color { get; set; }

        public Visibility PawnVisible { get{ return HasPawn ? Visibility.Visible : Visibility.Collapsed;}}
    }
}