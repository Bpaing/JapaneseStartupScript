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
using System.Threading;

namespace RoutineManager.MVVM.Service
{
    public class MonitorService : IMonitorService
    {
        private List<MonitorItem> _items = new();
        private HashSet<string> _fileExtensionsToMonitor = new();

        public int grabCurrentlyRunningProcesses()
        {
            Process[] allRunningProcesses = Process.GetProcesses();
            int processesGrabbed = 0;

            foreach (var process in allRunningProcesses)
            {
                string[] arguments = getArgumentsFromProcess(process);
                processesGrabbed += parseArguments(process, arguments);
            }

            return processesGrabbed;
        }


        public int listenForNewProcesses()
        {
            int processesGrabbed = 0;

            WqlEventQuery query =
                new WqlEventQuery("__InstanceCreationEvent",
                new TimeSpan(0, 0, 1),
                "TargetInstance isa \"Win32_Process\"");

            ManagementEventWatcher watcher = new ManagementEventWatcher(query);
            watcher.EventArrived += new EventArrivedEventHandler((sender, e) =>
            {
                ManagementBaseObject targetInstance = (ManagementBaseObject)e.NewEvent["TargetInstance"];
                Console.WriteLine(targetInstance);

                bool success = int.TryParse(targetInstance["ProcessId"]?.ToString(), out int pid);
                Process process = Process.GetProcessById(pid);

                string[] arguments = splitArguments(targetInstance["CommandLine"]?.ToString());
                processesGrabbed += parseArguments(process, arguments);
            });
            watcher.Start();

            Thread.Sleep(20000);

            watcher.Stop();

            return processesGrabbed;
        }

        private String[] getArgumentsFromProcess(Process process)
        {
            using (ManagementObjectSearcher searcher =
                    new ManagementObjectSearcher("SELECT CommandLine FROM Win32_Process WHERE ProcessId = " + process.Id))

            using (ManagementObjectCollection objects = searcher.Get())
            {
                var commandArguments = objects.Cast<ManagementBaseObject>().SingleOrDefault()?["CommandLine"]?.ToString();
                return splitArguments(commandArguments);
            }
        }

        private String[] splitArguments(String arguments)
        {
            //Update this to split more efficient arrays
            Regex regex = new Regex("\"([^\"\\\\]*(?:\\\\.[^\"\\\\]*)*)\"|'([^'\\\\]*(?:\\\\.[^'\\\\]*)*)'|[^\\s]+");
            if (arguments != null)
                return regex.Split(arguments);
            return new string[0];
        }

        private int parseArguments(Process process, string[] arguments)
        {
            foreach (var arg in arguments)
            {
                arg.Trim();
                bool emptyArgument = arg.Equals(" ") || arg.Equals(string.Empty);
                bool validFileExtension = _fileExtensionsToMonitor.Contains(arg);

                if (!emptyArgument && validFileExtension)
                {
                    process.EnableRaisingEvents = true;
                    process.Exited += (sender, e) => getRuntime(sender as Process);

                    MonitorItem? item = _items.FirstOrDefault(item => item.Name == arg);
                    if (item == null)
                        _items.Add(new MonitorItem { Name = arg, Runtime = TimeSpan.Zero, AssociatedProcess = process });
                    else
                        item.AssociatedProcess = process;

                    return 1;
                }
            }
            return 0;
        }


        private TimeSpan getRuntime(Process process)
        {
            if (process == null)
                return TimeSpan.Zero;

            TimeSpan runtime = process.ExitTime - process.StartTime;
            var item = _items.FirstOrDefault(item => item.AssociatedProcess == process);
            if (item != null)
            {
                item.Runtime += runtime;
                item.AssociatedProcess = null;
            }

            return runtime;
        }


        public bool writeListToFile()
        {
            throw new NotImplementedException();
        }

        public void addFileExtension(string fileExtension) { _fileExtensionsToMonitor.Add(fileExtension);}
        public void removeFileExtension(string fileExtension) { _fileExtensionsToMonitor.Remove(fileExtension); }
    }
}
