using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Avansight.Domain;
using Avansight.Domain.DTO;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Avansight.Business_Layer
{
    public class PatientService
    {
        private readonly RandomAgeGeneratorService _randomAgeGeneratorService;
        private readonly IConfiguration _config;

        public PatientService(RandomAgeGeneratorService randomAgeGeneratorService, IConfiguration config)
        {
            _randomAgeGeneratorService = randomAgeGeneratorService;
            _config = config;
        }

        public IEnumerable<Patient> GetPatients()
        {
            var result = DataAccessService.Query<Patient>("PatientGet", null, _config.GetConnectionString("AvansightDBCon"));
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

            var rndAgeGrp1MaleAges = _randomAgeGeneratorService.RandomAgeGenerator(21, 30, Convert.ToInt32(sessionPatientDetails.NoOfMalesInAgeGroup1));
            var rndAgeGrp2MaleAges = _randomAgeGeneratorService.RandomAgeGenerator(31, 40, Convert.ToInt32(sessionPatientDetails.NoOfMalesInAgeGroup2));
            var rndAgeGrp3MaleAges = _randomAgeGeneratorService.RandomAgeGenerator(41, 50, Convert.ToInt32(sessionPatientDetails.NoOfMalesInAgeGroup3));
            var rndAgeGrp4MaleAges = _randomAgeGeneratorService.RandomAgeGenerator(51, 60, Convert.ToInt32(sessionPatientDetails.NoOfMalesInAgeGroup4));
            var rndAgeGrp5MaleAges = _randomAgeGeneratorService.RandomAgeGenerator(61, 70, Convert.ToInt32(sessionPatientDetails.NoOfMalesInAgeGroup5));

            var rndAgeGrp1FemaleAges = _randomAgeGeneratorService.RandomAgeGenerator(21, 30, Convert.ToInt32(sessionPatientDetails.NoOfFemalesInAgeGroup1));
            var rndAgeGrp2FemaleAges = _randomAgeGeneratorService.RandomAgeGenerator(31, 40, Convert.ToInt32(sessionPatientDetails.NoOfFemalesInAgeGroup2));
            var rndAgeGrp3FemaleAges = _randomAgeGeneratorService.RandomAgeGenerator(41, 50, Convert.ToInt32(sessionPatientDetails.NoOfFemalesInAgeGroup3));
            var rndAgeGrp4FemaleAges = _randomAgeGeneratorService.RandomAgeGenerator(51, 60, Convert.ToInt32(sessionPatientDetails.NoOfFemalesInAgeGroup4));
            var rndAgeGrp5FemaleAges = _randomAgeGeneratorService.RandomAgeGenerator(61, 70, Convert.ToInt32(sessionPatientDetails.NoOfFemalesInAgeGroup5));

            #endregion

            var dtPatients = new DataTable();

            #region Add Male Patients to dt

            var dtAgeGrp1Males = CreateDataTable(rndAgeGrp1MaleAges, "Male");
            var dtAgeGrp2Males = CreateDataTable(rndAgeGrp2MaleAges, "Male");
            var dtAgeGrp3Males = CreateDataTable(rndAgeGrp3MaleAges, "Male");
            var dtAgeGrp4Males = CreateDataTable(rndAgeGrp4MaleAges, "Male");
            var dtAgeGrp5Males = CreateDataTable(rndAgeGrp5MaleAges, "Male");

            dtPatients.Merge(dtAgeGrp1Males);
            dtPatients.Merge(dtAgeGrp2Males);
            dtPatients.Merge(dtAgeGrp3Males);
            dtPatients.Merge(dtAgeGrp4Males);
            dtPatients.Merge(dtAgeGrp5Males);

            #endregion

            #region Add Female Patients to dt

            var dtAgeGrp1Females = CreateDataTable(rndAgeGrp1FemaleAges, "Female");
            var dtAgeGrp2Females = CreateDataTable(rndAgeGrp2FemaleAges, "Female");
            var dtAgeGrp3Females = CreateDataTable(rndAgeGrp3FemaleAges, "Female");
            var dtAgeGrp4Females = CreateDataTable(rndAgeGrp4FemaleAges, "Female");
            var dtAgeGrp5Females = CreateDataTable(rndAgeGrp5FemaleAges, "Female");

            dtPatients.Merge(dtAgeGrp1Females);
            dtPatients.Merge(dtAgeGrp2Females);
            dtPatients.Merge(dtAgeGrp3Females);
            dtPatients.Merge(dtAgeGrp4Females);
            dtPatients.Merge(dtAgeGrp5Females);

            #endregion

            var param = new DynamicParameters();
            param.Add("@Patients", dtPatients.AsTableValuedParameter("[dbo].[PatientTableType]"));
            return DataAccessService.Query<Patient>("PatientSet", param, _config.GetConnectionString("AvansightDBCon"));
        }

        public DataTable CreateDataTable(List<int> rndAgesForAgeGrp, string gender)
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