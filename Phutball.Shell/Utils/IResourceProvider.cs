using System.Windows;

namespace Phutball.Shell.Utils
{
    public interface IResourceProvider
    {
        DataTemplate FindDataTemplate(string key);
    }
}