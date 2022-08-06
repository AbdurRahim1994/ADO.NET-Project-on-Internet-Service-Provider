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
    public partial class frmUpdatePackagePlan : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-H2IQOKK;Initial Catalog=ADODB;Integrated Security=True");
        public frmUpdatePackagePlan()
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
            //cmbPackagePlanName.SelectedIndex = (int)EnumList.PackagePlanName.Unlimited_Package_1;

            string[] packageSpeedElements = Enum.GetNames(typeof(EnumList.PackageSpeed));
            foreach (var item in packageSpeedElements)
            {
                cmbPackageSpeed.Items.Add(item);
            }
            //cmbPackageSpeed.SelectedIndex = (int)EnumList.PackageSpeed.Mbps_5;
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
        private void LoadGrid()
        {
            txtID.Enabled = true;
            txtPrice.Focus();
            LoadCombo();
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM packagePlan", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            grdUpdatePackage.DataSource = dt;
        }

        private void frmUpdatePackagePlan_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM packagePlan WHERE planId=@PlanID", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@PlanID", txtID.Text);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txtID.Text = dr[0].ToString();
                cmbPackagePlanName.SelectedItem = dr[1].ToString();
                cmbPackageSpeed.SelectedItem = dr[2].ToString();
                txtPrice.Text = dr[3].ToString();
            }
            else
            {
                MessageBox.Show("ID is needed to make search operation", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            dr.Close();
            con.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                EnumList.PackagePlanName packagePlanName = (EnumList.PackagePlanName)cmbPackagePlanName.SelectedIndex;
                EnumList.PackageSpeed packageSpeed = (EnumList.PackageSpeed)cmbPackageSpeed.SelectedIndex;
                SqlCommand cmd = new SqlCommand("UPDATE packagePlan SET planName=@PlanName, speed=@Speed, price=@Price", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Price", txtPrice.Text.Trim());
                cmd.Parameters.AddWithValue("@PlanName", packagePlanName.ToString());
                cmd.Parameters.AddWithValue("@Speed", packageSpeed.ToString());
                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data Updated Successfully!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch
            {
                MessageBox.Show("Something is wrong. Please try again", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
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
            frmUpdateCommandList updateCommandList = new frmUpdateCommandList();
            updateCommandList.Show();
            this.Hide();
        }
    }
}
