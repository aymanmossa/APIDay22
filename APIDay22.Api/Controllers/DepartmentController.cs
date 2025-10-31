using APIDay22.Core.DTOs;
using APIDay22.Core.Interfaces;
using APIDay22.Core.Models;
using APIDay22.EF;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIDay22.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IUoW _uow;
        public DepartmentController(IUoW uow)
        {
            _uow = uow;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var depts = _uow.Departments.GetAll();
            return Ok(depts);
        }

        //----------------------------------------------------


        [HttpGet("{id:int}")]
        public IActionResult GetById(int id)
        {
            var dept = _uow.Departments.GetDepartmentsWithStudents()
                .FirstOrDefault(d => d.DeptId == id);
            if (dept == null)
                return NotFound(new { Message = "Department Not Found!" });

            var dto = new DepartmentDetailsDto
            {
                Name = dept.Name,
                Manager = dept.Manger,                  // keep your DTO’s property name
                Location = dept.Location,
                NumOfStudents = dept.Students?.Count ?? 0,
                Message = (dept.Students?.Count ?? 0) > 2 ? "Overload" : "We need more students",
                Color = (dept.Students?.Count ?? 0) > 2 ? "red" : "green",
            };

            return Ok(dto);
        }


        //----------------------------------------------------


        [HttpPost]
        public IActionResult Add([FromBody] Department department)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _uow.Departments.Add(department);
            _uow.Complete();

            return CreatedAtAction(nameof(GetById), new { id = department.DeptId },
                new { Message = "Department Added Successfully", Department = department });
        }


        //----------------------------------------------------


        [HttpPut("{id:int}")]
        public IActionResult Update(int id, [FromBody] Department updatedDepartment)
        {
            if (ModelState.IsValid)
                return BadRequest(ModelState);

            var existing = _uow.Departments.GetById(id);
            if (existing == null)
                return NotFound(new { Message = "Department Not Found!" });

            existing.Name = updatedDepartment.Name;
            existing.Manger = updatedDepartment.Manger;
            existing.Location = updatedDepartment.Location;

            _uow.Departments.Update(existing);
            _uow.Complete();

            return Ok(new { Message = "Department Not Found!", Department = existing });
        }


        //----------------------------------------------------


        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var dept = _uow.Departments.GetById(id);
            if (dept == null)
                return NotFound(new { Message = "Department Not Found!" });

            _uow.Departments.Delete(dept);
            _uow.Complete();

            return Ok(new { Message = "Department Deleted Successfully", Department = dept });
        }


        //----------------------------------------------------


        [HttpPost("{deptId:int}/addstudents/{ssn:int}")]
        public IActionResult AddStudentToDepartment(int deptId, int ssn)
        {
            var dept = _uow.Departments.GetDepartmentsWithStudents()
                .FirstOrDefault(d => d.DeptId == deptId);

            if (dept == null)
                return NotFound(new { Message = "Department Not Found!" });
    
            var student = _uow.Students.GetById(ssn);
            if (student == null)
                return NotFound(new { Message = "Student Not Found!" });

            if  (student != null && dept.Students.Any(s => s.Ssn == ssn))
                return BadRequest(new { Message = "Student Already Exists!" });

            dept.Students ??= new List<Student>();
            dept.Students.Add(student!);
            _uow.Departments.Update(dept);
            _uow.Complete();

            return Ok(new { Message = "Student Added Successfully", Department = dept });
        }

        [HttpDelete("{deptId:int}/deletestudent/{ssn:int}")]
        public IActionResult RemoveStudentFromDepartment(int deptId, int ssn)
        {
            var dept = _uow.Departments.GetDepartmentsWithStudents()
                .FirstOrDefault(d => d.DeptId == deptId);

            if (dept == null)
                return NotFound(new { Message = "Department Not Found!" });

            var student = dept.Students?.FirstOrDefault(s => s.Ssn == ssn);
            if (student == null)
                return NotFound(new { Message = "Student Not Found!" });

            dept.Students?.Remove(student);
            _uow.Departments.Update(dept);
            _uow.Complete();

            return Ok(new { Message = "Student Deleted Successfully", Department = dept });
        }
    }
}
