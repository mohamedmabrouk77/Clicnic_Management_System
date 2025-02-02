using Clinic_Management_System.Models;
using System.ComponentModel.DataAnnotations;

namespace Clinic_Management_System.Dtos
{
    public class AllMedicineDto
    {
        public int MedicineId { get; set; }
        [Required]
        public string MedicineName { get; set; }

        //Relation With Patient
        public List<PatientDto> PatientDto { get; set; }
    }
}
