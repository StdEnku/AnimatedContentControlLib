using Demo.Views;
using Prism.Ioc;
using System.Windows;
using Config = Demo.Properties.Settings;

namespace Demo
{
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Control1>(Config.Default.Control1ViewRegionName);
            containerRegistry.RegisterForNavigation<Control2>(Config.Default.Control2ViewRegionName);
            containerRegistry.RegisterForNavigation<Control3>(Config.Default.Control3ViewRegionName);
            containerRegistry.RegisterForNavigation<Control4>(Config.Default.Control4ViewRegionName);
        }
    }
}
