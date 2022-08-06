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
    public partial class frmDeletePaymentMethod : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-H2IQOKK;Initial Catalog=ADODB;Integrated Security=True");
        string imgloc = "";
        public frmDeletePaymentMethod()
        {
            InitializeComponent();
        }
        private void LoadGrid()
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM paymentMethod", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            grdDeletePaymentMethod.DataSource = dt;
        }
        private void Reset()
        {
            txtID.Clear();
            txtMethodName.Text = string.Empty;
        }

        private void frmDeletePaymentMethod_Load(object sender, EventArgs e)
        {
            txtID.Enabled = true;
            LoadGrid();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "JPG Files(*.jpg)|*.jpg|PNG Files(*.png)|*.png|All Files(*.*)|*.*";
                dlg.Title = "Select Payment Method Picture";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    imgloc = dlg.FileName.ToString();
                    picPaymentMethod.ImageLocation = imgloc;
                }
            }
            catch
            {
                MessageBox.Show("Picture not found", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM paymentMethod WHERE paymentMethodID=@ID", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", txtID.Text.Trim());
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txtID.Text = dr[0].ToString();
                txtMethodName.Text = dr[1].ToString();
                byte[] img = (byte[])(dr[2]);
                if (img == null)
                {
                    picPaymentMethod.Image = null;
                }
                else
                {
                    MemoryStream ms = new MemoryStream(img);
                    picPaymentMethod.Image = Image.FromStream(ms);
                }
            }
            else
            {
                MessageBox.Show("ID must be needed to make search operation", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            dr.Close();
            con.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //byte[] img = null;
            //FileStream fs = new FileStream(imgloc, FileMode.Open, FileAccess.Read);
            //BinaryReader br = new BinaryReader(fs);
            //img = br.ReadBytes((int)fs.Length);
            SqlCommand cmd = new SqlCommand("DELETE FROM paymentMethod WHERE paymentMethodID=@ID", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ID", txtID.Text.Trim());
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Data Deleted", "Deleted", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            Reset();
            picPaymentMethod.Image = null;
            LoadGrid();

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
