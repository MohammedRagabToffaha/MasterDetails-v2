using CustomersMasterDetails.BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomersMasterDetails.PresentationLayer
{
    public partial class AddEditInvoice : Form
    {
        DataGridView dgvDetails;
        int CustomerId;
        string InvoiceID;
        string InvoiceAmount;
        public AddEditInvoice(DataGridView dgvD,int CustId,string invoiceId="", string invoiceAmount="")
        {
            InitializeComponent();
            dgvDetails = dgvD;
            CustomerId = CustId;
            InvoiceID = invoiceId;
            InvoiceAmount = invoiceAmount;

        }
        private void AddEditInvoice_Load(object sender, EventArgs e)
        {
            txtCustomerId.Text = CustomerId.ToString();
            if (InvoiceID!="")
            {
                btnAddInvoice.Text = "Update Invoice";
                txtInvoiceAmount.Text = InvoiceAmount.ToString();
            }
        }
        private void btnAddInvoice_Click(object sender, EventArgs e)
        {
            if (txtInvoiceAmount.Text=="")
            {
                MessageBox.Show("أدخل ثمن الفاتوره");
                return;
            }
            double result = 0;
            if (!double.TryParse(txtInvoiceAmount.Text, out result))
            {
                MessageBox.Show("أدخل ثمن الفاتوره ارقام وليس حروف");
                return;
            }
            if (btnAddInvoice.Text== "Add Invoice")
            {
                int i = Invoices.CreateNewInvoice(CustomerId, Convert.ToDecimal(txtInvoiceAmount.Text));
                if (i > 0)
                {
                    MessageBox.Show("تم اضافة الفاتوره بنجاح");
                    SelectAllInvoices();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("حدثت مشكله اثناء عملية الاضافه حاول مره اخرى");
                }
            }
            else if (btnAddInvoice.Text== "Update Invoice")
            {
                int i = Invoices.UpdateInvoice(CustomerId, Convert.ToDecimal(txtInvoiceAmount.Text),Convert.ToInt32(InvoiceID));
                if (i > 0)
                {
                    MessageBox.Show("تم تعديل الفاتوره بنجاح");
                    SelectAllInvoices();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("حدثت مشكله اثناء عملية التعديل حاول مره اخرى");
                }
            }

        }

        private void SelectAllInvoices()
        {
            DataTable dt = Invoices.SelectInvoicesToCustomer(CustomerId);
            DataTable dt1 = AutoNumberedTable(dt);
            dgvDetails.DataSource = dt1;
            dgvDetails.Columns[1].Visible = false;
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
