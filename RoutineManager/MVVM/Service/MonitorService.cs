using RoutineManager.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutineManager.MVVM.Service
{
    public class MonitorService : IMonitorService
    {
        private List<MonitorItem> _items = new();
        
        public bool grabProcess()
        {
            throw new NotImplementedException();
        }

        public bool monitorProcess()
        {
            throw new NotImplementedException();
        }

        public TimeSpan getRuntime()
        {
            throw new NotImplementedException();
        }

        public bool writeListToFile(int numProcessesToSave)
        {
            throw new NotImplementedException();
        }

        public bool isValidFilePath(string str)
        {
            return Directory.Exists(str);
        }


    }
}
