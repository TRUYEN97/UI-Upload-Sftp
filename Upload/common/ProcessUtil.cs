using System;
using System.Diagnostics;
using System.Linq;

namespace Upload.Common
{
    public class ProcessUtil
    {
        public static Process IsRunning(string processName)
        {
            if(processName != null && !Process.GetProcessesByName(processName).Any())
            {
                foreach (Process proc in Process.GetProcesses())
                {
                    try
                    {
                        if (proc.MainModule != null
                            && proc.MainModule.FileName.Equals(processName, StringComparison.OrdinalIgnoreCase))
                        {
                           return proc;
                        }
                    }
                    catch
                    {
                    }
                }
            }
            return null;
        }
        public static Process IsRunningWithWindowTitle(string windowTitle)
        {
            if(windowTitle != null)
            {
                foreach (Process proc in Process.GetProcesses())
                {
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(proc.MainWindowTitle))
                        {
                            if (proc.MainWindowTitle.Contains(windowTitle))
                            {
                                return proc;
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            }
            return null;
        }
    }
}
