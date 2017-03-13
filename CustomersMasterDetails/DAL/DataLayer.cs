using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomersMasterDetails.DAL
{
    class DataLayer
    {
        public static string con = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\MaterDetailsDB.mdf;Integrated Security=True";

        public static SqlConnection cn;
        public static void open()
        {

            cn = new SqlConnection(con);
            cn.Open();
        }
        public static void close()
        {
            cn = new SqlConnection(con);
            cn.Close();
        }
        public static int excuteNonQuery(string query,CommandType type, params SqlParameter[] arr)
        {
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.CommandType = type;
            cmd.Parameters.AddRange(arr);
            int i = cmd.ExecuteNonQuery();
            return i;
        }
        public static object excuteScaler(string query, CommandType type, params SqlParameter[] arr)
        {
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.CommandType = type;
            cmd.Parameters.AddRange(arr);
            object o = cmd.ExecuteScalar();
            return o;
        }

        public static DataTable excuteTable(string query, CommandType type, params SqlParameter[] arr)
        {
            SqlCommand cmd = new SqlCommand(query, cn);
            cmd.CommandType = type;
            cmd.Parameters.AddRange(arr);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        public static SqlParameter createParams(string name, SqlDbType type, object value)
        {
            SqlParameter pr = new SqlParameter();
            pr.ParameterName = name;
            pr.SqlDbType = type;
            pr.SqlValue = value;
            return pr;
        }
    }
}
