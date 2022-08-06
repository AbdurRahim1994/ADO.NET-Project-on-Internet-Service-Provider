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
    public partial class frmDeleteCustomer : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-H2IQOKK;Initial Catalog=ADODB;Integrated Security=True");
        public frmDeleteCustomer()
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
                dateTimePicker1.Value = (DateTime)dr[8];
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
        private void btnDelete_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM customer WHERE IPNumber=@IP", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@IP", txtIPNumber.Text.Trim());
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Data Deleted!!", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Reset();
            txtIPNumber.Focus();
        }

        private void frmDeleteCustomer_Load(object sender, EventArgs e)
        {
            txtID.Enabled = false;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            frmDeleteCommandList deleteCommandList = new frmDeleteCommandList();
            deleteCommandList.Show();
            this.Hide();
        }
    }
}
