using System;
using System.Diagnostics;
using System.IO;
using RoutineManager.MVVM.Model;

namespace RoutineManager.MVVM.Service
{
    public class StartupService : IStartupService
    {
        public bool startProcess(string processName)
        {
            try
            {
                Process.Start(new ProcessStartInfo($"{processName}") { UseShellExecute = true });
                return true;
            }
            catch
            {
                return false;
            }
            
        }

        //Returns true if the given string is a valid HTTP or HTTPS url.
        public bool isValidURL(string str)
        {
            Uri uriResult;
            return Uri.TryCreate(str, UriKind.Absolute, out uriResult) &&
                (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        //Returns true if the given string is a valid absolute file path in the user's system.
        public bool isValidFilePath(string str)
        {
            return Directory.Exists(str);
        }

    }
}
