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
    public partial class frmRegistration : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-H2IQOKK;Initial Catalog=ADODB;Integrated Security=True");
        public frmRegistration()
        {
            InitializeComponent();
        }
        private void Reset()
        {
            txtUserID.Text = null;
            txtFirstName.Text = null;
            txtLastName.Text = null;
            txtEmail.Text = string.Empty;
            txtContact.Text = string.Empty;
            txtAddress.Text = "";
            txtUserName.Text = "";
            txtPassword.Text = string.Empty;
            txtConfirmPassword.Text = null;
        }
        private bool isValid()
        {
            if (txtFirstName.Text == null)
            {
                MessageBox.Show("Please Enter First Name", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (txtLastName.Text == null)
            {
                MessageBox.Show("Please Enter Last Name", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (txtEmail.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Email", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (txtContact.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Contact", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            else if (txtAddress.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Address", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (txtUserName.Text == "")
            {
                MessageBox.Show("Please Enter User Name", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (txtPassword.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Password", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if(txtPassword.Text.Length < 6)
            {
                MessageBox.Show("Password must be at least 6 characters", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Password don't match", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;

        }

        private void btnRegistration_Click(object sender, EventArgs e)
        {
            if (isValid())
            {
                SqlCommand cmd = new SqlCommand("spAddUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@firstName", txtFirstName.Text.Trim());
                cmd.Parameters.AddWithValue("@lastName", txtLastName.Text.Trim());
                cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@address", txtAddress.Text.Trim());
                cmd.Parameters.AddWithValue("@contact", txtContact.Text.Trim());
                cmd.Parameters.AddWithValue("@userName", txtUserName.Text.Trim());
                cmd.Parameters.AddWithValue("@password", txtPassword.Text.Trim());

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Data Registered Successfully!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Reset();
            }
            else
            {
                MessageBox.Show("Invalid Data!!!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmRegistration_Load(object sender, EventArgs e)
        {
            txtUserID.Enabled = false;
        }

        private void btnGotoLOGIN_Click(object sender, EventArgs e)
        {
            frmLogin frmLogin = new frmLogin();
            frmLogin.Show();
            this.Hide();
        }
    }
}
