using Clinic_Management_System.Dtos;

namespace Clinic_Management_System.Services.AppointmentRepositorys
{
    public interface IAppointmentRepo
    {
        public AllAppointmentDto GetAllById(int id);
        public void UpdateById(AllAppointmentDto dto, int id);
        public void DeleteById(int id);
        public void AddAll(AllAppointmentDto dto);
    }
}
