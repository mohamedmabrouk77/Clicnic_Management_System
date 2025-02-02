using System.ComponentModel.DataAnnotations;

namespace Clinic_Management_System.Models
{
    public class Patient
    {
        [Key]
        public int PatientId { get; set; }
        [Required]
        public string PatientName { get; set; }
        [Phone]
        public string PatientPhone { get; set; }
        [EmailAddress]
        public string PatientEmailAddress { get; set; }

        //Relation With Appointment O-to-M
        public List<Appointment> Appointment { get; set; }

        //Relation With Prescription O-to-O
        public Prescription Prescription { get; set; }

        //Relation With Medicine M-to-M
        public List<Medicine> Medicine { get; set; }
    }
}
