using System;
using System.Collections.Generic;
using System.Text;

namespace Avansight.Domain.DTO
{
    public class PatientDetailsDto
    {
        public decimal NoOfMales { get; set; }
        public decimal NoOfFemales { get; set; }

        public decimal NoOfPersonsInAgeGroup1 { get; set; }
        public decimal NoOfPersonsInAgeGroup2 { get; set; }
        public decimal NoOfPersonsInAgeGroup3 { get; set; }
        public decimal NoOfPersonsInAgeGroup4 { get; set; }
        public decimal NoOfPersonsInAgeGroup5 { get; set; }

        public decimal NoOfMalesInAgeGroup1 { get; set; }
        public decimal NoOfMalesInAgeGroup2 { get; set; }
        public decimal NoOfMalesInAgeGroup3 { get; set; }
        public decimal NoOfMalesInAgeGroup4 { get; set; }
        public decimal NoOfMalesInAgeGroup5 { get; set; }

        public decimal NoOfFemalesInAgeGroup1 { get; set; }
        public decimal NoOfFemalesInAgeGroup2 { get; set; }
        public decimal NoOfFemalesInAgeGroup3 { get; set; }
        public decimal NoOfFemalesInAgeGroup4 { get; set; }
        public decimal NoOfFemalesInAgeGroup5 { get; set; }
    }
}
