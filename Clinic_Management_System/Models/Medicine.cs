using System.ComponentModel.DataAnnotations;

namespace Clinic_Management_System.Models
{
    public class Medicine
    {
        [Key]
        public int MedicineId { get; set; }
        [Required]
        public string MedicineName { get; set; }

        //Relation With Patient
        public List<Patient> Patient { get; set; }
    }
}
