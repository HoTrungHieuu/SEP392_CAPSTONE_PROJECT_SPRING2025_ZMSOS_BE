using BO.Models;
using Microsoft.EntityFrameworkCore;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected Zoo_Management_And_Staff_Operations_SystemContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository()
        {
            _context = new Zoo_Management_And_Staff_Operations_SystemContext();
            _dbSet = _context.Set<T>();
        }


        public GenericRepository(Zoo_Management_And_Staff_Operations_SystemContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void PrepareCreate(T entity)
        {
            _dbSet.Add(entity);
        }

        public void PrepareUpdate(T entity)
        {
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
        }

        public void PrepareRemove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public List<T> Paging(List<T>? list, int pageSize, int pageIndex)
        {
            if (list == null) return null;
            if (pageSize < 1) pageSize = 1;
            if (pageIndex < 1) pageIndex = 1;
            List<T> result = new List<T>();
            for (int i = pageSize * (pageIndex - 1); (i < pageSize * pageIndex) && (i < list.Count); i++)
            {
                result.Add(list[i]);
            }
            return result;
        }
        public List<T> GetAll()
        {
            return _dbSet.ToList();
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
        public void Create(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public async Task<int> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            var tracker = _context.Attach(entity);
            tracker.State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }

        public bool Remove(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
            return true;
        }
        public async Task<bool> RemoveAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public T GetById(int? id)
        {
            if (id == null) return null;
            var result = _dbSet.Find(id);
            return result;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public T GetById(string code)
        {
            return _dbSet.Find(code);
        }

        public async Task<T> GetByIdAsync(string code)
        {
            return await _dbSet.FindAsync(code);
        }
    }
}
