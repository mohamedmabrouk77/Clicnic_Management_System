using System.ComponentModel.DataAnnotations;

namespace Clinic_Management_System.Dtos
{
    public class PatientDto
    {
        [Required]
        public string PatientName { get; set; }
        [Phone]
        public string PatientPhone { get; set; }
        [EmailAddress]
        public string PatientEmailAddress { get; set; }
    }
}
