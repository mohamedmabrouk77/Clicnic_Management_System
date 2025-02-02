using Clinic_Management_System.Dtos;

namespace Clinic_Management_System.Services.PatientController
{
    public interface IPatientRepo
    {
        public List<AllPatientDto> GetAllPatients();
        public AllPatientDto GetPatientById(int id);
        public void AddAllPatient(AllPatientDto dto);
        public void UpdateAllPatient(AllPatientDto dto, int id);
        public void DeleteAllPatient(int id);
    }
}
