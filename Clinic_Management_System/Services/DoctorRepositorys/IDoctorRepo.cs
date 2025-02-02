using Clinic_Management_System.Dtos;

namespace Clinic_Management_System.Services.DoctorRepositorys
{
    public interface IDoctorRepo
    {
        public List<AllDoctorDto> GetAllDoctor();
        public void AddAll(AllDoctorDto dto);
        public void UpdateAll(AllDoctorDto dto, int id);
    }
}
