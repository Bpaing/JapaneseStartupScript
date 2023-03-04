using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using RoutineManager.MVVM.Service;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace RoutineManager.MVVM.ViewModel
{
    public partial class MainViewModel : ViewModelBase
    {
        [ObservableProperty]
        private INavigationService? _navigationService;


        public MainViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        [RelayCommand]
        public void NavigateStartup()
        {
            NavigationService?.NavigateTo<StartupViewModel>();
        }

        [RelayCommand]
        public void NavigateMonitor()
        {
            NavigationService?.NavigateTo<MonitorViewModel>();
        }

        [RelayCommand]
        public void NavigateCalendar()
        {
            NavigationService?.NavigateTo<CalendarViewModel>();
        }

    }
}