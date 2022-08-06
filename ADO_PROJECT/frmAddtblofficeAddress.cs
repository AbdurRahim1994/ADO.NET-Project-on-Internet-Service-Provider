using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ADO_PROJECT
{
    public partial class frmAddtblofficeAddress : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\XML Practise File\ADO.NET\ISPDB(Internet Service Provider Database)\ADO_PROJECT\ADOPROJECTDB(DISCONNECTED DATABASE).mdf;Integrated Security=True");
        public frmAddtblofficeAddress()
        {
            InitializeComponent();
        }
        private void Reset()
        {
            txtOfficeAddressID.Text = string.Empty;
            txtOfficeAddressName.Clear();
        }
        private void LoadGrid()
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tblofficeAddress", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            grdOfficeAddress.DataSource = dt;
        }
        private bool isValid()
        {
            if (txtOfficeAddressName.Text == "")
            {
                MessageBox.Show("Office Address can't be empty", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void frmAddtblofficeAddress_Load(object sender, EventArgs e)
        {
            txtOfficeAddressID.Enabled = false;
            LoadGrid();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (isValid()) 
            {
                if (txtOfficeAddressName.Text.Length < 30)
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO tblofficeAddress VALUES(@name)", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@name", txtOfficeAddressName.Text.Trim());
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    Reset();
                    LoadGrid();
                    lblmsg.Text = "Data Inserted Successfully";
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblmsg.Text = "Office Address must less than 30 characters";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            frmInsertCommandList insertCommandList = new frmInsertCommandList();
            insertCommandList.Show();
            this.Hide();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT MAX(officeAddressId)+1 FROM tblofficeAddress", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txtOfficeAddressID.Text = dr[0].ToString();
            }
            else
            {
                MessageBox.Show("No data found. Please enter a data at first", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            dr.Close();
            con.Close();
        }
    }
}
