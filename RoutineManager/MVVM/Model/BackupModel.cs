using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutineManager.MVVM.Model
{
    //Backup store strings to create and run rclone commands in CMD.
    public class BackupModel
    {
        public string Program { get; set; }
        public string Action { get; set; }
        public string SourcePath { get; set; }
        public string DestPath { get; set; }
        public List<string> Flags { get; set; }
    }
}
