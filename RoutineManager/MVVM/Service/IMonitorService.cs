using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutineManager.MVVM.Service
{
    public interface IMonitorService
    {


        bool grabProcess();
        bool monitorProcess();
        TimeSpan getRuntime();

        bool isValidFilePath(string str);
        bool writeListToFile(int numProcessesToSave);
        


        /*
         * Process represents a file with the specified extension.
         * 
         * First grab all currently running processes to List. 
         * Then Add Any newly opened processes to List.
         * 
         * Listen for closing flags, calculate runtime for closed process.
         * 
         * 
         * Validate all filepaths then serialize to file for later use.
         */
    }
}
