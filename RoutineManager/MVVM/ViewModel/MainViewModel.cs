using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace RoutineManager.MVVM.ViewModel
{
    public partial class MainViewModel : ViewModelBase
    {
        [ObservableProperty]
        private ViewModelBase? _currentView;

        private ObservableCollection<ViewModelBase> _otherViewModels = new();


        public MainViewModel()
        {
   
            _otherViewModels.Add(App.Current._serviceProvider.GetService<StartupViewModel>());
            _otherViewModels.Add(App.Current._serviceProvider.GetService<MonitorViewModel>());
            _otherViewModels.Add(App.Current._serviceProvider.GetService<CalendarViewModel>());

            CurrentView = _otherViewModels[0];
        }

        [RelayCommand]
        public void DisplayStartup()
        {
            CurrentView = _otherViewModels[0];
        }

        [RelayCommand]
        public void DisplayMonitor()
        {
            CurrentView = _otherViewModels[1];
        }

        [RelayCommand]
        public void DisplayCalendar()
        {
            CurrentView = _otherViewModels[2];
        }

    }
}