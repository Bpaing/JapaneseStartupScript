/*
This file uses code from the following link:
https://stackoverflow.com/questions/349336/testing-process-start
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoutineManagerTest
{
    internal class MockProcess
    {
    }

    /*
                 public class UtilityManager
             {
                  public Process UtilityProcess { get; private set; }

                  private bool _isRunning;

                  public UtilityManager() : this(null) {}

                  public UtilityManager( Process process )
                  {
                      this. UtilityProcess = process ?? new Process();
                      this._isRunning = false;
                  }

                  public void Start()
                  {
                      if (!_isRunning) {
                      var startInfo = new ProcessStartInfo() {
                          CreateNoWindow = true,
                          UseShellExecute = true,

                          FileName = _cmdLine,
                          Arguments = _args
                      };

                      this.UtilityProcess.Start(startInfo);
                      _isRunning = true;

                  } else {
                      throw new InvalidOperationException("Process already started");
                  }
             }

            [TestMethod]
         public void StartTest()
         {
              Process proc = new FakeProcess();  // May need to use a wrapper class
              UtilityManager manager = new UtilityManager( proc );
              manager.CommandLine = "command";
              ...

              manager.Start();


              Assert.IsTrue( proc.StartCalled );
              Assert.IsNotNull( proc.StartInfo );
              Assert.AreEqual( "command", proc.StartInfo.FileName );
              ...
         }
    */
}
