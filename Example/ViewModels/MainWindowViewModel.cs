using Prism.Mvvm;
using Prism.Regions;
using Prism.Commands;
using System.Collections.ObjectModel;
using Acl = AnimatedContentControlLib.Core.Constants;

namespace Example.ViewModels;

public class MainWindowViewModel : BindableBase
{
    private IRegionManager _regionManager;
    private string _currentContent = "Control1";

    #region コマンド関連
    public DelegateCommand NavigateCmd { get; private set; }
    private void navigate()
    {
        this._currentContent = this._currentContent == "Control1" ? "Control2" : "Control1";
        this._regionManager.RequestNavigate("ContentRegion", this._currentContent);
    }

    public DelegateCommand LoadedCmd { get; private set; }
    private void loaded()
    {
        this._regionManager.RequestNavigate("ContentRegion", this._currentContent);
    }
    #endregion

    #region プロパティ関連
    private ObservableCollection<string> _animationName = new();
    public ObservableCollection<string> AnimationNameList
    {
        get => this._animationName;
        set => this.SetProperty(ref this._animationName, value);
    }
    #endregion

    public MainWindowViewModel(IRegionManager regionManager)
    {
        this._regionManager = regionManager;
        this.NavigateCmd = new DelegateCommand(this.navigate);
        this.LoadedCmd = new DelegateCommand(this.loaded);

        this.AnimationNameList.Add(Acl.EmbededAnimations.SlideinDown);
        this.AnimationNameList.Add(Acl.EmbededAnimations.SlideinUp);
        this.AnimationNameList.Add(Acl.EmbededAnimations.SlideinLeft);
        this.AnimationNameList.Add(Acl.EmbededAnimations.SlideinRight);
        this.AnimationNameList.Add(Acl.EmbededAnimations.ModernSlideinDown);
        this.AnimationNameList.Add(Acl.EmbededAnimations.ModernSlideinUp);
        this.AnimationNameList.Add(Acl.EmbededAnimations.ModernSlideinLeft);
        this.AnimationNameList.Add(Acl.EmbededAnimations.ModernSlideinRight);
    }
}
