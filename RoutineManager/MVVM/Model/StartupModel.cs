using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace RoutineManager.MVVM.Model
{

    //Startup will open browser URLs and start processes based on user input.
    public class StartupModel
    {
        public List<Url> Uris { get; set; }
        public List<Process> Processes { get; set; }
    }
}
