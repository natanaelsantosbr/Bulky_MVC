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

        public T Get(Expression<Func<T, bool>> filter, string? includeProperties, bool tracked )
        {
            IQueryable<T> query;
            
            if(tracked)
            query = _contextSet;
            else
                query = _contextSet.AsNoTracking();

            query = query.Where(filter);

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }


            return query.FirstOrDefault();

        }

        public IEnumerable<T> GetAll(string? includeProperties)
        {
            IQueryable<T> query = _contextSet;

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
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
