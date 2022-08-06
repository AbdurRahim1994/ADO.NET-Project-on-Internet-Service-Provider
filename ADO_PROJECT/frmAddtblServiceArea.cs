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
    public partial class frmAddtblServiceArea : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\XML Practise File\ADO.NET\ISPDB(Internet Service Provider Database)\ADO_PROJECT\ADOPROJECTDB(DISCONNECTED DATABASE).mdf;Integrated Security=True");
        string imgloc = "";
        public frmAddtblServiceArea()
        {
            InitializeComponent();
        }
        private void LoadGrid()
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tblserviceArea", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            grdShowServiceArea.DataSource = dt;
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
        private bool isValid()
        {
            if (txtAreaName.Text == "")
            {
                MessageBox.Show("Enter Area Name", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return false;
            }
            if (txtServiceProviderName.Text == "")
            {
                MessageBox.Show("Enter Provider Name", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return false;
            }
            if (picServiceProvider.Image == null)
            {
                MessageBox.Show("Enter Service Provider Image", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return false;
            }
            if (txtServiceCharge.Text == "")
            {
                MessageBox.Show("Enter Service Charge", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return false;
            }
            if (txtServiceProviderSalary.Text == "")
            {
                MessageBox.Show("Enter Salary", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return false;
            }
            if (txtOfficeAddressID.Text == "")
            {
                MessageBox.Show("Enter Office Area ID", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return false;
            }
            if (Convert.ToInt32 (txtServiceCharge.Text) >= 1000)
            {
                MessageBox.Show("Service Charge should not be exceeds 1000", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return false;
            }
            if (Convert.ToInt32(txtServiceProviderSalary.Text) < 10000)
            {
                MessageBox.Show("Salary must be greater than 10000", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return false;
            }
            if (Convert.ToInt32(txtServiceProviderSalary.Text) < Convert.ToInt32(txtServiceCharge.Text))
            {
                MessageBox.Show("Salary must be greater than service charge", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void frmAddtblServiceArea_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JPG Files(*.jpg)|*.jpg|PNG Files(*.png)|*.png|All Files(*.*)|*.*";
            dlg.Title = "Select Service Provider Picture";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                imgloc = dlg.FileName.ToString();
                picServiceProvider.ImageLocation = imgloc;
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (isValid())
            {
                byte[] img = null;
                FileStream fs = new FileStream(imgloc, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                img = br.ReadBytes((int)fs.Length);
                SqlCommand cmd = new SqlCommand("INSERT INTO tblserviceArea VALUES(@AreaName, @ProviderName, @ProviderImage, @ServiceCharge, @Salary, @OfficeAddress)", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@AreaName", txtAreaName.Text);
                cmd.Parameters.AddWithValue("@ProviderName", txtServiceProviderName.Text);
                cmd.Parameters.AddWithValue("@ServiceCharge", txtServiceCharge.Text);
                cmd.Parameters.AddWithValue("@Salary", txtServiceProviderSalary.Text);
                cmd.Parameters.AddWithValue("@OfficeAddress", txtOfficeAddressID.Text);
                cmd.Parameters.Add(new SqlParameter("@ProviderImage", img));

                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Service Area Inserted Successfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGrid();
                Reset();
                con.Close();
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
