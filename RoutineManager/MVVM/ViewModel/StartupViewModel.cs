using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using RoutineManager.MVVM.Model;
using RoutineManager.MVVM.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.IO;
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
        public ObservableCollection<StartupItem> FileList { get; set; }
        public ObservableCollection<StartupItem> UrlList { get; set; }

        public StartupViewModel(IStartupService startupService)
        {
            _startupService = startupService;
            FileList = new();
            UrlList = new();
        }

        //Validate each url/filepath. If all pass, start processes and save to settings for later.
        [RelayCommand]
        public void ProcessForm()
        {

        }


        //Adds an input row.
        [RelayCommand]
        public void AddItem(string type) 
        { 
            switch (type)
            {
                case "File":
                    this.FileList.Add(new StartupItem());
                    break;
                case "URL":
                    this.UrlList.Add(new StartupItem());
                    break;
            }
        }

        //Deletes an input row.
        [RelayCommand]
        public void DeleteItem(StartupItem selectedItem) 
        {
            if (this.FileList.Contains(selectedItem))
                this.FileList.Remove(selectedItem);
            else
                this.UrlList.Remove(selectedItem);
        }

        [RelayCommand]
        public void EditAlias(StartupItem selectedItem)
        {

        }

        //https://stackoverflow.com/questions/1619505/wpf-openfiledialog-with-the-mvvm-pattern
        [RelayCommand]
        public void ChooseFile(StartupItem selectedItem)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == true)
                selectedItem.Path = dlg.FileName;
        }

        //Loads data saved by Monitor service into MonitorData.
        //This should be ran everytime the application starts up.
        //Does nothing if data loaded is identical or data does not exist.
        public void LoadMonitorData()
        {

        }

    }
}
