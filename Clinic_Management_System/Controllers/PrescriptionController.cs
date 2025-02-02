using Clinic_Management_System.Dtos;
using Clinic_Management_System.Services.PrescriptionRepositorys;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clinic_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionRepo _repo;

        public PrescriptionController(IPrescriptionRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]   
        public IActionResult GetAll()
        {
            var result = _repo.GetAllPrescriptions();
            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddAll(AllPrescriptionDto dto)
        {
            _repo.AddPrescription(dto);
            return Accepted();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _repo.DeletePrescription(id);
            return NoContent();
        }
    }
}
