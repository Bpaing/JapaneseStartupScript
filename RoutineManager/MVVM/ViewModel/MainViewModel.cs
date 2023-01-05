using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace RoutineManager.MVVM.ViewModel
{
    public partial class MainViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ViewModelBase _currentView;


        public MainViewModel()
        {
            _currentView = new MonitorViewModel();
        }

    }
}