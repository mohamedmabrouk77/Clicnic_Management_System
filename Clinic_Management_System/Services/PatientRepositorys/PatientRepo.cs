using Clinic_Management_System.AppDbContext;
using Clinic_Management_System.Dtos;
using Clinic_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Clinic_Management_System.Services.PatientController
{
    public class PatientRepo : IPatientRepo
    {
        private readonly dbcontext _context;

        public PatientRepo(dbcontext context)
        {
            _context = context;
        }

        public void AddAllPatient(AllPatientDto dto)
        {
            var result = new Patient
            {
                PatientName = dto.PatientName,
                PatientPhone = dto.PatientPhone,
                PatientEmailAddress = dto.PatientEmailAddress,
                Appointment = dto.AppointmentDto.Select(x=> new Appointment
                {
                    DateTime = x.DateTime,
                    Doctor = new Doctor
                    {
                        DoctorName = x.doctorDto.DoctorName,
                        DoctorPhone = x.doctorDto.DoctorPhone,
                        DoctorEmailAddress = x.doctorDto.DoctorEmailAddress
                    }
                }).ToList(),
                Prescription = new Prescription
                {
                    PrescriptionName = dto.PrescriptionDto.PrescriptionName,
                },
                Medicine = dto.MedicineDto.Select(i => new Medicine
                {
                    MedicineName = i.MedicineName,
                }).ToList(),
            };
            _context.Patients.Add(result);
            _context.SaveChanges();
        }

        public void DeleteAllPatient(int id)
        {
            var result = _context.Patients.
                Include(x => x.Appointment).
                ThenInclude(x => x.Doctor).
                Include(x => x.Medicine).
                Include(x => x.Prescription).
                FirstOrDefault(x=>x.PatientId == id);

            if(result != null)
            {
                var doctordelete = result.Appointment?
                    .Select(x=>x.Doctor).Distinct().ToList();
               if(result.Medicine != null)
               {
                    _context.Medicines.RemoveRange(result.Medicine);
               }
               
               if(result.Prescription != null)
               {
                   _context.Prescriptions.RemoveRange(result.Prescription);
               }
               if(result.Appointment != null)
               {
                   _context.Appointments.RemoveRange(result.Appointment);
               }

               _context.Patients.Remove(result);

               foreach(var doctor in doctordelete)
               {
                    bool IsDoctordelete = _context.Appointments
                        .Any(a=>a.DoctorId == doctor.DoctorId);

                    if (!IsDoctordelete)
                    {
                        _context.Doctors.Remove(doctor);
                    }
               }
            }

            else
            {
                throw new Exception("Id Not Found");
            }

            _context.SaveChanges();
        }

        public List<AllPatientDto> GetAllPatients()
        {
            var result = _context.Patients.
                Include(x => x.Appointment).
                ThenInclude(x => x.Doctor).
                Include(x => x.Medicine).
                Include(x=>x.Prescription).
                Select(x=> new AllPatientDto
                {
                    PatientName = x.PatientName,
                    PatientPhone = x.PatientPhone,
                    PatientEmailAddress = x.PatientEmailAddress,
                    PrescriptionDto = new PrescriptionDto
                    {
                        PrescriptionName = x.Prescription.PrescriptionName,
                    },
                    MedicineDto = x.Medicine.Select(t => new MedicineDto
                    {
                        MedicineName = t.MedicineName,
                    }).ToList(),
                    AppointmentDto = x.Appointment.Select(x=> new appointdto
                    {
                        DateTime = x.DateTime,
                        doctorDto = new DoctorDto
                        {
                            DoctorName = x.Doctor.DoctorName,
                            DoctorEmailAddress = x.Doctor.DoctorEmailAddress,
                            DoctorPhone = x.Doctor.DoctorPhone,
                        }
                    }).ToList(),
                }).ToList();
            return result;
        }

        public AllPatientDto GetPatientById(int id)
        {
            var result = _context.Patients.
                Include(x => x.Appointment).
                ThenInclude(x => x.Doctor).
                Include(x => x.Medicine).
                Include(x => x.Prescription).
                FirstOrDefault(x=>x.PatientId == id);

            return new AllPatientDto
            {
                PatientName = result.PatientName,
                PatientEmailAddress = result.PatientEmailAddress,
                PatientPhone = result.PatientPhone,
                PrescriptionDto = new PrescriptionDto
                {
                    PrescriptionName = result.Prescription.PrescriptionName
                },
                MedicineDto = result.Medicine.Select(i => new MedicineDto
                {
                    MedicineName = i.MedicineName,
                }).ToList(),
                AppointmentDto = result.Appointment.Select(x=> new appointdto
                {
                    DateTime = x.DateTime,
                    doctorDto = new DoctorDto
                    {
                        DoctorName = x.Doctor.DoctorPhone,
                        DoctorEmailAddress = x.Doctor.DoctorEmailAddress,
                        DoctorPhone = x.Doctor.DoctorPhone,
                    }
                }).ToList(),
            };
        }

        public void UpdateAllPatient(AllPatientDto dto, int id)
        {
            var result = _context.Patients
                .Include(x => x.Appointment)
                .ThenInclude(x => x.Doctor)
                .Include(x => x.Medicine)
                .Include(x => x.Prescription)
                .FirstOrDefault(x => x.PatientId == id);

            if (result == null)
            {
                throw new Exception("Id Not Found");
            }

            result.PatientPhone = dto.PatientPhone;
            result.PatientName = dto.PatientName;
            result.PatientEmailAddress = dto.PatientEmailAddress;


            result.Appointment.Clear();
            foreach (var t in dto.AppointmentDto)
            {
                var existingDoctor = _context.Doctors
                    .FirstOrDefault(d => d.DoctorName == t.doctorDto.DoctorName);

                if (existingDoctor == null)
                {
                    existingDoctor = new Doctor
                    {
                        DoctorName = t.doctorDto.DoctorName,
                        DoctorEmailAddress = t.doctorDto.DoctorEmailAddress,
                        DoctorPhone = t.doctorDto.DoctorPhone
                    };
                    _context.Doctors.Add(existingDoctor);  
                    _context.SaveChanges();
                }

                result.Appointment.Add(new Appointment
                {
                    DateTime = t.DateTime,
                    DoctorId = existingDoctor.DoctorId
                });
            }

            result.Medicine.Clear();
            foreach (var a in dto.MedicineDto)
            {
                var existingMedicine = _context.Medicines
                    .FirstOrDefault(m => m.MedicineName == a.MedicineName);

                if (existingMedicine == null)
                {
                    existingMedicine = new Medicine
                    {
                        MedicineName = a.MedicineName
                    };
                    _context.Medicines.Add(existingMedicine);
                    _context.SaveChanges(); 
                }

                result.Medicine.Add(existingMedicine);
            }

            if (result.Prescription != null)
            {
                result.Prescription.PrescriptionName = dto.PrescriptionDto.PrescriptionName;
            }
            else
            {
                result.Prescription = new Prescription
                {
                    PrescriptionName = dto.PrescriptionDto.PrescriptionName
                };
            }

            _context.Patients.Update(result);
            _context.SaveChanges();
        }

    }
}
