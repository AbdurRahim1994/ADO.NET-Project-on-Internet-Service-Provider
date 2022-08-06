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
using System.IO;

namespace ADO_PROJECT
{
    public partial class frmDeletetblServiceArea : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\XML Practise File\ADO.NET\ISPDB(Internet Service Provider Database)\ADO_PROJECT\ADOPROJECTDB(DISCONNECTED DATABASE).mdf;Integrated Security=True");
        public frmDeletetblServiceArea()
        {
            InitializeComponent();
        }
        private void Reset()
        {
            txtAreaID.Text = string.Empty;
            txtAreaName.Text = string.Empty;
            txtServiceProviderName.Text = string.Empty;
            txtServiceCharge.Text = string.Empty;
            txtServiceProviderSalary.Text = string.Empty;
            txtOfficeAddressID.Text = string.Empty;
            picServiceProvider.Image = null;
        }
        private void LoadGrid()
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tblserviceArea", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            grdShowServiceArea.DataSource = dt;
        }

        private void frmDeletetblServiceArea_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            btnBrowse.Enabled = false;
            if (txtAreaID.Text != "")
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tblserviceArea WHERE serviceAreaId=@ID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", txtAreaID.Text.Trim());

                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtAreaName.Text = dr[1].ToString();
                    txtServiceProviderName.Text = dr[2].ToString();
                    byte[] img = (byte[])dr[3];
                    if (img == null)
                    {
                        picServiceProvider.Image = null;
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream(img);
                        picServiceProvider.Image = Image.FromStream(ms);
                    }
                    txtServiceCharge.Text = dr[4].ToString();
                    txtServiceProviderSalary.Text = dr[5].ToString();
                    txtOfficeAddressID.Text = dr[6].ToString();
                }
                else
                {
                    MessageBox.Show("Enter Valid ID", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
                dr.Close();
                con.Close();
            }
            else
            {
                MessageBox.Show("Enter Area ID", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtAreaID.Text != "")
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM tblserviceArea WHERE serviceAreaId=@ID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", txtAreaID.Text.Trim());

                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Service Area Deleted", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGrid();
                Reset();
                con.Close();
            }
            else
            {
                MessageBox.Show("Enter Area ID", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            frmDeleteCommandList deleteCommandList = new frmDeleteCommandList();
            deleteCommandList.Show();
            this.Hide();
        }
    }
}
