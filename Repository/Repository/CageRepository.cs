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
                var cages = (await GetAllAsync());
                cages.OrderByDescending(l=>l.DateCreate).ToList();
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
                var cages = (await GetListCage()).FindAll(l => l.ZooAreaId == zooAreaId);
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
                    Size = key.Size,
                    UrlImage = key.UrlImage,
                    DateCreate = DateOnly.FromDateTime(DateTime.Now),
                    Status = key.Status,
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
                cage.Name = key.Name;
                cage.Description = key.Description;
                cage.MaxQuantity = key.MaxQuantity;
                cage.UrlImage = key.UrlImage;
                cage.Size = key.Size;
                cage.Status = key.Status;
                cage.UpdatedDate = DateTime.Now;
                await UpdateAsync(cage);
                return cage;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> CheckListCageClassify(List<int> cagesId)
        {
            List<Cage> cages = new List<Cage>();
            foreach(int cageId in cagesId)
            {
                cages.Add(GetById(cageId));
            }
            if(cages.Count > 0)
            {
                string classifyIntial = cages[0].Classify;
                if (cages.FindAll(l => l.Classify == classifyIntial).Count != cages.Count)
                    return false;
            }
            return true;
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
