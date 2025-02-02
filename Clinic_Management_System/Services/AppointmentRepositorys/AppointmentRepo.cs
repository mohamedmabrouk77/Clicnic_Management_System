using Clinic_Management_System.AppDbContext;
using Clinic_Management_System.Dtos;
using Clinic_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Clinic_Management_System.Services.AppointmentRepositorys
{
    public class AppointmentRepo : IAppointmentRepo
    {
        private readonly dbcontext _context;

        public AppointmentRepo(dbcontext context)
        {
            _context = context;
        }

        public void AddAll(AllAppointmentDto dto)
        {
            var result = new Appointment
            {
                DateTime = dto.DateTime,
                Patient = new Patient
                {
                    PatientName = dto.PatientDto.PatientName,
                    PatientEmailAddress = dto.PatientDto.PatientEmailAddress,
                    PatientPhone = dto.PatientDto.PatientPhone,
                },
                Doctor = new Doctor
                {
                    DoctorEmailAddress = dto.DoctorDto.DoctorEmailAddress,
                    DoctorName = dto.DoctorDto.DoctorName,
                    DoctorPhone = dto.DoctorDto.DoctorPhone,
                }
            };
            _context.Appointments.Add(result);
            _context.SaveChanges();
        }

        public void DeleteById(int id)
        {
            var result = _context.Appointments
                .Include(x=>x.Doctor)
                .Include(x=>x.Patient)
                .FirstOrDefault(x=>x.AppointmentId == id);

            if(result != null)
            {
                if(result.Doctor != null)
                {
                    _context.Doctors.RemoveRange(result.Doctor);
                }
                if(result.Patient != null)
                {
                    _context.Patients.RemoveRange(result.Patient);
                }
                _context.Appointments.Remove(result);
            }
            else
            {
                throw new Exception("Id Not Found");
            }
            _context.SaveChanges();
        }

        public AllAppointmentDto GetAllById(int id)
        {
            var result = _context.Appointments
                .Include(x=>x.Doctor)
                .Include(x=>x.Patient)
                .FirstOrDefault(x=>x.AppointmentId == id);

            return new AllAppointmentDto
            {
                DateTime = result.DateTime,
                PatientDto = new PatientDto
                {
                    PatientName = result.Patient.PatientName,
                    PatientEmailAddress= result.Patient.PatientEmailAddress,
                    PatientPhone = result.Patient.PatientPhone,
                },
                DoctorDto = new DoctorDto
                {
                    DoctorName = result.Doctor.DoctorName,
                    DoctorEmailAddress= result.Doctor.DoctorEmailAddress,
                    DoctorPhone = result.Doctor.DoctorPhone,
                }
            };
        }

        public void UpdateById(AllAppointmentDto dto, int id)
        {
            var result = _context.Appointments
                .Include(x=>x.Doctor)
                .Include(x=>x.Patient)
                .FirstOrDefault (x=>x.AppointmentId == id);

            result.DateTime = dto.DateTime;

            if(result.Doctor != null && dto.DoctorDto != null)
            {
                result.Doctor.DoctorName = dto.DoctorDto.DoctorName;
                result.Doctor.DoctorPhone = dto.DoctorDto.DoctorPhone;
                result.Doctor.DoctorEmailAddress = dto.DoctorDto.DoctorEmailAddress;
            }

            if(result.Patient != null && dto.PatientDto != null)
            {
                result.Patient.PatientName = dto.PatientDto.PatientName;
                result.Patient.PatientPhone = dto.PatientDto.PatientPhone;
                result.Patient.PatientEmailAddress = dto.PatientDto.PatientEmailAddress;
            }

            _context.Appointments.Update(result);
            _context.SaveChanges();
        }
    }
}
