using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
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
                if (checkURLValid(url))
                    Process.Start(new ProcessStartInfo($"{url}") { UseShellExecute = true });
            }
        }

        //Returns true if the given string is a valid HTTP or HTTPS url.
        private static bool checkURLValid(string str)
        {
            Uri uriResult;
            return Uri.TryCreate(str, UriKind.Absolute, out uriResult) && 
                (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        public async Task startProcess()
        {
            await Task.Delay(100);
        }
    }
}
