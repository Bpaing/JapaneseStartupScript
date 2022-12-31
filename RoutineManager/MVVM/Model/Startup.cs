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
    public class Startup
    {
        public List<String> Urls { get; set; }
        public List<String> FilePaths { get; set; }
    }
}
