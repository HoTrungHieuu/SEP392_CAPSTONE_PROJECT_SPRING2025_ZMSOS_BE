using BO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IZooAreaImageRepository : IGenericRepository<ZooAreaImage>
    {
        public Task<List<ZooAreaImage>?> GetListZooAreaImageByZooAreaId(int zooAreaId);
        public Task<List<string>?> GetListZooAreaImageUrlByZooAreaId(int zooAreaId);
        public System.Threading.Tasks.Task AddZooAreaImageByZooAreaId(int zooAreaId, List<string> listUrl);
        public System.Threading.Tasks.Task DeleteZooAreaImageByZooAreaId(int zooAreaId);
    }
}
