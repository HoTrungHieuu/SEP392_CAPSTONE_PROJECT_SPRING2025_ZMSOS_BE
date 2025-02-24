using DAO.AddModel;
using DAO.UpdateModel;
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
        public Task<ServiceResult> GetZooAreaById(int id);
        public Task<ServiceResult> AddZooArea(ZooAreaAdd key);
        public Task<ServiceResult> UpdateZooArea(ZooAreaUpdate key);
    } 
}
