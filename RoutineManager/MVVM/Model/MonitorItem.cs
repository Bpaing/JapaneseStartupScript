using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutineManager.MVVM.Model
{
    public class MonitorItem
    {
        //Key: Path of file being monitored
        //Value: Runtime of the file found by subtracting process ExitTime from StartTime.
        public string Name { get; set; }
        public TimeSpan Runtime { get; set; }
        public Process? AssociatedProcess { get; set; }
    }
}
