using BO.Models;
using DAO.AddModel;
using DAO.UpdateModel;
using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface ICageRepository : IGenericRepository<Cage>
    {
        public Task<List<Cage>?> GetListCage();
        public Task<List<Cage>?> GetListCageByAreaId(int zooAreaId);
        public Task<Cage> AddCage(CageAdd key);
        public Task<Cage?> UpdateCage(CageUpdate key);
        public List<CageView> ConvertListCageIntoListCageView(List<Cage> cages, List<ZooAreaView> zooAreas);
        public CageView ConvertCageIntoCageView(Cage cage, ZooAreaView zooArea);
    }
}
