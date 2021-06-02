using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Avansight.Domain
{
    public class Patient
    {
        //    [Key]
        //    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PatientId { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
    }
}
