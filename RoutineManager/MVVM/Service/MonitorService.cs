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

        public int listenForNewProcesses(string fileExtension)
        {
            //process.MainModule.FileName
            //https://stackoverflow.com/questions/6575117/how-to-wait-for-process-that-will-be-started
            throw new NotImplementedException();
        }

        public TimeSpan getRuntime(Process process)
        {
            if (process == null)
                return TimeSpan.Zero;

            return process.ExitTime - process.StartTime;
        }

        public bool writeListToFile()
        {
            throw new NotImplementedException();
        }

        /*
         * https://learn.microsoft.com/en-us/dotnet/api/system.management.managementeventwatcher?view=dotnet-plat-ext-7.0
         * https://learn.microsoft.com/en-us/dotnet/api/system.management.managementeventwatcher.-ctor?view=dotnet-plat-ext-7.0#system-management-managementeventwatcher-ctor
         */


        private String[] getCommandLineArgsFromProcess(Process process)
        {
            //Update this to split more efficient arrays
            Regex regex = new Regex("\"([^\"\\\\]*(?:\\\\.[^\"\\\\]*)*)\"|'([^'\\\\]*(?:\\\\.[^'\\\\]*)*)'|[^\\s]+");

            using (ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("SELECT CommandLine FROM Win32_Process WHERE ProcessId = " + process.Id))

            using (ManagementObjectCollection objects = searcher.Get())
            {
                var cmdArguments = objects.Cast<ManagementBaseObject>().SingleOrDefault()?["CommandLine"]?.ToString();
                if (cmdArguments != null)
                    return regex.Split(cmdArguments);
            }

            return Array.Empty<string>();
        }

        private int parseArguments(Process process, string[] arguments, string fileExtension)
        {
            foreach (var arg in arguments)
            {
                arg.Trim();
                bool emptyArgument = arg.Equals(" ") || arg.Equals(string.Empty);
                if (!emptyArgument && arg.Contains(fileExtension))
                {
                    process.EnableRaisingEvents = true;
                    process.Exited += (sender, e) => getRuntime(sender as Process);

                    MonitorItem? item = _items.Find(item => item.Name == arg);
                    if (item == null)
                        _items.Add(new MonitorItem { Name = arg, Runtime = TimeSpan.Zero, AssociatedProcess = process });
                    else
                        item.AssociatedProcess = process;

                    return 1;
                }
            }
            return 0;
        }
    }
}
