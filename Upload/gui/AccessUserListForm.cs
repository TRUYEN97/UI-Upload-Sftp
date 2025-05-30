using System.Windows.Forms;
using static Upload.Service.LockManager;

namespace Upload.gui
{
    public partial class AccessUserListForm : Form
    {
        private readonly AccessUserControl _accessControl = new AccessUserControl();
        public AccessUserListForm()
        {
            InitializeComponent();
            this.Controls.Add(this._accessControl);
        }
    }
}
