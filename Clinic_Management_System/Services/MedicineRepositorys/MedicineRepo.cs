using Clinic_Management_System.AppDbContext;
using Clinic_Management_System.Dtos;
using Clinic_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Clinic_Management_System.Services.MedicineRepositorys
{
    public class MedicineRepo : IMedicineRepo
    {
        private readonly dbcontext _context;

        public MedicineRepo(dbcontext context)
        {
            _context = context;
        }

        public void AddMedicine(MedicineDto dto)
        {
            var result = new Medicine
            {
                MedicineName = dto.MedicineName,
            };
            _context.Medicines.Add(result); 
            _context.SaveChanges();
        }

        public AllMedicineDto GetAllById(int id)
        {
            var medicine = _context.Medicines
                .Include(x => x.Patient)
                .FirstOrDefault(x => x.MedicineId == id);

            return new AllMedicineDto
            {
                MedicineName = medicine.MedicineName,
                PatientDto = medicine.Patient.Select(i => new PatientDto
                {
                    PatientPhone = i.PatientPhone,
                    PatientName = i.PatientName,
                    PatientEmailAddress = i.PatientEmailAddress,
                }).ToList(),
            };
        }
    }
}
