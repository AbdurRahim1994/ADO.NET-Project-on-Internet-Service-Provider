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
    public partial class frmInsertCommandList : Form
    {
        public frmInsertCommandList()
        {
            InitializeComponent();
        }

        private void btnCustomerTable_Click(object sender, EventArgs e)
        {
            frmAddCustomer addCustomer = new frmAddCustomer();
            addCustomer.Show();
            this.Hide();
        }

        private void btnPackagePlanTable_Click(object sender, EventArgs e)
        {
            frmAddPackagePlan addPackagePlan = new frmAddPackagePlan();
            addPackagePlan.Show();
            this.Hide();
        }

        private void btnPaymentMethodTable_Click(object sender, EventArgs e)
        {
            frmAddPaymentMethod addPaymentMethod = new frmAddPaymentMethod();
            addPaymentMethod.Show();
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
            frmAddEmployee addEmployee = new frmAddEmployee();
            addEmployee.Show();
            this.Hide();
        }

        private void btnOfficeAddressTable_Click(object sender, EventArgs e)
        {
            frmAddtblofficeAddress addtblofficeAddress = new frmAddtblofficeAddress();
            addtblofficeAddress.Show();
            this.Hide();
        }

        private void btnServiceAreaTable_Click(object sender, EventArgs e)
        {
            frmAddtblServiceArea addtblServiceArea = new frmAddtblServiceArea();
            addtblServiceArea.Show();
            this.Hide();
        }
    }
}
