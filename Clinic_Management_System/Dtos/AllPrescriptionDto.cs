using Clinic_Management_System.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Clinic_Management_System.Dtos
{
    public class AllPrescriptionDto
    {
        [Required]
        public string PrescriptionName { get; set; }

        //Relation With PatientDto
        public PatientsDto PatientsDto { get; set; }
    }
}
