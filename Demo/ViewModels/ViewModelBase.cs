using Prism.Mvvm;
using Prism.Commands;
using Prism.Regions;

namespace Demo.ViewModels;

internal class ViewModelBase : BindableBase, INavigationAware, IRegionMemberLifetime
{
    private IRegionManager _regionManager;

    public ViewModelBase(IRegionManager regionManager)
    {
        this._regionManager = regionManager;
    }

    public DelegateCommand<string> NavigateCmd => new((nextViewName) => 
    {
        this._regionManager.RequestNavigate("PrimaryContent", nextViewName);
    });

    public bool KeepAlive => false;

    public bool IsNavigationTarget(NavigationContext navigationContext) => false;

    public virtual void OnNavigatedFrom(NavigationContext navigationContext)
    {
        
    }

    public virtual void OnNavigatedTo(NavigationContext navigationContext)
    {
        
    }
}
