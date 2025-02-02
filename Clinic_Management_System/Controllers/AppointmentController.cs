using Clinic_Management_System.Dtos;
using Clinic_Management_System.Services.AppointmentRepositorys;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clinic_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentRepo _repo;

        public AppointmentController(IAppointmentRepo repo)
        {
            _repo = repo;
        }

        [HttpGet ("GetById")]
        public IActionResult GetById(int id)
        {
            var result = _repo.GetAllById (id);
            return Ok (result);
        }

        [HttpPost]
        public IActionResult AddAll(AllAppointmentDto dto)
        {
            _repo.AddAll (dto);
            return Accepted();
        }

        [HttpPut]
        public IActionResult UpdateById(AllAppointmentDto dto, int id)
        {
            _repo.UpdateById(dto, id);
            return NoContent();
        }

        [HttpDelete]
        public IActionResult DeleteById(int id)
        {
            _repo.DeleteById (id);
            return NoContent ();
        }
    }
}
