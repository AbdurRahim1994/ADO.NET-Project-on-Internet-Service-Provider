using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADO_PROJECT
{
    public partial class frmDeleteCommandList : Form
    {
        public frmDeleteCommandList()
        {
            InitializeComponent();
        }

        private void btnCustomerTable_Click(object sender, EventArgs e)
        {
            frmDeleteCustomer deleteCustomer = new frmDeleteCustomer();
            deleteCustomer.Show();
            this.Hide();
        }

        private void btnEmployeeTable_Click(object sender, EventArgs e)
        {
            frmDeleteEmployee deleteEmployee = new frmDeleteEmployee();
            deleteEmployee.Show();
            this.Hide();
        }

        private void btnPackagePlanTable_Click(object sender, EventArgs e)
        {
            frmDeletePackagePlan deletePackagePlan = new frmDeletePackagePlan();
            deletePackagePlan.Show();
            this.Hide();

        }

        private void btnPaymentMethodTable_Click(object sender, EventArgs e)
        {
            frmDeletePaymentMethod deletePaymentMethod = new frmDeletePaymentMethod();
            deletePaymentMethod.Show();
            this.Hide();
        }

        private void btnHomePage_Click(object sender, EventArgs e)
        {
            frmHome home = new frmHome();
            home.Show();
            this.Hide();
        }

        private void btnOfficeAddressTable_Click(object sender, EventArgs e)
        {
            frmDeletetblofficeAddress deletetblofficeAddress = new frmDeletetblofficeAddress();
            deletetblofficeAddress.Show();
            this.Hide();
        }

        private void btnServiceAreaTable_Click(object sender, EventArgs e)
        {
            frmDeletetblServiceArea deletetblServiceArea = new frmDeletetblServiceArea();
            deletetblServiceArea.Show();
            this.Hide();
        }
    }
}
