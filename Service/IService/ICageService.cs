using DAO.AddModel;
using DAO.SearchModel;
using DAO.UpdateModel;
using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface ICageService
    {
        public Task<ServiceResult> GetListCage();
        public Task<ServiceResult> GetListCageSuitable(int animalId);
        public Task<ServiceResult> GetListCageSearching(CageSearch<CageView> key);
        public Task<ServiceResult> GetListCageByZooAreaId(int zooAreaId);
        public Task<ServiceResult> GetCageById(int id);
        public Task<ServiceResult> GetCageHistoryById(int id);
        public Task<ServiceResult> AddCage(CageAdd key);
        public Task<ServiceResult> UpdateCage(CageUpdate key);
        public Task<ServiceResult> DeleteCage(int id);
        public Task<ServiceResult> DisableCage(List<int> cageIds);
    }
}
