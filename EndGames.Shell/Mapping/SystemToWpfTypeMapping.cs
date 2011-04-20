using System.Windows;
using AutoMapper;
using Phutball.Utils;

namespace Phutball.Shell.Mapping
{
    public class SystemToWpfTypeMapping : IStartupTask
    {
        public void Execute()
        {
            Mapper.CreateMap<bool,Visibility>()
                .ConvertUsing(visible=> visible ? Visibility.Visible : Visibility.Collapsed);
        }
    }
}