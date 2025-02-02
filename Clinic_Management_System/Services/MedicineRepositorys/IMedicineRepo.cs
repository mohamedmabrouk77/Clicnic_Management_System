using Clinic_Management_System.Dtos;

namespace Clinic_Management_System.Services.MedicineRepositorys
{
    public interface IMedicineRepo
    {
        public AllMedicineDto GetAllById(int id);
        public void AddMedicine(MedicineDto dto);
    }
}
