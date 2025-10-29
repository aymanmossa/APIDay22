using APIDay22.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIDay22.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IUoW _uow;
        public StudentController (IUoW uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public IActionResult GetAllStudents()
        {
            var students = _uow.Students.GetAll();
            return Ok(new { Students = students, Message = "All Students Found" });
        }
    }
}
