using System.ComponentModel.DataAnnotations;

namespace Clinic_Management_System.Dtos
{
    public class PrescriptionDto
    {
        [Required]
        public string PrescriptionName { get; set; }
    }
}
