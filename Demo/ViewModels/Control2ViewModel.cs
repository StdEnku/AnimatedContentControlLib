using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Messangers = AnimatedContentControlLib.Core.Messengers;
using Constants = AnimatedContentControlLib.Core.Constants;
using Config = Demo.Properties.Settings;

namespace Demo.ViewModels;

internal class Control2ViewModel : ViewModelBase
{
    public Control2ViewModel(IRegionManager regionManager) : base(regionManager) { }

    public override void OnNavigatedFrom(NavigationContext navigationContext)
    {
        var toViewName = navigationContext.Uri.ToString();
        var nextAnimName = toViewName == Config.Default.Control1ViewRegionName ? Constants.EmbededAnimations.SlideinRight :
                           toViewName == Config.Default.Control3ViewRegionName ? Constants.EmbededAnimations.SlideinLeft :
                           null;

        Messangers.AnimationNameMessanger.SetAnimationName("PrimaryContent.AnimationMessageKey", nextAnimName);
    }
}
