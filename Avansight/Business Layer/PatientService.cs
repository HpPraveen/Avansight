using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Avansight.Domain;
using Avansight.Domain.DTO;
using Dapper;
using Microsoft.AspNetCore.Http;

namespace Avansight.Business_Layer
{
    public class PatientService
    {
        private readonly RandomAgeGeneratorService _randomAgeGeneratorService;

        public PatientService(RandomAgeGeneratorService randomAgeGeneratorService)
        { 
            _randomAgeGeneratorService = randomAgeGeneratorService;
        }

        public IEnumerable<Patient> GetPatients ()
        {
            var result = DataAccessService.Query<Patient>("PatientGet");
            return result.ToList();
        }

        public PatientDetailsDto CalculatePatientDetails(decimal sampleSize, decimal maleWeight, decimal femaleWeight, PatientDetailsDto patientDetails)
        {
            #region Logic

            var totalPersonsWeight = Math.Round(maleWeight + femaleWeight);

            var noOfMales = Math.Round(maleWeight / totalPersonsWeight * sampleSize);
            var noOfFemales = Math.Round(femaleWeight / totalPersonsWeight * sampleSize);

            var totalAgeGrpWeight = patientDetails.NoOfPersonsInAgeGroup1 + patientDetails.NoOfPersonsInAgeGroup2 + patientDetails.NoOfPersonsInAgeGroup3 + patientDetails.NoOfPersonsInAgeGroup4 + patientDetails.NoOfPersonsInAgeGroup5;

            var noOfMalesInAgeGrp1 = Math.Round(patientDetails.NoOfPersonsInAgeGroup1 / totalAgeGrpWeight * noOfMales);
            var noOfMalesInAgeGrp2 = Math.Round(patientDetails.NoOfPersonsInAgeGroup2 / totalAgeGrpWeight * noOfMales);
            var noOfMalesInAgeGrp3 = Math.Round(patientDetails.NoOfPersonsInAgeGroup3 / totalAgeGrpWeight * noOfMales);
            var noOfMalesInAgeGrp4 = Math.Round(patientDetails.NoOfPersonsInAgeGroup4 / totalAgeGrpWeight * noOfMales);
            var noOfMalesInAgeGrp5 = Math.Round(patientDetails.NoOfPersonsInAgeGroup5 / totalAgeGrpWeight * noOfMales);

            var noOfFemalesInAgeGrp1 = Math.Round(patientDetails.NoOfPersonsInAgeGroup1 / totalAgeGrpWeight * noOfFemales);
            var noOfFemalesInAgeGrp2 = Math.Round(patientDetails.NoOfPersonsInAgeGroup2 / totalAgeGrpWeight * noOfFemales);
            var noOfFemalesInAgeGrp3 = Math.Round(patientDetails.NoOfPersonsInAgeGroup3 / totalAgeGrpWeight * noOfFemales);
            var noOfFemalesInAgeGrp4 = Math.Round(patientDetails.NoOfPersonsInAgeGroup4 / totalAgeGrpWeight * noOfFemales);
            var noOfFemalesInAgeGrp5 = Math.Round(patientDetails.NoOfPersonsInAgeGroup5 / totalAgeGrpWeight * noOfFemales);

            var noOfPersonsInAgeGrp1 = noOfMalesInAgeGrp1 + noOfFemalesInAgeGrp1;
            var noOfPersonsInAgeGrp2 = noOfMalesInAgeGrp2 + noOfFemalesInAgeGrp2;
            var noOfPersonsInAgeGrp3 = noOfMalesInAgeGrp3 + noOfFemalesInAgeGrp3;
            var noOfPersonsInAgeGrp4 = noOfMalesInAgeGrp4 + noOfFemalesInAgeGrp4;
            var noOfPersonsInAgeGrp5 = noOfMalesInAgeGrp5 + noOfFemalesInAgeGrp5;

            #endregion

            var patientDetailsDto = new PatientDetailsDto
            {
                NoOfMales = Convert.ToInt32(noOfMales),
                NoOfFemales = Convert.ToInt32(noOfFemales),

                NoOfPersonsInAgeGroup1 = Convert.ToInt32(noOfPersonsInAgeGrp1),
                NoOfPersonsInAgeGroup2 = Convert.ToInt32(noOfPersonsInAgeGrp2),
                NoOfPersonsInAgeGroup3 = Convert.ToInt32(noOfPersonsInAgeGrp3),
                NoOfPersonsInAgeGroup4 = Convert.ToInt32(noOfPersonsInAgeGrp4),
                NoOfPersonsInAgeGroup5 = Convert.ToInt32(noOfPersonsInAgeGrp5),

                NoOfMalesInAgeGroup1 = Convert.ToInt32(noOfPersonsInAgeGrp1),
                NoOfMalesInAgeGroup2 = Convert.ToInt32(noOfPersonsInAgeGrp2),
                NoOfMalesInAgeGroup3 = Convert.ToInt32(noOfPersonsInAgeGrp3),
                NoOfMalesInAgeGroup4 = Convert.ToInt32(noOfPersonsInAgeGrp4),
                NoOfMalesInAgeGroup5 = Convert.ToInt32(noOfPersonsInAgeGrp5),

                NoOfFemalesInAgeGroup1 = Convert.ToInt32(noOfPersonsInAgeGrp1),
                NoOfFemalesInAgeGroup2 = Convert.ToInt32(noOfPersonsInAgeGrp2),
                NoOfFemalesInAgeGroup3 = Convert.ToInt32(noOfPersonsInAgeGrp3),
                NoOfFemalesInAgeGroup4 = Convert.ToInt32(noOfPersonsInAgeGrp4),
                NoOfFemalesInAgeGroup5 = Convert.ToInt32(noOfPersonsInAgeGrp5)
            };

            return patientDetailsDto;
        }

        public void AddPatients(int noOfMalesInAgeGrp1, int noOfMalesInAgeGrp2, int noOfMalesInAgeGrp3, 
            int noOfMalesInAgeGrp4, int noOfMalesInAgeGrp5, int noOfFemalesInAgeGrp1, 
            int noOfFemalesInAgeGrp2, int noOfFemalesInAgeGrp3, int noOfFemalesInAgeGrp4, int noOfFemalesInAgeGrp5)
        {

            var rndMaleAgesForAgeGrp1 = _randomAgeGeneratorService.RandomAgeGenerator(21, 30, noOfMalesInAgeGrp1);
            var rndMaleAgesForAgeGrp2 = _randomAgeGeneratorService.RandomAgeGenerator(31, 40, noOfMalesInAgeGrp2);
            var rndMaleAgesForAgeGrp3 = _randomAgeGeneratorService.RandomAgeGenerator(41, 50, noOfMalesInAgeGrp3);
            var rndMaleAgesForAgeGrp4 = _randomAgeGeneratorService.RandomAgeGenerator(51, 60, noOfMalesInAgeGrp4);
            var rndMaleAgesForAgeGrp5 = _randomAgeGeneratorService.RandomAgeGenerator(61, 70, noOfMalesInAgeGrp5);

            var rndFemaleAgesForAgeGrp1 = _randomAgeGeneratorService.RandomAgeGenerator(21, 30, noOfFemalesInAgeGrp1);
            var rndFemaleAgesForAgeGrp2 = _randomAgeGeneratorService.RandomAgeGenerator(31, 40, noOfFemalesInAgeGrp2);
            var rndFemaleAgesForAgeGrp3 = _randomAgeGeneratorService.RandomAgeGenerator(41, 50, noOfFemalesInAgeGrp3);
            var rndFemaleAgesForAgeGrp4 = _randomAgeGeneratorService.RandomAgeGenerator(51, 60, noOfFemalesInAgeGrp4);
            var rndFemaleAgesForAgeGrp5 = _randomAgeGeneratorService.RandomAgeGenerator(61, 70, noOfFemalesInAgeGrp5);

            var dt = new DataTable();
            var dr = dt.NewRow();
            
            dt.Columns.Add("Age", typeof(int));
            dt.Columns.Add("Gender", typeof(string));

            #region Add Male Patients to dt

            foreach (var rndAge in rndMaleAgesForAgeGrp1)
            {
                dr["Age"] = rndAge;
                dr["Gender"] = "Male";
                dt.Rows.Add(dr.ItemArray);
            }

            foreach (var rndAge in rndMaleAgesForAgeGrp2)
            {
                dr["Age"] = rndAge;
                dr["Gender"] = "Male";
                dt.Rows.Add(dr.ItemArray);
            }

            foreach (var rndAge in rndMaleAgesForAgeGrp3)
            {
                dr["Age"] = rndAge;
                dr["Gender"] = "Male";
                dt.Rows.Add(dr.ItemArray);
            }

            foreach (var rndAge in rndMaleAgesForAgeGrp4)
            {
                dr["Age"] = rndAge;
                dr["Gender"] = "Male";
                dt.Rows.Add(dr.ItemArray);
            }

            foreach (var rndAge in rndMaleAgesForAgeGrp5)
            {
                dr["Age"] = rndAge;
                dr["Gender"] = "Male";
                dt.Rows.Add(dr.ItemArray);
            }

            #endregion

            #region Add Female Patients to dt

            foreach (var rndAge in rndFemaleAgesForAgeGrp1)
            {
                dr["Age"] = rndAge;
                dr["Gender"] = "Female";
                dt.Rows.Add(dr.ItemArray);
            }

            foreach (var rndAge in rndFemaleAgesForAgeGrp2)
            {
                dr["Age"] = rndAge;
                dr["Gender"] = "Female";
                dt.Rows.Add(dr.ItemArray);
            }

            foreach (var rndAge in rndFemaleAgesForAgeGrp3)
            {
                dr["Age"] = rndAge;
                dr["Gender"] = "Female";
                dt.Rows.Add(dr.ItemArray);
            }

            foreach (var rndAge in rndFemaleAgesForAgeGrp4)
            {
                dr["Age"] = rndAge;
                dr["Gender"] = "Female";
                dt.Rows.Add(dr.ItemArray);
            }

            foreach (var rndAge in rndFemaleAgesForAgeGrp5)
            {
                dr["Age"] = rndAge;
                dr["Gender"] = "Female";
                dt.Rows.Add(dr.ItemArray);
            }

            #endregion

            var param = new DynamicParameters();
            param.Add("@Patients", dt.AsTableValuedParameter("[dbo].[PatientTableType]"));
            DataAccessService.Execute<Patient>("PatientSet", param);

        }
    }
}
