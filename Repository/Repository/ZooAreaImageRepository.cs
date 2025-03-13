using BO.Models;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class ZooAreaImageRepository : GenericRepository<ZooAreaImage>, IZooAreaImageRepository
    {
        public ZooAreaImageRepository()
        {
        }
        public async Task<List<ZooAreaImage>?> GetListZooAreaImageByZooAreaId(int zooAreaId)
        {
            try
            {
                var zooAreaImages = (await GetAllAsync()).FindAll(l => l.ZooAreaId == zooAreaId);
                return zooAreaImages;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<string>?> GetListZooAreaImageUrlByZooAreaId(int zooAreaId)
        {
            try
            {
                var zooAreaImages = (await GetAllAsync()).FindAll(l => l.ZooAreaId == zooAreaId);
                List<string> result = new();
                foreach (var zooAreaImage in zooAreaImages)
                {
                    result.Add(zooAreaImage.UrlImage);
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async System.Threading.Tasks.Task AddZooAreaImageByZooAreaId(int zooAreaId, List<string> listUrl)
        {
            try
            {
                foreach (var item in listUrl)
                {
                    ZooAreaImage zooAreaImage = new ZooAreaImage()
                    {
                        ZooAreaId = zooAreaId,
                        UrlImage = item,
                        Status = null
                    };
                    await CreateAsync(zooAreaImage);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async System.Threading.Tasks.Task DeleteZooAreaImageByZooAreaId(int zooAreaId)
        {
            try
            {
                var zooAreaImages = (await GetAllAsync()).FindAll(l => l.ZooAreaId == zooAreaId);
                foreach (var item in zooAreaImages)
                {
                    await RemoveAsync(item);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
