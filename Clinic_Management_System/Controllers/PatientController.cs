using Clinic_Management_System.Dtos;
using Clinic_Management_System.Services.PatientController;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clinic_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepo _repo;

        public PatientController(IPatientRepo repo)
        {
            _repo = repo;
        }

        [HttpGet ("GetAllPAtient")]
        public IActionResult GetAll()
        {
            var result = _repo.GetAllPatients();
            return Ok(result);
        }

        [HttpGet ("GetAllPatientByID")]
        public IActionResult GetById(int id)
        {
            var result = _repo.GetPatientById(id);
            return Ok(result);
        }

        [HttpPost ("AddAllPatient")]
        public IActionResult AddAll(AllPatientDto dto)
        {
            _repo.AddAllPatient(dto);
            return Accepted();
        }

        [HttpPut ("UpdateAllPatient")]
        public IActionResult UpdateAll(AllPatientDto dto, int id)
        {
            _repo.UpdateAllPatient(dto, id);
            return NoContent();
        }

        [HttpDelete ("DeleteAllPatient")]
        public IActionResult DeleteAllById(int id)
        {
            _repo.DeleteAllPatient(id);
            return NoContent();
        }
    }
}
