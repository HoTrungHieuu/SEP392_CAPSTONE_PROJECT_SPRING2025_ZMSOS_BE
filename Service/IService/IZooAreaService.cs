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
    public interface IZooAreaService
    {
        public Task<ServiceResult> GetListZooArea();
        public Task<ServiceResult> GetListZooAreaSearching(ZooAreaSearch<ZooAreaView> key);
        public Task<ServiceResult> GetZooAreaById(int id);
        public Task<ServiceResult> AddZooArea(ZooAreaAdd key);
        public Task<ServiceResult> UpdateZooArea(ZooAreaUpdate key);
        public Task<ServiceResult> DeleteZooArea(int id);
    } 
}
