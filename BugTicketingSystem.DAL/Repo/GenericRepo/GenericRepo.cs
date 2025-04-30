using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BugTicketingSystem.DAL
{
    public class GenericRepo<T> : IGenricRepo<T> where T : class
    {
        private readonly BugTicketingContext context;
        public GenericRepo(BugTicketingContext _context)
        {
            this.context = _context;

        }
        public  void Add(T entity)
        {
           
           context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            context?.Set<T>().Remove(entity);
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await context.Set<T>().AsNoTracking().ToListAsync(); 
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
           return await context.Set<T>().FindAsync(id);
        }

        public void Update(T entity)
        {
            
        }
    }
}
