using RoutineManager.MVVM.Model;
using RoutineManager.MVVM.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutineManager.MVVM.ViewModel
{
    public class BackupViewModel : ViewModelBase
    {
        private readonly IBackupService? _backupService;
        public BackupViewModel(IBackupService backupService)
        {
            _backupService = backupService;
        }

    }
}
