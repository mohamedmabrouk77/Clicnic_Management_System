using System.ComponentModel.DataAnnotations;

namespace Clinic_Management_System.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }
        public DateTime DateTime { get; set; }

        //Relation With Doctor
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        //Relation With Patient
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
    }
}
