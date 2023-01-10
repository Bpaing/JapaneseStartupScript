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

        /*
         * https://learn.microsoft.com/en-us/dotnet/api/system.management.managementeventwatcher?view=dotnet-plat-ext-7.0
         * https://learn.microsoft.com/en-us/dotnet/api/system.management.managementeventwatcher.-ctor?view=dotnet-plat-ext-7.0#system-management-managementeventwatcher-ctor
            using System.Diagnostics;
            using System.Management;
            using System.Text.RegularExpressions;

            Process[] pp = Process.GetProcesses();
            Regex regex = new Regex("\"([^\"\\\\]*(?:\\\\.[^\"\\\\]*)*)\"|'([^'\\\\]*(?:\\\\.[^'\\\\]*)*)'|[^\\s]+");
            foreach (var process in pp)
            {

                using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT CommandLine FROM Win32_Process WHERE ProcessId = " + process.Id))
                using (ManagementObjectCollection objects = searcher.Get())
                {
                    var cmdArguments = objects.Cast<ManagementBaseObject>().SingleOrDefault()?["CommandLine"]?.ToString();
                    if (cmdArguments != null)
                    {
                        var fileName = regex.Split(cmdArguments);
                        foreach (var s in fileName)
                        {
                            s.Trim();
                            if (!s.Equals(" ") && !s.Equals(string.Empty) && s.Contains(".cbz"))
                            {
                                Console.WriteLine(s);
                            }
               
                        }
                    }
                }
            }
         */
    }
}
