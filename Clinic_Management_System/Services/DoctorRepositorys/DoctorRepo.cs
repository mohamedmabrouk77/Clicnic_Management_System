using Clinic_Management_System.AppDbContext;
using Clinic_Management_System.Dtos;
using Clinic_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Clinic_Management_System.Services.DoctorRepositorys
{
    public class DoctorRepo : IDoctorRepo
    {
        private readonly dbcontext _context;

        public DoctorRepo(dbcontext context)
        {
            _context = context;
        }

        public void AddAll(AllDoctorDto dto)
        {
            var result = new Doctor
            {
                DoctorName = dto.DoctorName,
                DoctorPhone = dto.DoctorPhone,
                DoctorEmailAddress = dto.DoctorEmailAddress,
                Appointment = dto.AppointmentDto.Select(x=> new Appointment
                {
                    DateTime = x.DateTime,
                    Patient = new Patient
                    {
                        PatientName = x.PatientDto.PatientName,
                        PatientEmailAddress = x.PatientDto.PatientEmailAddress,
                        PatientPhone = x.PatientDto.PatientPhone,   
                    }
                }).ToList(),
            };
            _context.Doctors.Add(result);
            _context.SaveChanges();
        }

        public List<AllDoctorDto> GetAllDoctor()
        {
            var result = _context.Doctors.
                Include(x=>x.Appointment).
                ThenInclude(x=>x.Patient).
                Select(i => new AllDoctorDto
                {
                    DoctorName = i.DoctorName,
                    DoctorPhone = i.DoctorPhone,
                    DoctorEmailAddress = i.DoctorEmailAddress,
                    AppointmentDto = i.Appointment.Select(t => new AppointmentDto
                    {
                        DateTime = t.DateTime,
                        PatientDto = new PatientDto
                        {
                            PatientName = t.Patient.PatientName,
                            PatientEmailAddress = t.Patient.PatientEmailAddress,
                            PatientPhone = t.Patient.PatientPhone,
                        }
                    }).ToList()
                }).ToList();
            return result;
        }

        public void UpdateAll(AllDoctorDto dto, int id)
        {
            var result = _context.Doctors.
                Include(x => x.Appointment).
                ThenInclude(x => x.Patient).
                FirstOrDefault(x => x.DoctorId == id);

            if(result != null)
            {
                result.DoctorName = dto.DoctorName;
                result.DoctorPhone = dto.DoctorPhone;
                result.DoctorEmailAddress = dto.DoctorEmailAddress;
                result.Appointment = dto.AppointmentDto.Select(i => new Appointment
                {
                    DateTime= i.DateTime,
                    Patient = new Patient
                    {
                        PatientName = i.PatientDto.PatientName,
                        PatientPhone= i.PatientDto.PatientPhone,
                        PatientEmailAddress= i.PatientDto.PatientEmailAddress,
                    }
                }).ToList();
            }
            else
            {
                throw new Exception("Id Not Found");
            }
            _context.Doctors.Update(result);
            _context.SaveChanges();
        }
    }
}
