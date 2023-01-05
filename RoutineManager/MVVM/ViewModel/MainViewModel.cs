using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace RoutineManager.MVVM.ViewModel
{
    public partial class MainViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ViewModelBase? _currentView;

        public MainViewModel()
        {
            CurrentView = App.Current.Services.GetService<StartupViewModel>();
        }

        [RelayCommand]
        public void DisplayStartup()
        {
            CurrentView = App.Current.Services.GetService<StartupViewModel>();
        }

        [RelayCommand]
        public void DisplayMonitor()
        {
            CurrentView = App.Current.Services.GetService<MonitorViewModel>();
        }

        [RelayCommand]
        public void DisplayBackup()
        {
            CurrentView = App.Current.Services.GetService<BackupViewModel>();
        }

        [RelayCommand]
        public void DisplayCalendar()
        {
            CurrentView = App.Current.Services.GetService<CalendarViewModel>();
        }

    }
}