

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P01_HospitalDatabase.Data.Models
{
    public class Diagnose
    {
        public int DiagnoseId { get; set; }

        public string Name { get; set; }

        public string Comments { get; set; }

        [Required, ForeignKey("Patient")]
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
