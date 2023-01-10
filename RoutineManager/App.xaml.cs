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
    
        protected override void OnStartup(StartupEventArgs e)
        {

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel()
            };
            MainWindow.Show();

            base.OnStartup(e);
        }
        
        public App()
        {
            Services = ConfigureServices();
        }

        /// <summary>
        /// Gets the current <see cref="App"/> instance in use
        /// </summary>
        public new static App Current => (App)Application.Current;

        /// <summary>
        /// Gets the <see cref="IServiceProvider"/> instance to resolve application services.
        /// </summary>
        public IServiceProvider? Services { get; }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private static IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            //Services
            services.AddSingleton<IStartupService, StartupService>();
            services.AddSingleton<IMonitorService, MonitorService>();
            services.AddSingleton<IBackupService, BackupService>();
            services.AddSingleton<ICalendarService, CalendarService>();

            //ViewModels
            services.AddTransient<StartupViewModel>();
            services.AddTransient<MonitorViewModel>();
            services.AddTransient<BackupViewModel>();
            services.AddTransient<CalendarViewModel>();

            return services.BuildServiceProvider();
        }

        

    }
}
