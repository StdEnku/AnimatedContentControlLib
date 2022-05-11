using Prism.Ioc;
using System.Windows;
using Views = Example.Views;

namespace Example
{
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<Views.MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<Views.Control1>(nameof(Views.Control1));
            containerRegistry.RegisterForNavigation<Views.Control2>(nameof(Views.Control2));
        }
    }
}
