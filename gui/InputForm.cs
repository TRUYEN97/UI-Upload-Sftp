using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Upload.gui
{
    public partial class InputForm : Form
    {
        private InputForm(string title, string defaultValue, bool passWordMode = false)
        {
            InitializeComponent();
            this.lbLbTitle.Text = title;
            this.txtInput.Text = defaultValue;
            if (passWordMode)
            {
                this.txtInput.UseSystemPasswordChar = passWordMode;
            }
        }

        public static string GetInputString(string title, string defaultValue = null)
        {
            var inputForm = new InputForm(title, defaultValue);
            if (inputForm.ShowDialog() == DialogResult.OK &&  !string.IsNullOrWhiteSpace(inputForm.txtInput.Text))
            {
                return inputForm.txtInput.Text.Trim();
            }
            return null;
        }
        public static string GetInputPassword(string title)
        {
            var inputForm = new InputForm(title, null, true);
            if (inputForm.ShowDialog() == DialogResult.OK &&  !string.IsNullOrWhiteSpace(inputForm.txtInput.Text))
            {
                return inputForm.txtInput.Text.Trim();
            }
            return null;
        }

        private void txtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DialogResult = DialogResult.OK;
            }
        }
    }
}
