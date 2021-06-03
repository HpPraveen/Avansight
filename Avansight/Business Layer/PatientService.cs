using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Avansight.Domain;

namespace Avansight.Business_Layer
{
    public class PatientService
    {
        public IEnumerable<Patient> GetPatients ()
        {
            var result = DataAccessService.Query<Patient>("PatientGet");
            return result.ToList();
        }

        public void AddPatients()
        { 
            DataAccessService.Execute<Patient>("PatientGet",null);
       
        }
    }
}
