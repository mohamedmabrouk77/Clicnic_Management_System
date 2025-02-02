using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Clinic_Management_System.Models
{
    public class Prescription
    {
        [Key]
        public int PrescriptionId { get; set; }
        [Required]
        public string PrescriptionName { get; set; }

        //Relation With Patient
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        public Patient Patient { get; set; }
    }
}
