using APIDay22.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDay22.Core.Interfaces
{
    public interface IDepartmentRepository : IBaseRepository<Department>
    {
        IEnumerable<Department> GetDepartmentsWithStudents();
    }
}
