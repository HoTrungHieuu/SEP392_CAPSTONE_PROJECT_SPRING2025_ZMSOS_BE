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
    public class CageRepository : GenericRepository<Cage>, ICageRepository
    {
        public CageRepository()
        {
        }
        public async Task<List<Cage>?> GetListCage()
        {
            try
            {
                var cages = await GetAllAsync();
                return cages;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<Cage>?> GetListCageByAreaId(int zooAreaId)
        {
            try
            {
                var cages = (await GetAllAsync()).FindAll(l => l.ZooAreaId == zooAreaId);
                return cages;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Cage> AddCage(CageAdd key)
        {
            try
            {
                Cage cage = new()
                {
                    ZooAreaId = key.ZooAreaId,
                    Name = key.Name,
                    Description = key.Description,
                    Classify = key.Classify,
                    Status = null
                };
                if(key.Classify == "Individual")
                {
                    cage.MaxQuantity = key.MaxQuantity;
                    cage.CurrentQuantity = 0;
                }
                else
                {
                    cage.MaxQuantity = null;
                    cage.CurrentQuantity = null;
                }
                await CreateAsync(cage);
                return cage;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Cage?> UpdateCage(CageUpdate key)
        {
            try
            {
                var cage = GetById(key.Id);
                if (cage == null) return null;
                cage.ZooAreaId = key.ZooAreaId;
                cage.Name = key.Name;
                cage.Description = key.Description;
                cage.MaxQuantity = key.MaxQuantity;
                cage.Status = null;
                await UpdateAsync(cage);
                return cage;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public CageView ConvertCageIntoCageView(Cage cage, ZooAreaView zooArea, StatusView? status)
        {
            try
            {
                CageView result = new CageView();
                result.ConvertCageIntoCageView(cage,zooArea,status);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
