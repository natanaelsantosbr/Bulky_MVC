using Bulky.DataAccess.Data;
using Bulky.Models.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> _contextSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _contextSet = _context.Set<T>();

        }

        public void Add(T entity)
        {
            _contextSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = _contextSet;
            query = query.Where(filter);
            return query.FirstOrDefault();

        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = _contextSet;
            return query.ToList();
        }

        public void Remove(T entity)
        {
            _contextSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _contextSet.RemoveRange(entities);
        }
    }
}
