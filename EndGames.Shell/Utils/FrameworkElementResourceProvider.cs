using System.Windows;
using Phutball.Shell.Extensions;

namespace Phutball.Shell.Utils
{
    public class FrameworkElementResourceProvider : IResourceProvider
    {
        private readonly FrameworkElement _container;

        public FrameworkElementResourceProvider(FrameworkElement container)
        {
            _container = container;
        }

        public DataTemplate FindDataTemplate(string key)
        {
            return _container.FindResource<DataTemplate>(key);
        }
    }
}