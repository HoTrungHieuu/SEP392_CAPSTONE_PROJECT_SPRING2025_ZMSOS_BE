using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        public void PrepareCreate(T entity);
        public void PrepareUpdate(T entity);
        public void PrepareRemove(T entity);
        public int Save();
        public Task<int> SaveAsync();
        public List<T> Paging(List<T>? list, int pageSize, int pageIndex);
        public List<T> GetAll();
        public Task<List<T>> GetAllAsync();
        public T GetById(int? id);
        public Task<T> GetByIdAsync(int id);
        public T GetById(string code);
        public Task<T> GetByIdAsync(string code);
        public void Create(T entity);
        public Task<int> CreateAsync(T entity);
        public void Update(T entity);
        public Task<int> UpdateAsync(T entity);
        public bool Remove(T entity);
        public Task<bool> RemoveAsync(T entity);

    }
}
