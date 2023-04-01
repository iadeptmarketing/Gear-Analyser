using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eicher
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            LoginCheck = false;
            IsAdminUser = false;
        }

        public bool LoginCheck
        { get; set; }

        public bool IsAdminUser
        { get; set; }
        private void buttonOK_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBoxUser.Text) || string.IsNullOrEmpty(textBoxPassword.Text))
            {
                MessageBox.Show("Username/Password field can not be empty","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            FileHandling fileHandling = new FileHandling();
            var password = fileHandling.GetPassword(textBoxUser.Text);
            if(password==null)
            {
                MessageBox.Show("Incorrect Username/Password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if(string.Equals( password,textBoxPassword.Text))
            {
                LoginCheck = true;
                if (string.Equals(textBoxUser.Text, "Admin"))
                {
                    IsAdminUser = true;
                }
                this.Close();
            }
        }
    }
}
