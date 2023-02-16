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

        public int grabCurrentlyRunningProcesses(string fileExtension)
        {
            Process[] allRunningProcesses = Process.GetProcesses();
            int processesGrabbed = 0;

            foreach (var process in allRunningProcesses)
            {
                string[] formattedArguments = getCommandLineArgsFromProcess(process);
                processesGrabbed += parseArguments(process, formattedArguments, fileExtension);
            }

            return processesGrabbed;
        }

        //https://stackoverflow.com/questions/12159989/asynchronous-wmi-event-handling-in-net
        //For async, use Start() instead of WaitForNextEvent(), add event handler method to EventArrived subscriber.
        public int listenForNewProcesses(string fileExtension)
        {
            // Create event query to be notified within 1 second of
            // a change in a service
            WqlEventQuery query =
                new WqlEventQuery("__InstanceCreationEvent",
                new TimeSpan(0, 0, 1),
                "TargetInstance isa \"Win32_Process\"");

            // Initialize an event watcher and subscribe to events
            // that match this query
            ManagementEventWatcher watcher = new ManagementEventWatcher(query);
            watcher.EventArrived += new EventArrivedEventHandler(this.verifyProcess);
            watcher.Start();
            Console.WriteLine("Listening for new Processes...");
            Thread.Sleep(20000);
            Console.WriteLine("Stopping...");
            watcher.Stop();
            return 0;
        }

        private void verifyProcess(object sender, EventArrivedEventArgs e)
        {
            ManagementBaseObject targetInstance = (ManagementBaseObject)e.NewEvent["TargetInstance"];
            Console.WriteLine(targetInstance);

            bool success = int.TryParse(targetInstance["ProcessId"]?.ToString(), out int pid);
            Process process = Process.GetProcessById(pid);
            String[] args = splitArguments(targetInstance["CommandLine"]?.ToString());

        }

        public TimeSpan getRuntime(Process process)
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
                return splitArguments(cmdArguments);
            }
        }

        private String[] splitArguments(String commandLineArgs)
        {
            //Update this to split more efficient arrays
            Regex regex = new Regex("\"([^\"\\\\]*(?:\\\\.[^\"\\\\]*)*)\"|'([^'\\\\]*(?:\\\\.[^'\\\\]*)*)'|[^\\s]+");
            if (commandLineArgs != null)
                return regex.Split(commandLineArgs);
            return new string[0];
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

    }
}
