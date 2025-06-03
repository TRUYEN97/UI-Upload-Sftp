using System;
using System.Windows.Forms;
using Upload.Common;
using Upload.Model;

namespace Upload.gui
{
    public partial class UserForm : Form
    {
        private readonly Control firstControl;
        private UserForm(UserModel user)
        {
            InitializeComponent();
            Text = "Change password";
            txtId.ReadOnly = true;
            txtId.Text = user.Id;
            firstControl = txtPassword;
        }
        private UserForm()
        {
            InitializeComponent();
            firstControl = txtId;
        }
        internal static bool EditUserModel(UserModel userModel)
        {
            var inputForm = new UserForm(userModel);
            if (inputForm.ShowDialog() == DialogResult.OK)
            {
                userModel.Id = inputForm.Id;
                userModel.Password = Util.GetMD5HashFromString(inputForm.Password); 
                return true;
            }
            return false;
        }
        internal static UserModel CreateUserModel()
        {
            var inputForm = new UserForm();
            if (inputForm.ShowDialog() == DialogResult.OK)
            {
                var userModel = new UserModel();
                userModel.Id = inputForm.Id;
                userModel.Password = Util.GetMD5HashFromString(inputForm.Password);
                return userModel;
            }
            return null;
        }
        internal string Password => txtPassword.Text;
        internal string Id => txtId.Text;

        private void btOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Id) || string.IsNullOrWhiteSpace(Password))
            {
                MessageBox.Show("Id or Password invalid!");
                firstControl.Focus();
                return;
            }
            DialogResult = DialogResult.OK;
        }

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               txtPassword.Focus();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btOk.Focus();
            }
        }
    }
}
