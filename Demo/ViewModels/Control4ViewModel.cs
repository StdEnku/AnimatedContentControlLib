using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Messangers = AnimatedContentControlLib.Core.Messengers;
using Constants = AnimatedContentControlLib.Core.Constants;
using Config = Demo.Properties.Settings;

namespace Demo.ViewModels;

internal class Control4ViewModel : ViewModelBase
{
    public Control4ViewModel(IRegionManager regionManager) : base(regionManager) { }

    public override void OnNavigatedFrom(NavigationContext navigationContext)
    {
        Messangers.AnimationNameMessanger.SetAnimationName(
            "PrimaryContent.AnimationMessageKey", 
            Constants.EmbededAnimations.SlideinRight);
    }
}
