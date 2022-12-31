using CommunityToolkit.Mvvm.ComponentModel;
using RoutineManager.MVVM.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutineManager.MVVM.ViewModel
{
    /*
     * list of URLs
     * url
     * url
     * url
     * -------------------
     * list of processes
     * process
     * process
     * process
     * -------------------
     * Monitor Output list
     * cbz
     * cbz
     * cbz
     * 
     *  load strings into Startup for StartupService to execute
     *  for each list, you can append, edit or delete items
     *  (create prompts when attempting to edit or delete)
     *  
     */
    public partial class StartupViewModel : ObservableObject
    {
        private readonly IStartupService StartupService;

        public StartupViewModel(IStartupService startupService)
        {
            StartupService = startupService;
            
        }

        public ObservableCollection<string> Paths { get; } = new ObservableCollection<string>();

    }
}
