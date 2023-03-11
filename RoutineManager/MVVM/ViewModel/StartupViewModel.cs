using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RoutineManager.MVVM.Model;
using RoutineManager.MVVM.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        private readonly IStartupService? _startupService;
        public ObservableCollection<StartupItem> ItemList { get; set; }

        public StartupViewModel(IStartupService startupService)
        {
            _startupService = startupService;
            ItemList = new();
        }

        //Validate each url/filepath. If all pass, start processes and save to settings for later.
        [RelayCommand]
        public void ProcessForm()
        {

        }


        //Adds an input row.
        //https://stackoverflow.com/questions/24960476/dynamically-adding-textbox-using-a-button-within-mvvm-framework
        [RelayCommand]
        public void AddItem()
        {
            this.ItemList.Add(new StartupItem() { Alias="Test", Path="TestPath"});
            this.ItemList.Add(new StartupItem() { Alias = "Test2", Path = "TestPath2" });
        }

        //Deletes an input row.
        [RelayCommand]
        public void DeleteItem(StartupItem selectedItem)
        {
            this.ItemList.Remove(selectedItem);
        }

        [RelayCommand]
        public void EditItem(StartupItem selectedItem)
        {

        }

        //https://stackoverflow.com/questions/1619505/wpf-openfiledialog-with-the-mvvm-pattern
        [RelayCommand]
        public void ChooseFile()
        {

        }

        //Loads data saved by Monitor service into MonitorData.
        //This should be ran everytime the application starts up.
        //Does nothing if data loaded is identical or data does not exist.
        public void LoadMonitorData()
        {

        }

    }
}
