using ApiCatalogoNet8.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApiCatalogoNet8.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<T>> GetAll()
        {
            var items = await _context.Set<T>().ToListAsync();
            return items;
        }

        public async Task<T>? Get(Expression<Func<T, bool>> predicate)
        {
            var item = await _context.Set<T>().FirstOrDefaultAsync(predicate);
            return item;
        }

        public async Task<T> Create(T entity)
        {
            var item = await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<T> Update(T entity)
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
