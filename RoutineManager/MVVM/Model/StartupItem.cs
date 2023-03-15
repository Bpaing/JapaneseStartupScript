using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace RoutineManager.MVVM.Model
{
    public class StartupItem
    {
        public string? Alias { get; set; }

        //Represents the absolute path of a file or a valid URL.
        //Only visible when editing a StartupItem on the application.
        public string? Path { get; set; }
    }
}
