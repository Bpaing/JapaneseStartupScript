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
        private List<Process> _processes = new();
        public bool grabProcess()
        {
            Process process = new Process();
            process.EnableRaisingEvents = true;
            process.Exited += (sender, e) => getRuntime(sender);
            _processes.Add(process);
            return true;
        }

        public bool monitorProcess()
        {
            //process.MainModule.FileName
            throw new NotImplementedException();
        }

        public TimeSpan getRuntime(object sender)
        {
            Process? process = sender as Process;

            if (process == null)
                return TimeSpan.Zero;

            _processes.Remove(process);
            return process.ExitTime - process.StartTime;
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
