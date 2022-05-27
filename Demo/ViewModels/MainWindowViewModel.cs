using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Messangers = AnimatedContentControlLib.Core.Messengers;
using Constants = AnimatedContentControlLib.Core.Constants;

namespace Demo.ViewModels
{
    internal class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel(IRegionManager regionManager) : base(regionManager) { }
    }
}