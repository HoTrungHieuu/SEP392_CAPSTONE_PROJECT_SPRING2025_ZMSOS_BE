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
    public interface IZooAreaRepository : IGenericRepository<ZooArea>
    {
        public Task<List<ZooArea>?> GetListZooArea();
        public Task<ZooArea> AddZooArea(ZooAreaAdd key);
        public Task<ZooArea?> UpdateZooArea(ZooAreaUpdate key);
        public ZooAreaView ConvertZooAreaIntoZooAreaView(ZooArea zooArea, List<string> urlImages);
    }
}
