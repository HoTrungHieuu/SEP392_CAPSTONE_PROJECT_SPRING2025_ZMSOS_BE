using BO.Models;
using DAO.AddModel;
using DAO.UpdateModel;
using DAO.ViewModel;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class ZooAreaRepository : GenericRepository<ZooArea>, IZooAreaRepository
    {
        public ZooAreaRepository()
        {
        }
        public async Task<List<ZooArea>?> GetListZooArea()
        {
            try
            {
                var zooAreas = await GetAllAsync();
                return zooAreas;
            }
            catch (Exception)
            {
                throw;
            }
        }      
        public async Task<ZooArea> AddZooArea(ZooAreaAdd key)
        {
            try
            {
                ZooArea zooArea = new()
                {
                    Name = key.Name,
                    Description = key.Description,
                    AnimalOrder = key.AnimalOrder,
                    Location = key.Location,
                    Status = key.Status
                };
                await CreateAsync(zooArea);
                return zooArea;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ZooArea?> UpdateZooArea(ZooAreaUpdate key)
        {
            try
            {
                var zooArea = GetById(key.Id);
                if (zooArea == null) return null;
                zooArea.Name = key.Name;
                zooArea.Description = key.Description;
                zooArea.AnimalOrder =key.AnimalOrder;
                zooArea.Location = key.Location;
                zooArea.Status = key.Status;
                await UpdateAsync(zooArea);
                return zooArea;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ZooAreaView ConvertZooAreaIntoZooAreaView(ZooArea zooArea, List<string> urlImages)
        {
            try
            {
                ZooAreaView result = new ZooAreaView();
                result.ConvertZooAreaIntoZooAreaView(zooArea,urlImages);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
