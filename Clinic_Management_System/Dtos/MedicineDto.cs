using System.ComponentModel.DataAnnotations;

namespace Clinic_Management_System.Dtos
{
    public class MedicineDto
    {
        [Required]
        public string MedicineName { get; set; }
    }
}
