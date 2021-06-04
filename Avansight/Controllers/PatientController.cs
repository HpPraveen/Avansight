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
        public const string NoOfMalesInAgeGrp1 = "_mAgeGrp1";
        public const string NoOfMalesInAgeGrp2 = "_mAgeGrp2";
        public const string NoOfMalesInAgeGrp3 = "_mAgeGrp3";
        public const string NoOfMalesInAgeGrp4 = "_mAgeGrp4";
        public const string NoOfMalesInAgeGrp5 = "_mAgeGrp5";
        public const string NoOfFemalesInAgeGrp1 = "_fmAgeGrp1";
        public const string NoOfFemalesInAgeGrp2 = "_fmAgeGrp2";
        public const string NoOfFemalesInAgeGrp3 = "_fmAgeGrp3";
        public const string NoOfFemalesInAgeGrp4 = "_fmAgeGrp4";
        public const string NoOfFemalesInAgeGrp5 = "_fmAgeGrp5";


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
            PatientDetailsDto ageGroupData)
        {
            var totalPersonsWeight = Math.Round(maleWeight + femaleWeight);

            var noOfMales = Math.Round(maleWeight / totalPersonsWeight * sampleSize);
            var noOfFemales = Math.Round(femaleWeight / totalPersonsWeight * sampleSize);

            var totalAgeGrpWeight = ageGroupData.NoOfPersonsInAgeGroup1 + ageGroupData.NoOfPersonsInAgeGroup2 +
                                    ageGroupData.NoOfPersonsInAgeGroup3 +
                                    ageGroupData.NoOfPersonsInAgeGroup4 + ageGroupData.NoOfPersonsInAgeGroup5;

            var noOfMalesInAgeGrp1 = Math.Round(ageGroupData.NoOfPersonsInAgeGroup1 / totalAgeGrpWeight * noOfMales);
            var noOfMalesInAgeGrp2 = Math.Round(ageGroupData.NoOfPersonsInAgeGroup2 / totalAgeGrpWeight * noOfMales);
            var noOfMalesInAgeGrp3 = Math.Round(ageGroupData.NoOfPersonsInAgeGroup3 / totalAgeGrpWeight * noOfMales);
            var noOfMalesInAgeGrp4 = Math.Round(ageGroupData.NoOfPersonsInAgeGroup4 / totalAgeGrpWeight * noOfMales);
            var noOfMalesInAgeGrp5 = Math.Round(ageGroupData.NoOfPersonsInAgeGroup5 / totalAgeGrpWeight * noOfMales);

            var noOfFemalesInAgeGrp1 =
                Math.Round(ageGroupData.NoOfPersonsInAgeGroup1 / totalAgeGrpWeight * noOfFemales);
            var noOfFemalesInAgeGrp2 =
                Math.Round(ageGroupData.NoOfPersonsInAgeGroup2 / totalAgeGrpWeight * noOfFemales);
            var noOfFemalesInAgeGrp3 =
                Math.Round(ageGroupData.NoOfPersonsInAgeGroup3 / totalAgeGrpWeight * noOfFemales);
            var noOfFemalesInAgeGrp4 =
                Math.Round(ageGroupData.NoOfPersonsInAgeGroup4 / totalAgeGrpWeight * noOfFemales);
            var noOfFemalesInAgeGrp5 =
                Math.Round(ageGroupData.NoOfPersonsInAgeGroup5 / totalAgeGrpWeight * noOfFemales);

            var noOfPersonsInAgeGrp1 = noOfMalesInAgeGrp1 + noOfFemalesInAgeGrp1;
            var noOfPersonsInAgeGrp2 = noOfMalesInAgeGrp2 + noOfFemalesInAgeGrp2;
            var noOfPersonsInAgeGrp3 = noOfMalesInAgeGrp3 + noOfFemalesInAgeGrp3;
            var noOfPersonsInAgeGrp4 = noOfMalesInAgeGrp4 + noOfFemalesInAgeGrp4;
            var noOfPersonsInAgeGrp5 = noOfMalesInAgeGrp5 + noOfFemalesInAgeGrp5;


            var patientDetails = new PatientDetailsDto
            {
                NoOfPersonsInAgeGroup1 = Convert.ToInt32(noOfPersonsInAgeGrp1),
                NoOfPersonsInAgeGroup2 = Convert.ToInt32(noOfPersonsInAgeGrp2),
                NoOfPersonsInAgeGroup3 = Convert.ToInt32(noOfPersonsInAgeGrp3),
                NoOfPersonsInAgeGroup4 = Convert.ToInt32(noOfPersonsInAgeGrp4),
                NoOfPersonsInAgeGroup5 = Convert.ToInt32(noOfPersonsInAgeGrp5),
                NoOfMales = Convert.ToInt32(noOfMales),
                NoOfFemales = Convert.ToInt32(noOfFemales)
            };


            HttpContext.Session.SetInt32(NoOfMalesInAgeGrp1, Convert.ToInt32(noOfMalesInAgeGrp1));
            HttpContext.Session.SetInt32(NoOfMalesInAgeGrp2, Convert.ToInt32(noOfMalesInAgeGrp2));
            HttpContext.Session.SetInt32(NoOfMalesInAgeGrp3, Convert.ToInt32(noOfMalesInAgeGrp3));
            HttpContext.Session.SetInt32(NoOfMalesInAgeGrp4, Convert.ToInt32(noOfMalesInAgeGrp4));
            HttpContext.Session.SetInt32(NoOfMalesInAgeGrp5, Convert.ToInt32(noOfMalesInAgeGrp5));

            HttpContext.Session.SetInt32(NoOfFemalesInAgeGrp1, Convert.ToInt32(noOfFemalesInAgeGrp1));
            HttpContext.Session.SetInt32(NoOfFemalesInAgeGrp2, Convert.ToInt32(noOfFemalesInAgeGrp2));
            HttpContext.Session.SetInt32(NoOfFemalesInAgeGrp3, Convert.ToInt32(noOfFemalesInAgeGrp3));
            HttpContext.Session.SetInt32(NoOfFemalesInAgeGrp4, Convert.ToInt32(noOfFemalesInAgeGrp4));
            HttpContext.Session.SetInt32(NoOfFemalesInAgeGrp5, Convert.ToInt32(noOfFemalesInAgeGrp5));


            return Ok(patientDetails);

            //return PartialView("_SimulatePatient", ageGroup);
        }

        [HttpPost]
        public ActionResult SavePatientData()
        {
            var c = HttpContext.Session.GetInt32(NoOfMalesInAgeGrp1) ?? default(int);
            var ce = HttpContext.Session.GetInt32(NoOfMalesInAgeGrp2) ?? default(int);
            var rndMaleAgeForAgeGrp1 =
                RandomAgeGenerator(21, 30, HttpContext.Session.GetInt32(NoOfMalesInAgeGrp1) ?? default(int));
            var rndMaleAgeForAgeGrp2 =
                RandomAgeGenerator(31, 40, HttpContext.Session.GetInt32(NoOfMalesInAgeGrp2) ?? default(int));
            var rndMaleAgeForAgeGrp3 =
                RandomAgeGenerator(41, 50, HttpContext.Session.GetInt32(NoOfMalesInAgeGrp3) ?? default(int));
            var rndMaleAgeForAgeGrp4 =
                RandomAgeGenerator(51, 60, HttpContext.Session.GetInt32(NoOfMalesInAgeGrp4) ?? default(int));
            var rndMaleAgeForAgeGrp5 =
                RandomAgeGenerator(61, 70, HttpContext.Session.GetInt32(NoOfMalesInAgeGrp5) ?? default(int));

            var rndFemaleAgeForAgeGrp1 =
                RandomAgeGenerator(21, 30, HttpContext.Session.GetInt32(NoOfFemalesInAgeGrp1) ?? default(int));
            var rndFemaleAgeForAgeGrp2 =
                RandomAgeGenerator(31, 40, HttpContext.Session.GetInt32(NoOfFemalesInAgeGrp2) ?? default(int));
            var rndFemaleAgeForAgeGrp3 =
                RandomAgeGenerator(41, 50, HttpContext.Session.GetInt32(NoOfFemalesInAgeGrp3) ?? default(int));
            var rndFemaleAgeForAgeGrp4 =
                RandomAgeGenerator(51, 60, HttpContext.Session.GetInt32(NoOfFemalesInAgeGrp4) ?? default(int));
            var rndFemaleAgeForAgeGrp5 =
                RandomAgeGenerator(61, 70, HttpContext.Session.GetInt32(NoOfFemalesInAgeGrp5) ?? default(int));

            var dt = new DataTable();
            var dr = dt.NewRow();

            dt.Columns.Add("PatientId", typeof(int));
            dt.Columns.Add("Age", typeof(int));
            dt.Columns.Add("Gender", typeof(string));

            #region Male Patients

            foreach (var rndAge in rndMaleAgeForAgeGrp1)
            {
                dr["PatientId"] = 100;
                dr["Age"] = rndAge;
                dr["Gender"] = "Male";
                dt.Rows.Add(dr.ItemArray);
            }

            foreach (var rndAge in rndMaleAgeForAgeGrp2)
            {
                dr["PatientId"] = 200;
                dr["Age"] = rndAge;
                dr["Gender"] = "Male";
                dt.Rows.Add(dr.ItemArray);
            }

            foreach (var rndAge in rndMaleAgeForAgeGrp3)
            {
                dr["PatientId"] = 300;
                dr["Age"] = rndAge;
                dr["Gender"] = "Male";
                dt.Rows.Add(dr.ItemArray);
            }

            foreach (var rndAge in rndMaleAgeForAgeGrp4)
            {
                dr["PatientId"] = 400;
                dr["Age"] = rndAge;
                dr["Gender"] = "Male";
                dt.Rows.Add(dr.ItemArray);
            }

            foreach (var rndAge in rndMaleAgeForAgeGrp5)
            {
                dr["PatientId"] = 500;
                dr["Age"] = rndAge;
                dr["Gender"] = "Male";
                dt.Rows.Add(dr.ItemArray);
            }

            #endregion


            #region Female Patients

            foreach (var rndAge in rndFemaleAgeForAgeGrp1)
            {
                dr["PatientId"] = 600;
                dr["Age"] = rndAge;
                dr["Gender"] = "Female";
                dt.Rows.Add(dr.ItemArray);
            }

            foreach (var rndAge in rndFemaleAgeForAgeGrp2)
            {
                dr["PatientId"] = 700;
                dr["Age"] = rndAge;
                dr["Gender"] = "Female";
                dt.Rows.Add(dr.ItemArray);
            }

            foreach (var rndAge in rndFemaleAgeForAgeGrp3)
            {
                dr["PatientId"] = 800;
                dr["Age"] = rndAge;
                dr["Gender"] = "Female";
                dt.Rows.Add(dr.ItemArray);
            }

            foreach (var rndAge in rndFemaleAgeForAgeGrp4)
            {
                dr["PatientId"] = 900;
                dr["Age"] = rndAge;
                dr["Gender"] = "Female";
                dt.Rows.Add(dr.ItemArray);
            }

            foreach (var rndAge in rndFemaleAgeForAgeGrp5)
            {
                dr["PatientId"] = 1000;
                dr["Age"] = rndAge;
                dr["Gender"] = "Female";
                dt.Rows.Add(dr.ItemArray);
            }

            #endregion

            //var patientId = 0;
            //var getPatients = _patientService.GetPatients();
            //if (getPatients.Any())
            //    patientId = getPatients.LastOrDefault().PatientId + 1;
            //else
            //    patientId++;

            _patientService.AddPatients(dt);
            return Ok("OK");
        }

        public List<int> RandomAgeGenerator(int minAge, int maxAge, int rndAgeCount)
        {
            var rnd = new Random();
            var intArr = new int[rndAgeCount];
            var ageList = new List<int>();

            for (var i = 0; i < intArr.Length; i++)
            {
                var num = rnd.Next(minAge, maxAge);
                intArr[i] = num;
                ageList.Add(num);
            }

            return ageList;
        }
    }
}