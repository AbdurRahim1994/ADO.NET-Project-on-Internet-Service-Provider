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
    public partial class frmUpdateCommandList : Form
    {
        public frmUpdateCommandList()
        {
            InitializeComponent();
        }

        private void btnCustomerTable_Click(object sender, EventArgs e)
        {
            frmUpdateCustomer updateCustomer = new frmUpdateCustomer();
            updateCustomer.Show();
            this.Hide();
        }

        private void btnPackagePlanTable_Click(object sender, EventArgs e)
        {
            frmUpdatePackagePlan updatePackagePlan = new frmUpdatePackagePlan();
            updatePackagePlan.Show();
            this.Hide();
        }

        private void btnPaymentMethodTable_Click(object sender, EventArgs e)
        {
            frmUpdatePaymentMethod updatePaymentMethod = new frmUpdatePaymentMethod();
            updatePaymentMethod.Show();
            this.Hide();
        }

        private void btnHomePage_Click(object sender, EventArgs e)
        {
            frmHome home = new frmHome();
            home.Show();
            this.Hide();
        }

        private void btnEmployeeTable_Click(object sender, EventArgs e)
        {
            frmUpdateEmployee updateEmployee = new frmUpdateEmployee();
            updateEmployee.Show();
            this.Hide();
        }

        private void btnOfficeAddressTable_Click(object sender, EventArgs e)
        {
            frmUpdatetblofficeAddress updatetblofficeAddress = new frmUpdatetblofficeAddress();
            updatetblofficeAddress.Show();
            this.Hide();
        }

        private void btnServiceAreaTable_Click(object sender, EventArgs e)
        {
            frmUpdatetblServiceArea updatetblServiceArea = new frmUpdatetblServiceArea();
            updatetblServiceArea.Show();
            this.Hide();
        }
    }
}
