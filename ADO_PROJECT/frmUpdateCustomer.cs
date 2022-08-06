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
    public partial class frmUpdateCustomer : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-H2IQOKK;Initial Catalog=ADODB;Integrated Security=True");
        public frmUpdateCustomer()
        {
            InitializeComponent();
        }
        private void Reset()
        {
            txtID.Clear();
            txtFirstName.Text = string.Empty;
            txtLastName.Text = "";
            txtContact.Text = string.Empty;
            txtAddress.Clear();
            txtPostalCode.Text = "";
            txtEmail.Clear();
            txtIPNumber.Clear();
            dateTimePicker1.Text = string.Empty;
            txtPackageID.Clear();
            txtPaymentMethodID.Text = null;
        }
        private bool isValid()
        {
            if (txtFirstName.Text == null)
            {
                MessageBox.Show("Please Enter First Name", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return false;
            }
            else if (txtLastName.Text == "")
            {
                MessageBox.Show("Please Enter Last Name", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return false;
            }
            else if (txtContact.Text == null || txtContact.Text.Length < 6 || txtContact.Text.Length > 11)
            {
                MessageBox.Show("Contact Number must be at least 6 digits and maximum 11 digits and must not be empty", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return false;
            }
            else if (txtAddress.Text == "")
            {
                MessageBox.Show("Please Enter Address", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return false;
            }
            else if (txtEmail.Text == "")
            {
                MessageBox.Show("Email must be unique or not to be null", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return false;
            }
            else if (txtIPNumber.Text == "")
            {
                MessageBox.Show("IP Number must be unique or not to be null", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return false;
            }
            else if (txtPaymentMethodID.Text == "")
            {
                MessageBox.Show("Please Enter Payment Method", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM customer WHERE IPNumber=@IP", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@IP", txtIPNumber.Text);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txtID.Text = dr[0].ToString();
                txtFirstName.Text = dr[1].ToString();
                txtLastName.Text = dr[2].ToString();
                txtContact.Text = dr[3].ToString();
                txtAddress.Text = dr[4].ToString();
                txtPostalCode.Text = dr[5].ToString();
                txtEmail.Text = dr[6].ToString();
                txtIPNumber.Text = dr[7].ToString();
                dateTimePicker1.Value =(DateTime) dr[8];
                txtPackageID.Text = dr[9].ToString();
                txtPaymentMethodID.Text = dr[10].ToString();
            }
            else
            {
                MessageBox.Show("Either IP Number is empty or don't match", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            dr.Close();
            con.Close();
            txtIPNumber.Focus();
        }

        private void frmUpdateCustomer_Load(object sender, EventArgs e)
        {
            txtID.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (isValid())
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spUpdateCustomer", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@customerFirstName", txtFirstName.Text.Trim());
                    cmd.Parameters.AddWithValue("@customerLastName", txtLastName.Text.Trim());
                    cmd.Parameters.AddWithValue("@contactNumber", txtContact.Text.Trim());
                    cmd.Parameters.AddWithValue("@address", txtAddress.Text.Trim());
                    cmd.Parameters.AddWithValue("@postalCode", txtPostalCode.Text.Trim());
                    cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@IPNumber", txtIPNumber.Text.Trim());
                    cmd.Parameters.AddWithValue("@planId", txtPackageID.Text.Trim());
                    cmd.Parameters.AddWithValue("@paymentMethodID", txtPaymentMethodID.Text.Trim());
                    cmd.Parameters.AddWithValue("@connectionStartDate", dateTimePicker1.Value);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Updated Successfully!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("Contact Number or Email or IP Number is already exists", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    con.Close();
                    Reset();
                    txtIPNumber.Focus();
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            frmUpdateCommandList updateCommandList = new frmUpdateCommandList();
            updateCommandList.Show();
            this.Hide();
        }
    }
}
