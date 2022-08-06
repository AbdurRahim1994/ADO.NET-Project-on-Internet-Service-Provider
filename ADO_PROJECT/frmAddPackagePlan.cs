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
    public partial class frmAddPackagePlan : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-H2IQOKK;Initial Catalog=ADODB;Integrated Security=True");
        public frmAddPackagePlan()
        {
            InitializeComponent();
        }
        private void LoadCombo()
        {
            string[] packagePlanElements = Enum.GetNames(typeof(EnumList.PackagePlanName));
            foreach (var item in packagePlanElements)
            {
                cmbPackagePlanName.Items.Add(item);
            }
            cmbPackagePlanName.SelectedIndex = (int)EnumList.PackagePlanName.Unlimited_Package_1;

            string[] packageSpeedElements = Enum.GetNames(typeof(EnumList.PackageSpeed));
            foreach (var item in packageSpeedElements)
            {
                cmbPackageSpeed.Items.Add(item);
            }
            cmbPackageSpeed.SelectedIndex = (int)EnumList.PackageSpeed.Mbps_5;
        }
        private void Reset()
        {
            txtID.Clear();
            txtPrice.Text = string.Empty;
            cmbPackagePlanName.Text = string.Empty;
            cmbPackageSpeed.Text = string.Empty;
        }
        private bool isValid()
        {
            if (txtPrice.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Package Price", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void frmAddPackagePlan_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void LoadGrid()
        {
            txtID.Enabled = false;
            txtPrice.Focus();
            LoadCombo();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM packagePlan", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            grdAddPackagePlan.DataSource = dt;
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT MAX(planId)+1 FROM packagePlan", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txtID.Text = dr[0].ToString();
            }
            con.Close();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (isValid())
            {
                EnumList.PackageSpeed packageSpeed = (EnumList.PackageSpeed)cmbPackageSpeed.SelectedIndex;
                EnumList.PackagePlanName packagePlanName = (EnumList.PackagePlanName)cmbPackagePlanName.SelectedIndex;
                SqlCommand cmd = new SqlCommand("INSERT INTO packagePlan VALUES(@PlanName, @Speed, @Price)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Price", txtPrice.Text.Trim());
                cmd.Parameters.AddWithValue("@PlanName", packagePlanName.ToString());
                cmd.Parameters.AddWithValue("@Speed", packageSpeed.ToString());
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Data Inserted Successfully!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGrid();
                Reset();
                
            }

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            frmInsertCommandList insertCommandList = new frmInsertCommandList();
            insertCommandList.Show();
            this.Hide();
        }
    }
}
