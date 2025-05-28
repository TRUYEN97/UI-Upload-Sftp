using System;
using System.Windows.Forms;

namespace Upload.gui
{
    public partial class ConfirmOverrideFile : Form
    {
        private int isAll = 0;
        public ConfirmOverrideFile()
        {
            InitializeComponent();
        }

        public bool IsAccerpt(string mess)
        {
            if (isAll != 0)
            {
                return isAll == 1;
            }
            this.txtMess.Text = mess;
            return ShowDialog() == DialogResult.OK;
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            isAll = cbAll.Checked? 1 : 0;
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            isAll = cbAll.Checked ? 2 : 0;
        }
    }
}
