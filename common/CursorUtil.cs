using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoDownload.Common;

namespace Upload.common
{
    internal class CursorUtil
    {
        private static Lazy<CursorUtil> _instance = new Lazy<CursorUtil>(() => new CursorUtil());

        public static CursorUtil Instance => _instance.Value;
        private CursorUtil() { }
        public FormMain FormMain {  get; set; }

        public void SetCursor(Cursor cursor)
        {
            Util.SetCursor(FormMain, cursor);
        }
        
        public static void SetCursorIs(Cursor cursor)
        {
            Instance.SetCursor(cursor);
        }
    }

    
}
