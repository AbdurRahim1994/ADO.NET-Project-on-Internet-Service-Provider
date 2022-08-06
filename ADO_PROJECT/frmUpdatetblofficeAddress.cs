﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ADO_PROJECT
{
    public partial class frmUpdatetblofficeAddress : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=D:\XML Practise File\ADO.NET\ISPDB(Internet Service Provider Database)\ADO_PROJECT\ADOPROJECTDB(DISCONNECTED DATABASE).mdf;Integrated Security=True");
        public frmUpdatetblofficeAddress()
        {
            InitializeComponent();
        }
        private void LoadGrid()
        {
            SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM tblofficeAddress", con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            grdOfficeAddress.DataSource = dt;
        }

        private void frmUpdatetblofficeAddress_Load(object sender, EventArgs e)
        {
            LoadGrid();
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
            try
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tblofficeAddress WHERE officeAddressId=@ID", con);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ID", txtOfficeAddressID.Text);
                con.Open();
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtOfficeAddressName.Text = dr[1].ToString();
                }
                else
                {
                    MessageBox.Show("No data found", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
                dr.Close();
                
            }
            catch
            {
                MessageBox.Show("ID must be number", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            finally
            {
                con.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtOfficeAddressName.Text != "")
                {
                    SqlCommand cmd = new SqlCommand("UPDATE tblofficeAddress SET officeAddressName=@Name WHERE officeAddressId=@ID", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Name", txtOfficeAddressName.Text.Trim());
                    cmd.Parameters.AddWithValue("@ID", txtOfficeAddressID.Text);

                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Office Address Successfully Updated", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadGrid();
                    txtOfficeAddressID.Clear();
                    txtOfficeAddressName.Clear();
                    con.Close();
                }
                else
                {
                    MessageBox.Show("Enter Office Address Name", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                }
                
            }
            catch
            {
                MessageBox.Show("Office Address must be character", "Failed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
        }

        
    }
}
