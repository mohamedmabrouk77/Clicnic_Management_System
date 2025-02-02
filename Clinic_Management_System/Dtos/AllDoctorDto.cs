using Clinic_Management_System.Models;
using System.ComponentModel.DataAnnotations;

namespace Clinic_Management_System.Dtos
{
    public class AllDoctorDto
    {
        [Required]
        public string DoctorName { get; set; }
        [Phone]
        public string DoctorPhone { get; set; }
        [EmailAddress]
        public string DoctorEmailAddress { get; set; }

        //Relation With Appointment
        public List<AppointmentDto> AppointmentDto { get; set; }
    }
}
