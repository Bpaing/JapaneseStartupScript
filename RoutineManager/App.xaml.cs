using Microsoft.Extensions.DependencyInjection;
using RoutineManager.MVVM.Service;
using RoutineManager.MVVM.ViewModel;
using RoutineManager.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RoutineManager
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider? _serviceProvider;

        public App()
        {
            _serviceProvider = ConfigureServices();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            MainWindow.Show();
            base.OnStartup(e);
        }

        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            services.AddSingleton<MainWindow>(provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });

            //Services
            services.AddSingleton<IStartupService, StartupService>();
            services.AddSingleton<IMonitorService, MonitorService>();
            services.AddSingleton<ICalendarService, CalendarService>();

            services.AddSingleton<INavigationService, NavigationService>();
            services.AddSingleton<Func<Type, ViewModelBase>>
                (provider => viewModelType => (ViewModelBase)provider.GetRequiredService(viewModelType));

            //ViewModels
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<StartupViewModel>();
            services.AddSingleton<MonitorViewModel>();
            services.AddSingleton<CalendarViewModel>();

            return services.BuildServiceProvider();
        }

    }
}
