using System.Windows;

namespace EndGames.Shell.Extensions
{
    public static class WpfExtensions
    {
        public static TResourceType FindResource<TResourceType>(this FrameworkElement me, string name) where TResourceType : class
        {
            return (TResourceType) me.FindResource(name);
        }
    }
}