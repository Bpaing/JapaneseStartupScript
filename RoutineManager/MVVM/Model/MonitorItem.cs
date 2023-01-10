using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutineManager.MVVM.Model
{
    public class MonitorItem
    {
        //Key: Path of file being monitored
        //Value: Runtime of the file found by subtracting process ExitTime from StartTime.
        public KeyValuePair<string, TimeSpan> File { get; set; }
    }
}
