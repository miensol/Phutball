using System.Windows;

namespace Phutball.Shell.Extensions
{
    public static class WpfExtensions
    {
        public static TResourceType FindResource<TResourceType>(this FrameworkElement me, string name) where TResourceType : class
        {
            return (TResourceType) me.FindResource(name);
        }
    }
}