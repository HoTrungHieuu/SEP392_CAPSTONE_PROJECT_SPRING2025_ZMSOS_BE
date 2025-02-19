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
                    Status = ""
                };
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
                cage.Status = "";
                await UpdateAsync(cage);
                return cage;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<CageView> ConvertListCageIntoListCageView(List<Cage> cages, List<ZooAreaView> zooAreas)
        {
            try
            {
                List<CageView> result = new();
                for (int i = 0; i < cages.Count; i++)
                {
                    result.Add(ConvertCageIntoCageView(cages[i], zooAreas[i]));
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public CageView ConvertCageIntoCageView(Cage cage, ZooAreaView zooArea)
        {
            try
            {
                CageView result = new CageView();
                result.ConvertCageIntoCageView(cage,zooArea);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
