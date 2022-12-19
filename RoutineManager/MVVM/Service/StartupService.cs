using System;
using System.Diagnostics;
using System.IO;
using RoutineManager.MVVM.Model;

namespace RoutineManager.MVVM.Service
{
    public class StartupService
    {
        private Startup startup;
        public void openURL(params string[] urlList)
        {
            for (int i = 0; i < urlList.Length; i++)
            {
                string url = urlList[i];
                if (checkURL(url))
                    startProcess(url);
            }
        }

        public void openFile(params string[] fileList)
        {
            for (int i = 0; i < fileList.Length; i++)
            {
                string filePath = fileList[i];
                if (checkFilePath(filePath))
                    startProcess(filePath);
            }
        }

        public void startProcess(string processName)
        {
            Process.Start(new ProcessStartInfo($"{processName}") { UseShellExecute = true });
        }

        //Returns true if the given string is a valid HTTP or HTTPS url.
        private static bool checkURL(string str)
        {
            Uri uriResult;
            return Uri.TryCreate(str, UriKind.Absolute, out uriResult) && 
                (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        //Returns true if the given string is a valid absolute file path in the user's system.
        private static bool checkFilePath(string str)
        {
            return Directory.Exists(str);
        }

    }
}
