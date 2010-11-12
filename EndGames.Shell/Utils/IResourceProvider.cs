using System.Windows;

namespace EndGames.Shell.Utils
{
    public interface IResourceProvider
    {
        DataTemplate FindDataTemplate(string key);
    }
}