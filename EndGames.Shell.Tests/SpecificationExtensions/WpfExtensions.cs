using System.Windows;
using ForTesting;

namespace EndGames.Shell.Tests.SpecificationExtensions
{
    public static class WpfExtensions
    {
        public static void ShouldNotBeVisible(this Visibility me)
        {
            me.ShouldEqual(Visibility.Collapsed);
        }
        public static void ShouldBeVisible(this Visibility me)
        {
            me.ShouldEqual(Visibility.Visible);
        }
    }
}