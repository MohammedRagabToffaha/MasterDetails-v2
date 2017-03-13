using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CustomersMasterDetails.DAL;
using System.Data;

namespace CustomersMasterDetails.BusinessLayer
{
    class Customers
    {
        public static int CreateNewCustomer(string Name,string Address,string Email,string Mobile)
        {
            DataLayer.open();
            int i = DataLayer.excuteNonQuery("CreateNewCustomer", CommandType.StoredProcedure,
                DataLayer.createParams("@Name", SqlDbType.NVarChar, Name),
                DataLayer.createParams("@Address", SqlDbType.NVarChar, Address),
                DataLayer.createParams("@Email", SqlDbType.NVarChar, Email),
                DataLayer.createParams("@Mobile", SqlDbType.NVarChar, Mobile));
            DataLayer.close();
            return i;
        }
       
        public static DataTable SelectAllCustomers()
        {
            DataLayer.open();
            DataTable dt = DataLayer.excuteTable("SelectAllCustomers", CommandType.StoredProcedure);
            DataLayer.close();
            return dt;
        }
        public static int UpdateCustomer(int id,string Name, string Address, string Email, string Mobile)
        {
            DataLayer.open();
            int i = DataLayer.excuteNonQuery("UpdateCustomer", CommandType.StoredProcedure,
                DataLayer.createParams("@CustID", SqlDbType.Int, id),
                DataLayer.createParams("@Name", SqlDbType.NVarChar, Name),
                DataLayer.createParams("@Address", SqlDbType.NVarChar, Address),
                DataLayer.createParams("@Email", SqlDbType.NVarChar, Email),
                DataLayer.createParams("@Mobile", SqlDbType.NVarChar, Mobile));
            DataLayer.close();
            return i;
        }
        public static int DeleteCustomer(int c_id)
        {
            DataLayer.open();
            int i = DataLayer.excuteNonQuery("DeleteCustomer",CommandType.StoredProcedure,
                DataLayer.createParams("@CustID", SqlDbType.Int, c_id));
            DataLayer.close();
            return i;
        }
    }
}
