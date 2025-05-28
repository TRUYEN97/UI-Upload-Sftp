using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDownload.Common
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
