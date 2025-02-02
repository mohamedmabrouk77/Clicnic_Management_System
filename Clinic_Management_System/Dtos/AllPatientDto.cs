using Clinic_Management_System.Models;
using System.ComponentModel.DataAnnotations;

namespace Clinic_Management_System.Dtos
{
    public class AllPatientDto
    {
        [Required]
        public string PatientName { get; set; }
        [Phone]
        public string PatientPhone { get; set; }
        [EmailAddress]
        public string PatientEmailAddress { get; set; }

        //Relation With AppointmentDto
        public List<appointdto> AppointmentDto { get; set; }

        //Relation With MedicineDto
        public List<MedicineDto> MedicineDto { get; set; }

        //Relation With PrescriptionDto
        public PrescriptionDto PrescriptionDto { get; set; }
    }
}
