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

namespace Repository.IRepositoyr
{
    public interface INewsRepository : IGenericRepository<News>
    {
        public Task<List<News>?> GetListNews();
        public Task<News> AddNews(NewsAdd key);
        public Task<News?> UpdateNews(NewsUpdate key);
        public List<NewsView> ConvertListNewsIntoListNewsView(List<News> newss, List<UserView> users);
        public NewsView ConvertNewsIntoNewsView(News news, UserView user);
    }
}
