using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADO_PROJECT
{
    public partial class frmAddPaymentMethod : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-H2IQOKK;Initial Catalog=ADODB;Integrated Security=True");
        string imgloc = "";
        public frmAddPaymentMethod()
        {
            InitializeComponent();
        }
        private void LoadGrid()
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM paymentMethod", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            grdAddPaymentMethod.DataSource = dt;
        }

        private void frmAddPaymentMethod_Load(object sender, EventArgs e)
        {
            txtID.Enabled = false;
            LoadGrid();
        }
        private bool isValid()
        {
            if (txtMethodName.Text == string.Empty)
            {
                MessageBox.Show("Please Enter Method Name", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }
        private void Reset()
        {
            txtID.Clear();
            txtMethodName.Text = string.Empty;
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT MAX(paymentMethodID)+1 FROM paymentMethod", con);
            cmd.CommandType = CommandType.Text;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txtID.Text = dr[0].ToString();
            }
            dr.Close();
            con.Close();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "JPG Files(*.jpg)|*.jpg|PNG Files(*.png)|*.png|All Files(*.*)|*.*";
                dlg.Title = "Select Method Picture";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    imgloc = dlg.FileName.ToString();
                    picPaymentMethod.ImageLocation = imgloc;
                }
            }
            catch
            {
                MessageBox.Show("Image not found", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
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
                SqlCommand cmd = new SqlCommand("spInsertPaymentMethod", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@paymentMethodName", txtMethodName.Text.Trim());
                cmd.Parameters.Add(new SqlParameter("@methodImage", img));

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Data Inserted Successfully!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Reset();
                LoadGrid();

            }
            else
            {
                MessageBox.Show("All the text boxes must be filled", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
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
