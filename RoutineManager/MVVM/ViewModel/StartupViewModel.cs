using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RoutineManager.MVVM.Model;
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
    public partial class StartupViewModel : ViewModelBase
    {
        private readonly IStartupService StartupService;
        private ObservableCollection<StartupItem> ItemList { get; } = new();

        public StartupViewModel(IStartupService startupService)
        {
            StartupService = startupService;
        }


        //Validate each url/filepath. If all pass, start processes and save to settings for later.
        [RelayCommand]
        private void ProcessForm()
        {

        }


        //Adds an input row.
        //https://stackoverflow.com/questions/24960476/dynamically-adding-textbox-using-a-button-within-mvvm-framework
        [RelayCommand]
        private void AddItem()
        {
            this.ItemList.Add(new StartupItem());
        }

        //Deletes an input row.
        [RelayCommand]
        private void DeleteItem(object selectedItem)
        {
            try
            {
                this.ItemList.Remove((StartupItem)selectedItem);
            }
            catch
            {

            }
        }

        [RelayCommand]
        private void EditItem()
        {

        }

        //https://stackoverflow.com/questions/1619505/wpf-openfiledialog-with-the-mvvm-pattern
        [RelayCommand]
        private void ChooseFile()
        {

        }

        //Loads data saved by Monitor service into MonitorData.
        //This should be ran everytime the application starts up.
        //Does nothing if data loaded is identical or data does not exist.
        private void LoadMonitorData()
        {

        }

    }
}
