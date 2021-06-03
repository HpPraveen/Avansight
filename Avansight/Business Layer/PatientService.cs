using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Avansight.Domain;
using Dapper;

namespace Avansight.Business_Layer
{
    public class PatientService
    {
        public IEnumerable<Patient> GetPatients ()
        {
            var result = DataAccessService.Query<Patient>("PatientGet");
            return result.ToList();
        }

        public void AddPatients(DataTable dt)
        {
            var param = new DynamicParameters();
            param.Add("@Patients", dt.AsTableValuedParameter("[dbo].[PatientTableType]"));
            DataAccessService.Execute<Patient>("PatientSet", param);
       
        }
    }
}
