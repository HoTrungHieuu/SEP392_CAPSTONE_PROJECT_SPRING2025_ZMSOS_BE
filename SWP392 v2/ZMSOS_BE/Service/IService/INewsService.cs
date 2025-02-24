using DAO.AddModel;
using DAO.UpdateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface INewsService
    {
        public Task<ServiceResult> GetListNews();
        public Task<ServiceResult> GetNewsById(int id);
        public Task<ServiceResult> AddNews(NewsAdd key);
        public Task<ServiceResult> UpdateNews(NewsUpdate key);
    }
}
