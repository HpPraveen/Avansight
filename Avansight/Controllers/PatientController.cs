using Avansight.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Avansight.Business_Layer;
using Avansight.Domain.DTO;

namespace Avansight.Controllers
{
    public class PatientController : Controller
    {
        private readonly PatientService _patientService;

        public PatientController(PatientService patientService)
        {
            _patientService = patientService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetPatientData(decimal sampleSize, decimal maleWeight, decimal femaleWeight, AgeGroupData ageGroupData)
        {
            var noOfMales = maleWeight / (maleWeight + femaleWeight) * sampleSize;
            var noOfFemales = femaleWeight / (maleWeight + femaleWeight) * sampleSize;

            var dt = new DataTable();

            dt.Columns.Add("PatientId", typeof(int));
            dt.Columns.Add("Age", typeof(int));
            dt.Columns.Add("Gender", typeof(string));

            var patientId = 0;
            var getPatients = _patientService.GetPatients();
            if (getPatients.Any())
                patientId = getPatients.LastOrDefault().PatientId + 1;
            else
                patientId++;

            var dr = dt.NewRow();

            dr["PatientId"] = patientId;
            dr["Age"] = "24";
            dr["Gender"] = "Female";

            dt.Rows.Add(dr);

            _patientService.AddPatients(dt);

            return Json(new {getPatients});
        }

        public PartialViewResult PatientData()
        {
            return PartialView("_SimulatePatient");
        }
    }
}
