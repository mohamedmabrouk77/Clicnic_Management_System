using Clinic_Management_System.Dtos;
using Clinic_Management_System.Services.MedicineRepositorys;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clinic_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicineController : ControllerBase
    {
        private readonly IMedicineRepo _repo;

        public MedicineController(IMedicineRepo repo)
        {
            _repo = repo;
        }

        [HttpGet ("Get Medicine ById")]
        public IActionResult GetById(int id)
        {
            var result = _repo.GetAllById(id);
            return Ok(result);
        }

        [HttpPost ("Add New Medicine")]
        public IActionResult AddMedicine(MedicineDto dto)
        {
            _repo.AddMedicine(dto);
            return Accepted();
        }
    }
}
