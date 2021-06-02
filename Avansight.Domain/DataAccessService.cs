using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Avansight.Domain
{
    public class DataAccessService
    {
        private static string con = @"Data Source=HOWPAGE\PRAVEEN;Initial Catalog=PatientDB;Integrated Security=True";
            
        public static T ExecuteScalar<T>(string sp, DynamicParameters parm)
        {
            using SqlConnection sqlCon = new SqlConnection(con);
            sqlCon.Open();
            return (T)Convert.ChangeType(sqlCon.ExecuteScalar(sp, parm, commandType: CommandType.StoredProcedure), typeof(T));
        }

        public static void Execute<T>(string sp, DynamicParameters parm)
        {
            using SqlConnection sqlCon = new SqlConnection(con);
            sqlCon.Open();
            sqlCon.Execute(sp, parm, commandType: CommandType.StoredProcedure);
        }

        public static IEnumerable<T> Query<T>(string sp, DynamicParameters parm = null)
        {
            using SqlConnection sqlCon = new SqlConnection(con);
            sqlCon.Open();
            var result = sqlCon.Query<T>(sp, parm, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
