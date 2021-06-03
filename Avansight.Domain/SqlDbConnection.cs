using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using Avansight.Domain.Models;

namespace Avansight.Domain
{
    public class SqlDbConnection

    {
        private DbConnection _connection;
        public DbConnection Connection 
        {
            get
            {
                if (_connection != null)
                {
                    _connection = new SqlConnection("Data Source=HOWPAGE\\PRAVEEN;Initial Catalog=PatientDB;Integrated Security=True");
                    _connection.Open();
                }
                else if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }

                return _connection;
            } 
        }
  
    }
}
