using System.ComponentModel.DataAnnotations;

namespace Clinic_Management_System.Models
{
    public class Doctor
    {
        [Key]
        public int DoctorId { get; set; }
        [Required]
        public string DoctorName { get; set; }
        [Phone]
        public string DoctorPhone { get; set; }
        [EmailAddress]
        public string DoctorEmailAddress { get; set; }

        //Relation With Appointment
        public List<Appointment> Appointment { get; set; }
    }
}
