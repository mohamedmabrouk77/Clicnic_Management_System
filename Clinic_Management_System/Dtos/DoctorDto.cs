using System.ComponentModel.DataAnnotations;

namespace Clinic_Management_System.Dtos
{
    public class DoctorDto
    {
        [Required]
        public string DoctorName { get; set; }
        [Phone]
        public string DoctorPhone { get; set; }
        [EmailAddress]
        public string DoctorEmailAddress { get; set; }
    }
}
