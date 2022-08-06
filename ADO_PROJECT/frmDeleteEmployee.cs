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
    public partial class frmDeleteEmployee : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-H2IQOKK;Initial Catalog=ADODB;Integrated Security=True");
        public frmDeleteEmployee()
        {
            InitializeComponent();
        }
        private void LoadCombo()
        {
            string[] genderElements = Enum.GetNames(typeof(EnumList.Gender));
            foreach (var item in genderElements)
            {
                cmbGender.Items.Add(item);
            }
            cmbGender.SelectedIndex = (int)EnumList.Gender.Male;
        }
        private void Reset()
        {
            txtID.Text = null;
            txtFirstName.Clear();
            txtLastName.Text = string.Empty;
            cmbGender.Text = "";
            txtContact.Clear();
            txtEmail.Text = null;
            dateTimePicker1.Text = "";
            txtNID.Clear();
            txtAddress.Text = string.Empty;
            txtPostalCode.Clear();
            txtEmployeeType.Text = "";
            picEmployee.Image = null;
        }

        private void frmDeleteEmployee_Load(object sender, EventArgs e)
        {
            LoadCombo();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM employee WHERE employeeId=@EmployeeId", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@EmployeeId", txtID.Text.Trim());
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txtID.Text = dr[0].ToString();
                txtFirstName.Text = dr[1].ToString();
                txtLastName.Text = dr[2].ToString();
                cmbGender.SelectedItem = dr[3].ToString();
                txtEmail.Text = dr[5].ToString();
                dateTimePicker1.Value = (DateTime)dr[6];
                txtNID.Text = dr[7].ToString();
                txtAddress.Text = dr[8].ToString();
                txtPostalCode.Text = dr[9].ToString();
                byte[] img = (byte[])dr[10];
                txtEmployeeType.Text = dr[11].ToString();
                if (img == null)
                {
                    picEmployee.Image = null;
                }
                else
                {
                    MemoryStream ms = new MemoryStream(img);
                    picEmployee.Image = Image.FromStream(ms);
                }
            }
            else
            {
                MessageBox.Show("Data no found. Invalid ID Number", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            con.Close();
            txtNID.Enabled = false;
            txtID.Enabled = false;
            
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("spDeleteEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@employeeId", txtID.Text.Trim());
                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data Deleted!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Make the search operation first then make delete operation", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
