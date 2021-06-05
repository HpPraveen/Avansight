using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Avansight.Domain;
using Avansight.Domain.DTO;
using Dapper;

namespace Avansight.Business_Layer
{
    public class PatientService
    {
        private readonly RandomAgeGeneratorService _randomAgeGeneratorService;

        public PatientService(RandomAgeGeneratorService randomAgeGeneratorService)
        {
            _randomAgeGeneratorService = randomAgeGeneratorService;
        }

        public IEnumerable<Patient> GetPatients()
        {
            var result = DataAccessService.Query<Patient>("PatientGet");
            return result.ToList();
        }

        public PatientDetailsDto CalculatePatientDetails(decimal sampleSize, decimal maleWeight, decimal femaleWeight,
            PatientDetailsDto patientDetails)
        {
            #region Logic

            var totalPersonsWeight = Math.Round(maleWeight + femaleWeight);

            var noOfMales = Math.Round(maleWeight / totalPersonsWeight * sampleSize);
            var noOfFemales = Math.Round(femaleWeight / totalPersonsWeight * sampleSize);

            var totalAgeGrpWeight = patientDetails.NoOfPersonsInAgeGroup1 + patientDetails.NoOfPersonsInAgeGroup2 +
                                    patientDetails.NoOfPersonsInAgeGroup3 + patientDetails.NoOfPersonsInAgeGroup4 +
                                    patientDetails.NoOfPersonsInAgeGroup5;

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

        public IEnumerable<Patient> AddPatients(PatientDetailsDto sessionPatientDetails)
        {
            #region Random age generator for  male & female age-groups

            var rndMaleAgesForAgeGrp1 = _randomAgeGeneratorService.RandomAgeGenerator(21, 30, Convert.ToInt32(sessionPatientDetails.NoOfMalesInAgeGroup1));
            var rndMaleAgesForAgeGrp2 = _randomAgeGeneratorService.RandomAgeGenerator(31, 40, Convert.ToInt32(sessionPatientDetails.NoOfMalesInAgeGroup2));
            var rndMaleAgesForAgeGrp3 = _randomAgeGeneratorService.RandomAgeGenerator(41, 50, Convert.ToInt32(sessionPatientDetails.NoOfMalesInAgeGroup3));
            var rndMaleAgesForAgeGrp4 = _randomAgeGeneratorService.RandomAgeGenerator(51, 60, Convert.ToInt32(sessionPatientDetails.NoOfMalesInAgeGroup4));
            var rndMaleAgesForAgeGrp5 = _randomAgeGeneratorService.RandomAgeGenerator(61, 70, Convert.ToInt32(sessionPatientDetails.NoOfMalesInAgeGroup5));

            var rndFemaleAgesForAgeGrp1 = _randomAgeGeneratorService.RandomAgeGenerator(21, 30, Convert.ToInt32(sessionPatientDetails.NoOfFemalesInAgeGroup1));
            var rndFemaleAgesForAgeGrp2 = _randomAgeGeneratorService.RandomAgeGenerator(31, 40, Convert.ToInt32(sessionPatientDetails.NoOfFemalesInAgeGroup2));
            var rndFemaleAgesForAgeGrp3 = _randomAgeGeneratorService.RandomAgeGenerator(41, 50, Convert.ToInt32(sessionPatientDetails.NoOfFemalesInAgeGroup3));
            var rndFemaleAgesForAgeGrp4 = _randomAgeGeneratorService.RandomAgeGenerator(51, 60, Convert.ToInt32(sessionPatientDetails.NoOfFemalesInAgeGroup4));
            var rndFemaleAgesForAgeGrp5 = _randomAgeGeneratorService.RandomAgeGenerator(61, 70, Convert.ToInt32(sessionPatientDetails.NoOfFemalesInAgeGroup5));

            #endregion

            var dt = new DataTable();

            #region Add Male Patients to dt

            var dtMalesAgeGrp1 = CreateDataRow(rndMaleAgesForAgeGrp1, "Male");
            var dtMalesAgeGrp2 = CreateDataRow(rndMaleAgesForAgeGrp2, "Male");
            var dtMalesAgeGrp3 = CreateDataRow(rndMaleAgesForAgeGrp3, "Male");
            var dtMalesAgeGrp4 = CreateDataRow(rndMaleAgesForAgeGrp4, "Male");
            var dtMalesAgeGrp5 = CreateDataRow(rndMaleAgesForAgeGrp5, "Male");

            dt.Merge(dtMalesAgeGrp1);
            dt.Merge(dtMalesAgeGrp2);
            dt.Merge(dtMalesAgeGrp3);
            dt.Merge(dtMalesAgeGrp4);
            dt.Merge(dtMalesAgeGrp5);

            #endregion

            #region Add Female Patients to dt

            var dtFemalesAgeGrp1 = CreateDataRow(rndFemaleAgesForAgeGrp1, "Female");
            var dtFemalesAgeGrp2 = CreateDataRow(rndFemaleAgesForAgeGrp2, "Female");
            var dtFemalesAgeGrp3 = CreateDataRow(rndFemaleAgesForAgeGrp3, "Female");
            var dtFemalesAgeGrp4 = CreateDataRow(rndFemaleAgesForAgeGrp4, "Female");
            var dtFemalesAgeGrp5 = CreateDataRow(rndFemaleAgesForAgeGrp5, "Female");

            dt.Merge(dtFemalesAgeGrp1);
            dt.Merge(dtFemalesAgeGrp2);
            dt.Merge(dtFemalesAgeGrp3);
            dt.Merge(dtFemalesAgeGrp4);
            dt.Merge(dtFemalesAgeGrp5);

            #endregion

            var param = new DynamicParameters();
            param.Add("@Patients", dt.AsTableValuedParameter("[dbo].[PatientTableType]"));
            return DataAccessService.Query<Patient>("PatientSet", param);
        }

        public DataTable CreateDataRow(List<int> rndAgesForAgeGrp, string gender)
        {
            var dt = new DataTable();
            var dr = dt.NewRow();
            dt.Columns.Add("Age", typeof(int));
            dt.Columns.Add("Gender", typeof(string));

            foreach (var rndAge in rndAgesForAgeGrp)
            {
                dr["Age"] = rndAge;
                dr["Gender"] = gender;
                dt.Rows.Add(dr.ItemArray);
            }

            return dt;
        }
    }
}