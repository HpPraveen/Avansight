using Avansight.Business_Layer;
using Avansight.Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Avansight.Controllers
{
    public class PatientController : Controller
    {
        public const string SessionPatientDetails = "_patientDetails";

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
        public ActionResult GetPatientData(decimal sampleSize, decimal maleWeight, decimal femaleWeight,
            PatientDetailsDto patientDetails)
        {
            var patientDetailsDto =
                _patientService.CalculatePatientDetails(sampleSize, maleWeight, femaleWeight, patientDetails);

            #region Add age-group data to session

            if (HttpContext.Session.Get<PatientDetailsDto>(SessionPatientDetails) == default)
                HttpContext.Session.Set(SessionPatientDetails, patientDetailsDto);

            #endregion

            return Ok(patientDetailsDto);
        }

        [HttpPost]
        public ActionResult SavePatientData()
        {
            #region Get age-group session data

            var sessionPatientDetails = HttpContext.Session.Get<PatientDetailsDto>(SessionPatientDetails);

            #endregion

           var insertedPatientList = _patientService.AddPatients(sessionPatientDetails);

            return PartialView("_SimulatePatient", insertedPatientList);
        }
    }
}