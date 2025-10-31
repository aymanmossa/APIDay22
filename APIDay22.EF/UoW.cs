using APIDay22.Core.Interfaces;
using APIDay22.Core.Models;
using APIDay22.EF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIDay22.EF
{
    public class UoW : IUoW
    {
        private readonly ApplicationDbContext _context;
        public IBaseRepository<Student> Students { get; private set; }
        public IDepartmentRepository Departments { get; private set; }

        public UoW(ApplicationDbContext context)
        {
            _context = context;
            Students = new BaseRepository<Student>(_context);
            Departments = new DepartmentRepository(_context);
        }

        public int Complete() => _context.SaveChanges();

        public void Dispose() => _context.Dispose();
    }
}
