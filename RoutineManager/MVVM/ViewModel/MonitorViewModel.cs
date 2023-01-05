using RoutineManager.MVVM.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutineManager.MVVM.ViewModel
{
    public class MonitorViewModel : ViewModelBase
    {
        private readonly IMonitorService? _monitorService;
        public MonitorViewModel(IMonitorService monitorService)
        {
            _monitorService = monitorService;
        }
    }
}
