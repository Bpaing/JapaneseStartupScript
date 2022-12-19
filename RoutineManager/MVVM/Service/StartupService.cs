using System;
using System.Diagnostics;
using System.IO;
using RoutineManager.MVVM.Model;

namespace RoutineManager.MVVM.Service
{
    public class StartupService
    {
        private Startup startup;

        public int openURL(params string[] urlList)
        {
            int urlsOpened = 0;
            for (int i = 0; i < urlList.Length; i++)
            {
                string url = urlList[i];
                if (isValidURL(url))
                {
                    //startProcess(url);
                    urlsOpened++;
                }
            }
            return urlsOpened;
        }

        public int openFile(params string[] fileList)
        {
            int filesOpened = 0;
            for (int i = 0; i < fileList.Length; i++)
            {
                string filePath = fileList[i];
                if (isValidFilePath(filePath))
                {
                    //startProcess(filePath);
                    filesOpened++;
                }
            }
            return filesOpened;
        }

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
        public static bool isValidURL(string str)
        {
            Uri uriResult;
            return Uri.TryCreate(str, UriKind.Absolute, out uriResult) &&
                (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        //Returns true if the given string is a valid absolute file path in the user's system.
        public static bool isValidFilePath(string str)
        {
            return Directory.Exists(str);
        }

    }
}
