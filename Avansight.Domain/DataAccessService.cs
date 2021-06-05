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
        public static T ExecuteScalar<T>(string sp, DynamicParameters param, string con)
        {
            using var sqlCon = new SqlConnection(con);
            sqlCon.Open();
            return (T)Convert.ChangeType(sqlCon.ExecuteScalar(sp, param, commandType: CommandType.StoredProcedure), typeof(T));
        }

        public static void Execute<T>(string sp, DynamicParameters param, string con)
        {
            using var sqlCon = new SqlConnection(con);
            sqlCon.Open();
            sqlCon.Execute(sp, param, commandType: CommandType.StoredProcedure);
        }

        public static IEnumerable<T> Query<T>(string sp, DynamicParameters param, string con)
        {
            using var sqlCon = new SqlConnection(con);
            sqlCon.Open();
            var result = sqlCon.Query<T>(sp, param, commandType: CommandType.StoredProcedure);
            return result;
        }
    }
}
