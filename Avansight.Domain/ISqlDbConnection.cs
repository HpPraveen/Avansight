using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Avansight.Domain.Models
{
    public class ISqlDbConnection
    {
        public DbConnection Connection { get; }
    }
}
