using Prism.Mvvm;
using Prism.Commands;
using Prism.Regions;

namespace Demo.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        private bool _flag = true;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public DelegateCommand NavigationCommand { get; private set; }
        private void navigation()
        {
            var nextViewName = this._flag ? "Dummy1" : "Dummy2";
            this._regionManager.RequestNavigate("ContentRegion", nextViewName);
            this._flag = !this._flag;
        }

        private IRegionManager _regionManager;

        public MainWindowViewModel(IRegionManager regionManager)
        {
            this._regionManager = regionManager;
            this.NavigationCommand = new DelegateCommand(this.navigation);
        }
    }
}
