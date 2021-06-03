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
        private const string Con = @"Data Source=HOWPAGE\PRAVEEN;Initial Catalog=PatientDB;Integrated Security=True";

        public static T ExecuteScalar<T>(string sp, DynamicParameters param)
        {
            using var sqlCon = new SqlConnection(Con);
            sqlCon.Open();
            return (T)Convert.ChangeType(sqlCon.ExecuteScalar(sp, param, commandType: CommandType.StoredProcedure), typeof(T));
        }

        public static void Execute<T>(string sp, DynamicParameters param)
        {
            using var sqlCon = new SqlConnection(Con);
            sqlCon.Open();
            sqlCon.Execute(sp, param, commandType: CommandType.StoredProcedure);
        }

        public static IEnumerable<T> Query<T>(string sp, DynamicParameters param = null)
        {
            using var sqlCon = new SqlConnection(Con);
            sqlCon.Open();
            var result = sqlCon.Query<T>(sp, param, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
