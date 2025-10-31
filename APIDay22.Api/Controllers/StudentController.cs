using APIDay22.Core.DTOs;
using APIDay22.Core.Interfaces;
using APIDay22.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIDay22.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IUoW _uow;
        public StudentController(IUoW uow)
        {
            _uow = uow;
        }
        //------------------------------------------

        [HttpGet]
        public IActionResult GetAllStudents()
        {
            var students = _uow.Students.GetAll();
            return Ok(new { Students = students, Message = "All Students Found" });
        }
        //------------------------------------------

        [HttpGet("GetStudentDetaails")]
        public IActionResult GetStudentDeatails()
        {
            var students = _uow.Students.GetAll();

            var result = students.Select(s => new  StudentDetailsDto
            {
                Name = s.Name,
                Dept = s.Department?.Name ?? "N/A",
                Manager = s.Department?.Manger ?? "N/A",
                Address = s.Address ?? "N/A",
                Date = DateTime.Now
            }).ToList();

            return Ok(result);
        }

        //-------------------------------------------

        [HttpGet("{ssn:int}")]
        public IActionResult GetStudentBySsn(int ssn)
        {
            var student = _uow.Students.GetById(ssn);
            if (student == null)
                return NotFound(new { Message = "Student Not Found!" });

            return Ok(new { Student = student, Message = $"Student {student.Name}" });
        }

        //-------------------------------------------   

        [HttpGet("name/{name:alpha}")]
        public IActionResult GetStudentByName(string name)
        {
            var student = _uow.Students.Find(s => s.Name == name).FirstOrDefault();
            if (student == null)
                return NotFound(new { Message = $"Student {name}" });

            return Ok(new { Student = student, Message = $"Student {student.Name}" });
        }

        //-------------------------------------------

        [HttpPost]
        public IActionResult AddStudent([FromBody] Student student)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _uow.Students.Add(student);
            _uow.Complete();

            return CreatedAtAction(nameof(GetStudentBySsn), new { ssn = student.Ssn },
                new { Message = "Student Added Successfully", Student = student });
        }

        //-------------------------------------------

        [HttpPut("{ssn:int}")]
        public IActionResult EditStudent(int ssn, [FromBody] Student updatedStudent)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var student = _uow.Students.GetById(ssn);
            if (student == null)
                return NotFound(new { Message = "Student Not Found!" });

            student.Name = updatedStudent.Name;
            student.Age = updatedStudent.Age;
            student.Address = updatedStudent.Address;
            student.Gender = updatedStudent.Gender;
            student.Image = updatedStudent.Image;

            _uow.Students.Update(student);
            _uow.Complete();

            return Ok( new { Message = "Student Updated Successfully", Student = student });
        }

        //-------------------------------------------


        [HttpDelete("{ssn:int}")]
        public IActionResult DeleteStudent(int ssn)
        {
            var student  = _uow.Students.GetById(ssn);
            if (student == null)
                return NotFound(new { Message = "Student Not Found!" });

            _uow.Students.Delete(student);
            _uow.Complete();

            return Ok( new { Message = "Student Deleted Successfully", Student = student });
        }
    }
}
