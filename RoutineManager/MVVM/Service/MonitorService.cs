using RoutineManager.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Management;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace RoutineManager.MVVM.Service
{
    public class MonitorService : IMonitorService
    {
        private List<MonitorItem> _items = new();
        private List<Process> _processes = new();
        public int grabCurrentlyRunningProcesses(string fileExtension)
        {
            Process[] allRunningProcesses = Process.GetProcesses();
            int processesGrabbed = 0;

            foreach (var process in allRunningProcesses)
            {
                String[] formattedArguments = getCommandLineArgsFromProcess(process);
                processesGrabbed += parseArguments(process, formattedArguments, fileExtension);
            }

            return processesGrabbed;
        }

        private String[] getCommandLineArgsFromProcess(Process process)
        {
            Regex regex = new Regex("\"([^\"\\\\]*(?:\\\\.[^\"\\\\]*)*)\"|'([^'\\\\]*(?:\\\\.[^'\\\\]*)*)'|[^\\s]+");

            using (ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("SELECT CommandLine FROM Win32_Process WHERE ProcessId = " + process.Id))

            using (ManagementObjectCollection objects = searcher.Get())
            {
                var cmdArguments = objects.Cast<ManagementBaseObject>().SingleOrDefault()?["CommandLine"]?.ToString();
                if (cmdArguments != null)
                    return regex.Split(cmdArguments);
                return new string[0];
            }
        }

        private int parseArguments(Process process, String[] formattedArguments, String fileExtension)
        {
            foreach (var argument in formattedArguments)
            {
                argument.Trim();
                if (!argument.Equals(" ") && !argument.Equals(string.Empty) && argument.Contains(fileExtension))
                {
                    process.EnableRaisingEvents = true;
                    process.Exited += (sender, e) => getRuntime(sender);
                    _processes.Add(process);
                    return 1;
                }
            }
            return 0;
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
         */
    }
}
