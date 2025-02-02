using Clinic_Management_System.Dtos;

namespace Clinic_Management_System.Services.PrescriptionRepositorys
{
    public interface IPrescriptionRepo
    {
        public List<AllPrescriptionDto> GetAllPrescriptions();
        public void AddPrescription(AllPrescriptionDto dto);
        public void DeletePrescription(int id);
    }
}
