using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoDownload.Common;

namespace AutoDownload.Gui
{
    public class LoggerBox
    {
        private static readonly Lazy <LoggerBox> _instance = new Lazy<LoggerBox>(()=>new LoggerBox());
        public static TextBox TxtMessager { get; set; }
        private readonly  List<string> logLines;
        private readonly int maxLinesToShow = 10;
        private LoggerBox() {
            logLines = new List<string>();
        }

        private static LoggerBox Instance =>  _instance.Value;

        public static void Addlog(string message)
        {
            LoggerBox.Instance.AddMessage(message);
        }

        public void AddMessage(string message)
        {
            logLines.Add($"{DateTime.Now:HH:mm:ss} -> {message}");
            if (logLines.Count > maxLinesToShow)
            {
                logLines.RemoveAt(0);
            }
            Util.SafeInvoke(TxtMessager, () =>
            {
                TxtMessager.Lines = logLines.ToArray();
                TxtMessager.SelectionStart = TxtMessager.Text.Length;
                TxtMessager.ScrollToCaret();
            });
           
        }


    }
}
