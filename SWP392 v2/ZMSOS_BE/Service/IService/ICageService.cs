using DAO.AddModel;
using DAO.UpdateModel;
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
        public Task<ServiceResult> GetListCageByZooAreaId(int zooAreaId);
        public Task<ServiceResult> GetCageById(int id);
        public Task<ServiceResult> AddCage(CageAdd key);
        public Task<ServiceResult> UpdateCage(CageUpdate key);
    }
}
