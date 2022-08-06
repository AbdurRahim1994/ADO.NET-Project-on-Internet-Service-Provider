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
    public partial class frmUpdatePaymentMethod : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-H2IQOKK;Initial Catalog=ADODB;Integrated Security=True");
        string imgloc = "";
        public frmUpdatePaymentMethod()
        {
            InitializeComponent();
        }
        private void LoadGrid()
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM paymentMethod", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            grdUpdatePaymentMethod.DataSource = dt;
        }
        private void Reset()
        {
            txtID.Clear();
            txtMethodName.Text = string.Empty;
        }

        private void frmUpdatePaymentMethod_Load(object sender, EventArgs e)
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

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] img= null;
                FileStream fs = new FileStream(imgloc, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                img = br.ReadBytes((int)fs.Length);
                SqlCommand cmd = new SqlCommand("UPDATE paymentMethod SET paymentMethodName=@Name, methodImage=@Image WHERE paymentMethodID=@ID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@Name", txtMethodName.Text.Trim());
                cmd.Parameters.AddWithValue("@ID", txtID.Text.Trim());
                cmd.Parameters.Add(new SqlParameter("@Image", img));
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("ID can't be Updated", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
                Reset();
                LoadGrid();
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
