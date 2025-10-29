using APIDay22.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace APIDay22.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbset;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbset = _context.Set<T>();
        }

        public void Add(T entity) => _dbset.Add(entity);

        public void Delete(T entity) => _dbset.Remove(entity);

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate) => _dbset.Where(predicate).ToList();

        public IEnumerable<T> GetAll() => _dbset.ToList();

        public T? GetById(int id) => _dbset.Find(id);

        public void Update(T entity) => _dbset.Update(entity);
    }
}
