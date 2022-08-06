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
    public partial class frmAddEmployee : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-H2IQOKK;Initial Catalog=ADODB;Integrated Security=True");
        string imgloc = "";
        public frmAddEmployee()
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
            cmbGender.SelectedIndex =(int) EnumList.Gender.Male;
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
            if (txtContact.Text == string.Empty||txtContact.Text.Length > 11)
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

        private void frmAddEmployee_Load(object sender, EventArgs e)
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
            frmInsertCommandList insertCommandList = new frmInsertCommandList();
            insertCommandList.Show();
            this.Hide();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT MAX(employeeId)+1", con);
                cmd.CommandType = CommandType.Text;
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtID.Text = dr[0].ToString();
                }
                dr.Close();
            }
            catch
            {
                MessageBox.Show("Empty table can't return any value. Please do the Insert operation at first", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (isValid())
            {
                try
                {
                    byte[] img = null;
                    FileStream fs = new FileStream(imgloc, FileMode.Open, FileAccess.Read);
                    BinaryReader br = new BinaryReader(fs);
                    img = br.ReadBytes((int)fs.Length);
                    EnumList.Gender gender = (EnumList.Gender)cmbGender.SelectedIndex;
                    SqlCommand cmd = new SqlCommand("INSERT INTO employee VALUES(@FirstName, @LastName, @Gender, @Contact, @Email, @DateOfBirth, @NID, @Address, @Postal, @Image, @EmployeeType)", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@FirstName", txtFirstName.Text.Trim());
                    cmd.Parameters.AddWithValue("@LastName", txtLastName.Text.Trim());
                    cmd.Parameters.AddWithValue("@Postal", txtPostalCode.Text.Trim());
                    cmd.Parameters.AddWithValue("@Contact", txtContact.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@NID", txtNID.Text.Trim());
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text.Trim());
                    cmd.Parameters.AddWithValue("@EmployeeType", txtEmployeeType.Text.Trim());
                    cmd.Parameters.AddWithValue("@DateOfBirth", dateTimePicker1.Value);
                    cmd.Parameters.AddWithValue("@Gender", gender.ToString());
                    cmd.Parameters.Add(new SqlParameter("@Image", img));
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Data Inserted Successfully!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("Contact Number or Email or NID is already exists", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
                finally
                {
                    con.Close();
                    Reset();
                    txtFirstName.Focus();
                }
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "JPG Files(*.jpg)|*.jpg|PNG Files(*.png)|*.png|All Files(*.*)|*.*";
            dlg.Title="Select Employee Picture";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                imgloc = dlg.FileName.ToString();
                picEmployee.ImageLocation = imgloc;
            }
        }
    }
}
