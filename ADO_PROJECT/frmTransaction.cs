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
    public partial class frmTransaction : Form
    {
        SqlConnection con = new SqlConnection("Data Source=DESKTOP-H2IQOKK;Initial Catalog=ADODB;Integrated Security=True");
        string imgloc = "";
        public frmTransaction()
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

            string[] pakcagePlanElements = Enum.GetNames(typeof(EnumList.PackagePlanName));
            foreach (var item in pakcagePlanElements)
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
            txtFirstName.Clear();
            txtLastName.Clear();
            txtContact.Clear();
            txtAddress.Clear();
            txtPostalCode.Clear();
            txtEmail.Clear();
            txtIPNumber.Clear();
            dtConnectionStart.Text = string.Empty;
            txtPackageID.Clear();
            txtPaymentMethodID.Clear();
            txtEmpID.Text = null;
            txtEmpFirstName.Text = null;
            txtEmpLastName.Text = null;
            cmbGender.Text = null;
            txtEmpContact.Text = null;
            txtEmpEmail.Text = null;
            dtEmpDOB.Text = string.Empty;
            txtEmpNID.Text = null;
            txtEmpAddress.Text = null;
            txtEmpPostal.Text = null;
            txtEmpType.Text = null;
            txtPackagePlanID.Text = string.Empty;
            cmbPackagePlanName.Text = null;
            cmbPackageSpeed.Text = null;
            txtPackagePrice.Text = string.Empty;
            txtMethodID.Text = "";
            txtMethodName.Text = "";
            picEmployee.Image = null;
            picPaymentMethod.Image = null;
        }


        private void frmTransaction_Load(object sender, EventArgs e)
        {
            LoadCombo();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            frmHome home = new frmHome();
            home.Show();
            this.Hide();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT MAX(customerId)+1 FROM customer", con, transaction);
                cmd.CommandType = CommandType.Text;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtID.Text = dr[0].ToString();
                }
                else
                {
                    MessageBox.Show("Data not found", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
                dr.Close();

                SqlCommand cmd1 = new SqlCommand("SELECT MAX(employeeId)+1 FROM employee", con, transaction);
                cmd1.CommandType = CommandType.Text;
                SqlDataReader dr1 = cmd1.ExecuteReader();
                if (dr1.Read())
                {
                    txtEmpID.Text = dr1[0].ToString();
                }
                else
                {
                    MessageBox.Show("Data not found", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
                dr1.Close();

                SqlCommand cmd2 = new SqlCommand("SELECT MAX(planId)+1 FROM packagePlan", con, transaction);
                cmd2.CommandType = CommandType.Text;
                SqlDataReader dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {
                    txtPackagePlanID.Text = dr2[0].ToString();
                }
                else
                {
                    MessageBox.Show("Data not found", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
                dr2.Close();

                SqlCommand cmd3 = new SqlCommand("SELECT MAX(paymentMethodID)+1 FROM paymentMethod", con, transaction);
                cmd3.CommandType = CommandType.Text;
                SqlDataReader dr3 = cmd3.ExecuteReader();
                if (dr3.Read())
                {
                    txtMethodID.Text = dr3[0].ToString();
                }
                else
                {
                    MessageBox.Show("Data not found", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
                dr3.Close();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                MessageBox.Show("You have to make sure all the tables have data for performing this operation. If no data found please Insert data first", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
            
        }

        private void btnEmpPicBrowse_Click(object sender, EventArgs e)
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

        private void btnPaymentPicBrowse_Click(object sender, EventArgs e)
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

        private void btnInsert_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand("spInsertCustomer", con, transaction);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@customerFirstName", txtFirstName.Text.Trim());
                cmd.Parameters.AddWithValue("@customerLastName", txtLastName.Text.Trim());
                cmd.Parameters.AddWithValue("@contactNumber", txtContact.Text.Trim());
                cmd.Parameters.AddWithValue("@address", txtAddress.Text.Trim());
                cmd.Parameters.AddWithValue("@postalCode", txtPostalCode.Text.Trim());
                cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@IPNumber", txtIPNumber.Text.Trim());
                cmd.Parameters.AddWithValue("@planId", txtPackageID.Text.Trim());
                cmd.Parameters.AddWithValue("@paymentMethodID", txtPaymentMethodID.Text.Trim());
                cmd.Parameters.AddWithValue("@connectionStartDate", dtConnectionStart.Value);
                cmd.ExecuteNonQuery();

                byte[] img = null;
                FileStream fs = new FileStream(imgloc, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                img = br.ReadBytes((int)fs.Length);
                EnumList.Gender gender = (EnumList.Gender)cmbGender.SelectedIndex;
                SqlCommand cmd1 = new SqlCommand("INSERT INTO employee VALUES(@EmployeeFirstName,@EmployeeLastName,@Gender,@ContactNumber,@Email,@DateOfBirth,@NID,@Address,@PostalCode,@EmployeeImage,@EmployeeTypeId)", con, transaction);
                cmd1.CommandType = CommandType.Text;
                cmd1.Parameters.AddWithValue("@EmployeeFirstName", txtEmpFirstName.Text.Trim());
                cmd1.Parameters.AddWithValue("@EmployeeLastName", txtEmpLastName.Text.Trim());
                cmd1.Parameters.AddWithValue("@ContactNumber", txtEmpContact.Text.Trim());
                cmd1.Parameters.AddWithValue("@Email", txtEmpEmail.Text.Trim());
                cmd1.Parameters.AddWithValue("@NID", txtEmpNID.Text.Trim());
                cmd1.Parameters.AddWithValue("@Address", txtEmpAddress.Text.Trim());
                cmd1.Parameters.AddWithValue("@PostalCode", txtEmpPostal.Text.Trim());
                cmd1.Parameters.AddWithValue("@EmployeeTypeId", txtEmpType.Text.Trim());
                cmd1.Parameters.AddWithValue("@Gender", gender.ToString());
                cmd1.Parameters.AddWithValue("@DateOfBirth", dtEmpDOB.Value);
                cmd1.Parameters.Add(new SqlParameter("@EmployeeImage", img));
                cmd1.ExecuteNonQuery();

                EnumList.PackagePlanName packagePlanName = (EnumList.PackagePlanName)cmbPackagePlanName.SelectedIndex;
                EnumList.PackageSpeed packageSpeed = (EnumList.PackageSpeed)cmbPackageSpeed.SelectedIndex;
                SqlCommand cmd2 = new SqlCommand("INSERT INTO packagePlan VALUES(@PlanName,@Speed,@Price)", con, transaction);
                cmd2.CommandType = CommandType.Text;
                cmd2.Parameters.AddWithValue("@Price", txtPackagePrice.Text.Trim());
                cmd2.Parameters.AddWithValue("@PlanName", packagePlanName.ToString());
                cmd2.Parameters.AddWithValue("@Speed", packageSpeed.ToString());
                cmd2.ExecuteNonQuery();

                byte[] img1 = null;
                FileStream fs1 = new FileStream(imgloc, FileMode.Open, FileAccess.Read);
                BinaryReader br1 = new BinaryReader(fs1);
                img1 = br1.ReadBytes((int)fs1.Length);
                SqlCommand cmd3 = new SqlCommand("spInsertPaymentMethod", con, transaction);
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue("@paymentMethodName", txtMethodName.Text.Trim());
                cmd3.Parameters.Add(new SqlParameter("@methodImage", img1));
                cmd3.ExecuteNonQuery();
                transaction.Commit();
                MessageBox.Show("Data Inserted Successfully!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                transaction.Rollback();
                MessageBox.Show("Insert Failed. Some data is already exists", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM packagePlan WHERE planId=@PlanID", con, transaction);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@PlanID", txtPackagePlanID.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtPackagePlanID.Text = dr[0].ToString();
                    cmbPackagePlanName.SelectedItem = dr[1].ToString();
                    cmbPackageSpeed.SelectedItem = dr[2].ToString();
                    txtPackagePrice.Text = dr[3].ToString();
                }
                else
                {
                    MessageBox.Show("ID is needed to make the search operation", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
                dr.Close();

                SqlCommand cmd1 = new SqlCommand("SELECT * FROM employee WHERE employeeId=@ID", con, transaction);
                cmd1.CommandType = CommandType.Text;
                cmd1.Parameters.AddWithValue("@ID", txtEmpID.Text);
                SqlDataReader dr1 = cmd1.ExecuteReader();
                if (dr1.Read())
                {
                    txtEmpID.Text = dr1[0].ToString();
                    txtEmpFirstName.Text = dr1[1].ToString();
                    txtEmpLastName.Text = dr1[2].ToString();
                    cmbGender.SelectedValue = dr1[3].ToString();
                    txtEmpContact.Text = dr1[4].ToString();
                    txtEmpEmail.Text = dr1[5].ToString();
                    dtEmpDOB.Value = (DateTime)dr1[6];
                    txtEmpNID.Text = dr1[7].ToString();
                    txtEmpAddress.Text = dr1[8].ToString();
                    txtEmpPostal.Text = dr1[9].ToString();
                    byte[] img = (byte[])(dr1[10]);
                    if (img == null)
                    {
                        picEmployee.Image = null;
                    }
                    else
                    {
                        MemoryStream ms = new MemoryStream(img);
                        picEmployee.Image = Image.FromStream(ms);
                    }
                    txtEmpType.Text = dr1[11].ToString();
                }
                dr1.Close();

                SqlCommand cmd2 = new SqlCommand("SELECT * FROM paymentMethod WHERE paymentMethodID=@ID", con, transaction);
                cmd2.CommandType = CommandType.Text;
                cmd2.Parameters.AddWithValue("@ID", txtMethodID.Text.Trim());
                SqlDataReader dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {
                    txtMethodID.Text = dr2[0].ToString();
                    txtMethodName.Text = dr2[1].ToString();
                    byte[] img =(byte[]) dr2[2];
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
                dr2.Close();

                SqlCommand cmd3 = new SqlCommand("SELECT * FROM customer WHERE customerId=@ID", con, transaction);
                cmd3.CommandType = CommandType.Text;
                cmd3.Parameters.AddWithValue("@ID", txtID.Text.Trim());
                SqlDataReader dr3 = cmd3.ExecuteReader();
                if (dr3.Read())
                {
                    txtID.Text = dr3[0].ToString();
                    txtFirstName.Text = dr3[1].ToString();
                    txtLastName.Text = dr3[2].ToString();
                    txtContact.Text = dr3[3].ToString();
                    txtAddress.Text = dr3[4].ToString();
                    txtPostalCode.Text = dr3[5].ToString();
                    txtEmail.Text = dr3[6].ToString();
                    txtIPNumber.Text = dr3[7].ToString();
                    dtConnectionStart.Value =(DateTime) dr3[8];
                    txtPackageID.Text = dr3[9].ToString();
                    txtPaymentMethodID.Text = dr3[10].ToString();
                }
                dr3.Close();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                MessageBox.Show("ID is needed to make the search operation", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();
            try
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM packagePlan WHERE planId=@ID", con, transaction);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", txtPackagePlanID.Text.Trim());
                cmd.ExecuteNonQuery();

                SqlCommand cmd1 = new SqlCommand("DELETE FROM paymentMethod WHERE paymentMethodID=@ID", con, transaction);
                cmd1.CommandType = CommandType.Text;
                cmd1.Parameters.AddWithValue("@ID", txtMethodID.Text.Trim());
                cmd1.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand("DELETE FROM customer WHERE customerId=@ID", con, transaction);
                cmd2.CommandType = CommandType.Text;
                cmd2.Parameters.AddWithValue("@ID", txtID.Text.Trim());
                cmd2.ExecuteNonQuery();

                SqlCommand cmd3 = new SqlCommand("spDeleteEmployee", con, transaction);
                cmd3.CommandType = CommandType.StoredProcedure;
                cmd3.Parameters.AddWithValue("@employeeId", txtEmpID.Text.Trim());
                cmd3.ExecuteNonQuery();
                transaction.Commit();
                MessageBox.Show("Data Deleted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                transaction.Rollback();
                MessageBox.Show("Invalid Data");
            }
            finally
            {
                con.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            con.Open();
            SqlTransaction transaction = con.BeginTransaction();
            try
            {
                EnumList.PackagePlanName packagePlanName = (EnumList.PackagePlanName)cmbPackagePlanName.SelectedIndex;
                EnumList.PackageSpeed packageSpeed = (EnumList.PackageSpeed)cmbPackageSpeed.SelectedIndex;
                SqlCommand cmd = new SqlCommand("UPDATE packagePlan SET planName=@PlanName, speed=@Speed, price=@Price WHERE planId=@PlanId", con, transaction);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@PlanId", txtPackagePlanID.Text.Trim());
                cmd.Parameters.AddWithValue("@Price", txtPackagePrice.Text.Trim());
                cmd.Parameters.AddWithValue("@PlanName", packagePlanName.ToString());
                cmd.Parameters.AddWithValue("@Speed", packageSpeed.ToString());
                cmd.ExecuteNonQuery();

                byte[] img = null;
                FileStream fs = new FileStream(imgloc, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                img = br.ReadBytes((int)fs.Length);
                SqlCommand cmd1 = new SqlCommand("UPDATE paymentMethod SET paymentMethodName=@Name, methodImage=@Image WHERE paymentMethodID=@ID", con, transaction);
                cmd1.CommandType = CommandType.Text;
                cmd1.Parameters.AddWithValue("@ID", txtMethodID.Text.Trim());
                cmd1.Parameters.AddWithValue("@Name", txtMethodName.Text.Trim());
                cmd1.Parameters.Add(new SqlParameter("@Image", img));
                cmd1.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand("spUpdateCustomer", con, transaction);
                cmd2.CommandType = CommandType.StoredProcedure;
                cmd2.Parameters.AddWithValue("@customerFirstName", txtFirstName.Text.Trim());
                cmd2.Parameters.AddWithValue("@customerLastName", txtLastName.Text.Trim());
                cmd2.Parameters.AddWithValue("@contactNumber", txtContact.Text.Trim());
                cmd2.Parameters.AddWithValue("@address", txtAddress.Text.Trim());
                cmd2.Parameters.AddWithValue("@postalCode", txtPostalCode.Text.Trim());
                cmd2.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                cmd2.Parameters.AddWithValue("@IPNumber", txtIPNumber.Text.Trim());
                cmd2.Parameters.AddWithValue("@connectionStartDate", dtConnectionStart.Value);
                cmd2.Parameters.AddWithValue("@planId", txtPackageID.Text.Trim());
                cmd2.Parameters.AddWithValue("@paymentMethodID", txtPaymentMethodID.Text.Trim());
                cmd2.ExecuteNonQuery();

                byte[] img1 = null;
                FileStream fs1 = new FileStream(imgloc, FileMode.Open, FileAccess.Read);
                BinaryReader br1 = new BinaryReader(fs1);
                img1 = br1.ReadBytes((int)fs1.Length);
                EnumList.Gender gender = (EnumList.Gender)cmbGender.SelectedIndex;
                SqlCommand cmd3 = new SqlCommand("UPDATE employee SET employeeFirstName=@FirstName, employeeLastName=@LastName, gender=@Gender, contactNumber=@Contact, email=@Email, dateOfBirth=@DateOfBirth, NID=@NID, address=@Address, postalCode=@Postal, employeeImage=@Image, employeeTypeId=@EmployeeType WHERE employeeId=@ID", con, transaction);
                cmd3.CommandType = CommandType.Text;
                cmd3.Parameters.AddWithValue("@FirstName", txtEmpFirstName.Text.Trim());
                cmd3.Parameters.AddWithValue("@LastName", txtEmpLastName.Text.Trim());
                cmd3.Parameters.AddWithValue("@Contact", txtEmpContact.Text.Trim());
                cmd3.Parameters.AddWithValue("@Email", txtEmpEmail.Text.Trim());
                cmd3.Parameters.AddWithValue("@NID", txtEmpNID.Text.Trim());
                cmd3.Parameters.AddWithValue("@Address", txtEmpAddress.Text.Trim());
                cmd3.Parameters.AddWithValue("@Postal", txtEmpPostal.Text.Trim());
                cmd3.Parameters.AddWithValue("@EmployeeType", txtEmpType.Text.Trim());
                cmd3.Parameters.AddWithValue("@ID", txtEmpID.Text.Trim());
                cmd3.Parameters.AddWithValue("@DateOfBirth", dtEmpDOB.Value);
                cmd3.Parameters.AddWithValue("@Gender", gender.ToString());
                cmd3.Parameters.Add(new SqlParameter("@Image", img1));
                cmd3.ExecuteNonQuery();
                transaction.Commit();
                MessageBox.Show("Data Updated Successfully!!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                transaction.Rollback();
                MessageBox.Show("Invalid Data");
            }
            finally
            {
                con.Close();
            }
        }
    }
}
