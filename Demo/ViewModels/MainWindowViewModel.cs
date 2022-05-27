using AnimatedContentControlLib.Core.Constants;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace Demo.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private IRegionManager _regionManager;

        public MainWindowViewModel(IRegionManager regionManager)
        {
            this._regionManager = regionManager;
        }
    }
}