using CommunityToolkit.Mvvm.ComponentModel;
using RoutineManager.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutineManager.MVVM.Service
{
    //Reference: https://www.youtube.com/watch?v=wFzmBZpjuAo
    public partial class NavigationService : ObservableObject, INavigationService
    {
        [ObservableProperty]
        private ViewModelBase? _currentView;
        private readonly Func<Type, ViewModelBase> _viewModelFactory;


        public NavigationService(Func<Type, ViewModelBase> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }


        public void NavigateTo<TViewModel>() where TViewModel : ViewModelBase
        {
            ViewModelBase viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
            CurrentView = viewModel;
        }
    }
}
