using Clinic_Management_System.Models;

namespace Clinic_Management_System.Dtos
{
    public class AppointmentDto
    {
        public DateTime DateTime { get; set; }
        //Relation With PatientDto
        public PatientDto PatientDto { get; set; }
    }
}
