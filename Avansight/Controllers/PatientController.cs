using System;
using System.Collections.Generic;
using System.Data;
using Avansight.Business_Layer;
using Avansight.Domain.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Avansight.Controllers
{
    public class PatientController : Controller
    {
        #region

        public const string SessionMalesAgeGrp1 = "_mAgeGrp1";
        public const string SessionMalesAgeGrp2 = "_mAgeGrp2";
        public const string SessionMalesAgeGrp3 = "_mAgeGrp3";
        public const string SessionMalesAgeGrp4 = "_mAgeGrp4";
        public const string SessionMalesAgeGrp5 = "_mAgeGrp5";
        public const string SessionFemalesAgeGrp1 = "_fmAgeGrp1";
        public const string SessionFemalesAgeGrp2 = "_fmAgeGrp2";
        public const string SessionFemalesAgeGrp3 = "_fmAgeGrp3";
        public const string SessionFemalesAgeGrp4 = "_fmAgeGrp4";
        public const string SessionFemalesAgeGrp5 = "_fmAgeGrp5";

        #endregion

        private readonly PatientService _patientService;
        private readonly RandomAgeGeneratorService _randomAgeGeneratorService;

        public PatientController(PatientService patientService, RandomAgeGeneratorService randomAgeGeneratorService)
        {
            _patientService = patientService;
            _randomAgeGeneratorService = randomAgeGeneratorService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetPatientData(decimal sampleSize, decimal maleWeight, decimal femaleWeight, PatientDetailsDto patientDetails)
        {
            var patientDetailsDto = _patientService.CalculatePatientDetails(sampleSize, maleWeight, femaleWeight, patientDetails);
              
            #region  Add age-group data to session

            HttpContext.Session.SetInt32(SessionMalesAgeGrp1, Convert.ToInt32(patientDetailsDto.NoOfMalesInAgeGroup1));
            HttpContext.Session.SetInt32(SessionMalesAgeGrp2, Convert.ToInt32(patientDetailsDto.NoOfMalesInAgeGroup2));
            HttpContext.Session.SetInt32(SessionMalesAgeGrp3, Convert.ToInt32(patientDetailsDto.NoOfMalesInAgeGroup3));
            HttpContext.Session.SetInt32(SessionMalesAgeGrp4, Convert.ToInt32(patientDetailsDto.NoOfMalesInAgeGroup4));
            HttpContext.Session.SetInt32(SessionMalesAgeGrp5, Convert.ToInt32(patientDetailsDto.NoOfMalesInAgeGroup5));
                                         
            HttpContext.Session.SetInt32(SessionFemalesAgeGrp1, Convert.ToInt32(patientDetailsDto.NoOfFemalesInAgeGroup1));
            HttpContext.Session.SetInt32(SessionFemalesAgeGrp2, Convert.ToInt32(patientDetailsDto.NoOfFemalesInAgeGroup2));
            HttpContext.Session.SetInt32(SessionFemalesAgeGrp3, Convert.ToInt32(patientDetailsDto.NoOfFemalesInAgeGroup3));
            HttpContext.Session.SetInt32(SessionFemalesAgeGrp4, Convert.ToInt32(patientDetailsDto.NoOfFemalesInAgeGroup4));
            HttpContext.Session.SetInt32(SessionFemalesAgeGrp5, Convert.ToInt32(patientDetailsDto.NoOfFemalesInAgeGroup5));

            #endregion

            return Ok(patientDetailsDto);
        }

        [HttpPost]
        public ActionResult SavePatientData()
        {
            #region Get age-group session data

            var noOfMalesInAgeGrp1 = HttpContext.Session.GetInt32(SessionMalesAgeGrp1) ?? default(int);
            var noOfMalesInAgeGrp2 = HttpContext.Session.GetInt32(SessionMalesAgeGrp2) ?? default(int);
            var noOfMalesInAgeGrp3 = HttpContext.Session.GetInt32(SessionMalesAgeGrp3) ?? default(int);
            var noOfMalesInAgeGrp4 = HttpContext.Session.GetInt32(SessionMalesAgeGrp4) ?? default(int);
            var noOfMalesInAgeGrp5 = HttpContext.Session.GetInt32(SessionMalesAgeGrp5) ?? default(int);

            var noOfFemalesInAgeGrp1 = HttpContext.Session.GetInt32(SessionFemalesAgeGrp1) ?? default(int);
            var noOfFemalesInAgeGrp2 = HttpContext.Session.GetInt32(SessionFemalesAgeGrp2) ?? default(int);
            var noOfFemalesInAgeGrp3 = HttpContext.Session.GetInt32(SessionFemalesAgeGrp3) ?? default(int);
            var noOfFemalesInAgeGrp4 = HttpContext.Session.GetInt32(SessionFemalesAgeGrp4) ?? default(int);
            var noOfFemalesInAgeGrp5 = HttpContext.Session.GetInt32(SessionFemalesAgeGrp5) ?? default(int);

            #endregion

            _patientService.AddPatients(noOfMalesInAgeGrp1, noOfMalesInAgeGrp2, noOfMalesInAgeGrp3, noOfMalesInAgeGrp4, noOfMalesInAgeGrp5,
                noOfFemalesInAgeGrp1, noOfFemalesInAgeGrp2, noOfFemalesInAgeGrp3, noOfFemalesInAgeGrp4, noOfFemalesInAgeGrp5);

            return PartialView("_SimulatePatient", _patientService.GetPatients());
        }

    }
}