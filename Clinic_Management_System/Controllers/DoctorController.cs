using Clinic_Management_System.Dtos;
using Clinic_Management_System.Services.DoctorRepositorys;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clinic_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorRepo _repo;

        public DoctorController(IDoctorRepo repo)
        {
            _repo = repo;
        }

        [HttpGet ("GetAllDoctor")]
        public IActionResult GetAll()
        {
            var result = _repo.GetAllDoctor();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost ("AddAllDoctor")]
        public IActionResult AddAllDoctor(AllDoctorDto dto)
        {
            _repo.AddAll(dto);
            if (dto == null)
            {
                return BadRequest();
            }
            return Created();
        }

        [HttpPut ("UpdateAllDoctor")]
        public IActionResult UpdateAllDoctor(AllDoctorDto dto, int id)
        {
            _repo.UpdateAll(dto, id);
            if (dto == null && id == null)
            {
                return BadRequest();
            }
            return NoContent();
        }
    }
}
