using CustomersMasterDetails.BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomersMasterDetails.PresentationLayer
{
    public partial class AddEditCustomer : Form
    {
        DataGridView dgvCustomer;
        string CustomerID;
        string Name;
        string Address;
        string Email;
        string Mobile;
        public AddEditCustomer(DataGridView dgvC,string CustId="",string name="",string address="",string email="",string mobile="")
        {
            InitializeComponent();
            dgvCustomer = dgvC;
            CustomerID = CustId;
            Name = name;
            Address = address;
            Email = email;
            Mobile = mobile;
        }
        
        private void AddEditCustomer_Load(object sender, EventArgs e)
        {
            if (CustomerID != "")
            {
                btnAdd.Text = "Update";
                txtName.Text = Name;
                txtAddress.Text = Address;
                txtEmail.Text = Email;
                txtMobile.Text = Mobile;
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            #region validate textboxs
            if (txtName.Text == string.Empty)
            {
                MessageBox.Show("ادخل اسم العميل");
                return;
            }
            int result = 0;
            if (txtMobile.Text!="")
            {
                if (!int.TryParse(txtMobile.Text, out result))
                {
                    MessageBox.Show("أدخل رقم الموبايل ارقام وليس حروف");
                    return;
                }
            }
            if (txtName.Text.Length > 100 || txtAddress.Text.Length > 100)
            {
                MessageBox.Show("ادخل عدد الاحرف اقل من 100");
                return;
            }
            if (txtEmail.Text != "")
            {
                if (!Regex.IsMatch(txtEmail.Text, @"^[a-z,A-Z]{1,10}((-|.)\w+)*@\w+.\w{3}$"))
                {
                    MessageBox.Show("ادخل ايميل صحيح");
                    return;
                }
            }
            #endregion
            #region Add and Edit
            if (btnAdd.Text=="Add")
            {
                int i = Customers.CreateNewCustomer(txtName.Text, txtAddress.Text, txtEmail.Text, txtMobile.Text);
                if (i > 0)
                {
                    MessageBox.Show("تم اضافة العميل بنجاح");
                    SelectAllCustomer();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("حدثت مشكله اثناء عملية الاضافه حاول مره اخرى");
                }
              
            }
            else if (btnAdd.Text== "Update")
            {
                int id = Convert.ToInt32(CustomerID);
                int i = Customers.UpdateCustomer(id, txtName.Text, txtAddress.Text, txtEmail.Text, txtMobile.Text);
                if (i>0)
                {
                    MessageBox.Show("تم التعديل بنجاح");
                    SelectAllCustomer();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("حدثت مشكله اثناء عملية التعديل حاول مره اخرى");
                }
            }
            #endregion
        }
        public void SelectAllCustomer()
        {
            DataTable dt = Customers.SelectAllCustomers();
            DataTable dt1 = AutoNumberedTable(dt);
            dgvCustomer.DataSource = dt1;
            dgvCustomer.Columns[1].Visible = false;
        }
        private DataTable AutoNumberedTable(DataTable SourceTable)

        {

            DataTable ResultTable = new DataTable();

            DataColumn AutoNumberColumn = new DataColumn();

            AutoNumberColumn.ColumnName = "No.";

            AutoNumberColumn.DataType = typeof(int);

            AutoNumberColumn.AutoIncrement = true;

            AutoNumberColumn.AutoIncrementSeed = 1;

            AutoNumberColumn.AutoIncrementStep = 1;

            ResultTable.Columns.Add(AutoNumberColumn);

            ResultTable.Merge(SourceTable);

            return ResultTable;

        }
    }
}
