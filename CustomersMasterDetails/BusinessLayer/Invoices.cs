using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomersMasterDetails.DAL;
using System.Data;

namespace CustomersMasterDetails.BusinessLayer
{
    class Invoices
    {
        public static int CreateNewInvoice(int CustomerId,decimal Amount)
        {
            DataLayer.open();
            int i = DataLayer.excuteNonQuery("AddNewInvoice", CommandType.StoredProcedure,
                DataLayer.createParams("@custId", SqlDbType.Int, CustomerId),
                DataLayer.createParams("@amount", SqlDbType.Decimal, Amount));
            DataLayer.close();
            return i;
        }
        public static DataTable SelectInvoicesToCustomer( int customerId)
        {
            DataLayer.open();
            DataTable dt = DataLayer.excuteTable("SelectInvoices", CommandType.StoredProcedure,
                DataLayer.createParams("@custId", SqlDbType.Int, customerId));
            DataLayer.close();
            return dt;
        }
        public static int UpdateInvoice(int CustomerId, decimal Amount,int InvoiceID)
        {
            DataLayer.open();
            int i = DataLayer.excuteNonQuery("EditInvoice", CommandType.StoredProcedure,
                DataLayer.createParams("@CustumerID", SqlDbType.Int, CustomerId),
                DataLayer.createParams("@mount", SqlDbType.Decimal, Amount),
                DataLayer.createParams("@invoiceID", SqlDbType.Decimal, InvoiceID));
            DataLayer.close();
            return i;
        }
        public static int DeleteInvoice(int invoiceID)
        {
            DataLayer.open();
            int i = DataLayer.excuteNonQuery("RemoveInvoice", CommandType.StoredProcedure,
                DataLayer.createParams("@invoiceId", SqlDbType.Int, invoiceID));
            DataLayer.close();
            return i;
        }
    }
}
