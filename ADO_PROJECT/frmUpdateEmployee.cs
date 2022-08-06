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
    public partial class frmUpdateEmployee : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-H2IQOKK;Initial Catalog=ADODB;Integrated Security=True");
        string imgloc = "";
       

        public frmUpdateEmployee()
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
        private bool isValid()
        {
            if (txtFirstName.Text == null)
            {
                MessageBox.Show("Please Enter First Name", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return false;
            }
            if (txtLastName.Text == "")
            {
                MessageBox.Show("Please Enter Last Name", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return false;
            }
            if (txtContact.Text == string.Empty || txtContact.Text.Length > 11)
            {
                MessageBox.Show("Contact Number is empty or greater than 11 digits", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return false;
            }
            if (txtEmail.Text == "")
            {
                MessageBox.Show("Please Enter Email", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return false;
            }
            if (txtNID.Text == string.Empty)
            {
                MessageBox.Show("Please Enter NID Number", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return false;
            }
            if (txtAddress.Text == "")
            {
                MessageBox.Show("Please Enter Address", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return false;
            }
            if (txtEmployeeType.Text == null)
            {
                MessageBox.Show("Please Enter Employee Type", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return false;
            }
            if (picEmployee.Image == null)
            {
                MessageBox.Show("Please Enter Image", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                return false;
            }
            return true;
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

        private void frmUpdateEmployee_Load(object sender, EventArgs e)
        {
            txtID.Enabled = false;
            LoadCombo();
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM employee WHERE contactNumber=@Contact", con);
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Contact", txtContact.Text.Trim());
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
                MessageBox.Show("Data no found. Contact Number is needed to make search operation", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            con.Close();
            txtNID.Enabled = false;
            
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] img = null;
                FileStream fs = new FileStream(imgloc, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                img = br.ReadBytes((int)fs.Length);
                EnumList.Gender gender = (EnumList.Gender)cmbGender.SelectedIndex;
                SqlCommand cmd = new SqlCommand("UPDATE employee SET employeeFirstName=@EmployeeFirstName, employeeLastName=@employeeLastName, gender=@Gender, email=@Email, dateOfBirth=@DateOfBirth, NID=@NID, address=@Address, postalCode=@PostalCode, employeeImage=@EmployeeImage, employeeTypeId=@EmployeeTypeId WHERE contactNumber=@ContactNumber", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@EmployeeFirstName", txtFirstName.Text.Trim());
                cmd.Parameters.AddWithValue("@employeeLastName", txtLastName.Text.Trim());
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@NID", txtNID.Text.Trim());
                cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                cmd.Parameters.AddWithValue("@PostalCode", txtPostalCode.Text.Trim());
                cmd.Parameters.AddWithValue("@EmployeeTypeId", txtEmployeeType.Text.Trim());
                cmd.Parameters.AddWithValue("@ContactNumber", txtContact.Text.Trim());
                cmd.Parameters.AddWithValue("@DateOfBirth", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@Gender", gender.ToString());
                cmd.Parameters.Add(new SqlParameter("@EmployeeImage", img));

                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Data Updated Successfully!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            catch
            {
                MessageBox.Show("Phote is needed to make update operation", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
                Reset();
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JPG Files(*.jpg)|*.jpg|PNG Files(*.png)|*.png|All Files(*.*)|*.*";
            dlg.Title = "Select Employee Picture";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                imgloc = dlg.FileName.ToString();
                picEmployee.ImageLocation = imgloc;
            }
        }
    }
}
