using APIDay22.Core.Interfaces;
using APIDay22.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDay22.EF.Repositories
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext context) : base(context) { }
        public IEnumerable<Department> GetDepartmentsWithStudents()
        {
            return _context.Departments.Include(d => d.Students).ToList();
        }
    }
}
