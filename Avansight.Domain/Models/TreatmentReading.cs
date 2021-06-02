using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Avansight.Domain
{
    public class TreatmentReading
    {
        public int TreatmentReadingId { get; set; }
        public string VisitWeek { get; set; }

        public decimal Reading { get; set; }
      
        public long PatientId { get; set; }

        [ForeignKey("PatientId")]
        public virtual Patient Patients { get; set; }
    }
}
