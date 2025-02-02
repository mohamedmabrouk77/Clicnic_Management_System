using Clinic_Management_System.AppDbContext;
using Clinic_Management_System.Dtos;
using Clinic_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Clinic_Management_System.Services.PrescriptionRepositorys
{
    public class PrescriptionRepo : IPrescriptionRepo
    {
        private readonly dbcontext _context;

        public PrescriptionRepo(dbcontext context)
        {
            _context = context;
        }

        public void AddPrescription(AllPrescriptionDto dto)
        {
            var result = new Prescription
            {
                PrescriptionName = dto.PrescriptionName,
                Patient = new Patient
                {
                    PatientName = dto.PatientsDto.PatientName,
                    PatientPhone = dto.PatientsDto.PatientPhone,
                    PatientEmailAddress = dto.PatientsDto.PatientEmailAddress,
                    Medicine = dto.PatientsDto.MedicineDto.Select(x=> new Medicine
                    {
                        MedicineName = x.MedicineName,
                    }).ToList(),
                }
            };
            
            _context.Prescriptions.Add(result);
            _context.SaveChanges();
        }

        public void DeletePrescription(int id)
        {
            var result = _context.Prescriptions
                .Include(x => x.Patient)
                .ThenInclude(x => x.Medicine)
                .FirstOrDefault(x=>x.PrescriptionId == id);

            if(result != null)
            {
                if(result.Patient != null)
                {
                    _context.Patients.Remove(result.Patient);
                    var medicineDelete = result.Patient.Medicine.Distinct().ToList();
                    _context.Medicines.RemoveRange(medicineDelete);
                }
                _context.Prescriptions.Remove(result);
                _context.SaveChanges();
            }
        }

        public List<AllPrescriptionDto> GetAllPrescriptions()
        {
            var result = _context.Prescriptions
                .Include(x => x.Patient)
                .ThenInclude(x => x.Medicine)
                .Select(i => new AllPrescriptionDto
                {
                    PrescriptionName = i.PrescriptionName,
                    PatientsDto = new PatientsDto
                    {
                        PatientName = i.Patient.PatientName,
                        PatientPhone = i.Patient.PatientPhone,
                        PatientEmailAddress = i.Patient.PatientEmailAddress,

                        MedicineDto = i.Patient.Medicine.Select(i=> new MedicineDto
                        {
                            MedicineName = i.MedicineName,
                        }).ToList(),
                    }
                }).ToList();
            return result;
        }
    }
}
