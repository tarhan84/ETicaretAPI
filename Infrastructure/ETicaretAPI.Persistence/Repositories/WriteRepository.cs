using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ETicaretAPI.Application.Repositories;
using ETicaretAPI.Domain.Entities.Common;
using ETicaretAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ETicaretAPI.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
    {
        private readonly ETicaretAPIDbContext _context;

        public WriteRepository(ETicaretAPIDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task<bool> AddAsync(T entity)
        {
            EntityEntry<T> data = await Table.AddAsync(entity);
            return data.State == EntityState.Added;
        }

        public async Task<bool> AddRangeAsync(List<T> entities)
        {
            await Table.AddRangeAsync(entities);
            return true;
        }

        public bool Remove(string id)
        {
            T entity = Table.FirstOrDefault(data => data.Id == Guid.Parse(id));
            return Remove(entity);
        }

        public bool Remove(T entity)
        {
            EntityEntry<T> entry = Table.Remove(entity);
            return entry.State == EntityState.Deleted;
        }

        public bool RemoveRange(List<T> entities)
        {
            Table.RemoveRange(entities);
            return true;
        }

        public Task<int> SaveAsync() => _context.SaveChangesAsync();

        public bool Update(T entity)
        {
            EntityEntry entry = Table.Update(entity);
            return entry.State == EntityState.Modified;
        }
    }
}
