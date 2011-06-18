using System.Collections.Generic;
using Caliburn.PresentationFramework;
using Caliburn.PresentationFramework.Screens;
using Microsoft.Practices.ServiceLocation;

namespace Phutball.Shell.Extensions
{
    public static class CaliburnExtensions
    {
        public static void OpenScreen<TScreen>(this IScreenCollection<IScreen> screenCollection)
            where TScreen : IScreen
        {
            var presenter = ServiceLocator.Current.GetInstance<TScreen>();
            screenCollection.OpenScreen(presenter);
        }

        public static BindableCollection<TTarget> ToBindableCollection<TTarget>(this IEnumerable<TTarget> source)
        {
            return new BindableCollection<TTarget>(source);
        }
    }
}