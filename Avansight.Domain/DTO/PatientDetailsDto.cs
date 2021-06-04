using System;
using System.Collections.Generic;
using System.Text;

namespace Avansight.Domain.DTO
{
    public class PatientDetailsDto
    {
        public decimal NoOfPersonsInAgeGroup1 { get; set; }
        public decimal NoOfPersonsInAgeGroup2 { get; set; }
        public decimal NoOfPersonsInAgeGroup3 { get; set; }
        public decimal NoOfPersonsInAgeGroup4 { get; set; }
        public decimal NoOfPersonsInAgeGroup5 { get; set; }
        public decimal NoOfMales { get; set; }
        public decimal NoOfFemales { get; set; }
    }
}
