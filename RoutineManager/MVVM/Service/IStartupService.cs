using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutineManager.MVVM.Service
{
    public interface IStartupService
    {
        bool startProcess(string processName);
        bool isValidURL(string str);
        bool isValidFilePath(string str);
        bool readMonitorDataFromFile(string filePath);
    }
}
