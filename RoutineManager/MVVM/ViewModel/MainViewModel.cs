using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace RoutineManager.MVVM.ViewModel
{
    [ObservableObject]
    public partial class MainViewModel
    {
        [ObservableProperty]
        private ViewModelBase _currentView;

        public MonitorViewModel MonitorViewModel { get; set; }
        public CalendarViewModel CalendarViewModel { get; set; }
        public BackupViewModel BackupViewModel { get; set; }

        public MainViewModel()
        {
            MonitorViewModel = new MonitorViewModel();
            CalendarViewModel = new CalendarViewModel();
            BackupViewModel = new BackupViewModel();
            CurrentView = BackupViewModel;
        }

        [RelayCommand]
        public void DisplayMonitor()
        {
            CurrentView = MonitorViewModel;
        }

        [RelayCommand]
        public void DisplayCalendar()
        {
            CurrentView = CalendarViewModel;
        }



    }
}