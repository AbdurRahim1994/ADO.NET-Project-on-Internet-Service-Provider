using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADO_PROJECT
{
    public partial class frmLogin : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-H2IQOKK;Initial Catalog=ADODB;Integrated Security=True");
        public frmLogin()
        {
            InitializeComponent();
        }

        private void lblLinkRegistration_Click(object sender, EventArgs e)
        {
            frmRegistration registration = new frmRegistration();
            registration.Show();
            this.Hide();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Registration WHERE userName='" + txtUserName.Text.Trim() + "' and password='" + txtPassword.Text.Trim() + "'", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count == 1)
            {
                frmHome home = new frmHome();
                home.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("User Name or Password don't match. Please Enter valid Data", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                txtUserName.Clear();
                txtPassword.Text = string.Empty;
                txtUserName.Focus();
            }
        }

        private void lblLinkRevoverPassword_Click(object sender, EventArgs e)
        {
            frmPasswordRecovery frmPasswordRecovery = new frmPasswordRecovery();
            frmPasswordRecovery.Show();
            this.Hide();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowPassword.Checked)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }
    }
}
