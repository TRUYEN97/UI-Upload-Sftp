using System.Windows.Forms;

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

        public void LoadModel(string removePath)
        {
            this._accessControl.LoadFormPath(removePath);
        }

    }
}
