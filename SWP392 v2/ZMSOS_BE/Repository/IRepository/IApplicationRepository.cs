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
    public interface IApplicationRepository : IGenericRepository<Application>
    {
        public Task<List<Application>?> GetListApplcationBySenderId(int senderId);
        public Task<List<Application>?> GetListApplcationByRecieverId(int recieverId);
        public Task<Application> AddApplication(ApplicationAdd key);
        public Task<Application?> UpdateApplication(ApplicationUpdate key);
        public List<ApplicationView> ConvertListApplicationIntoListApplicationView(List<Application> applications, List<UserView> senders, List<UserView> recievers, List<ApplicationTypeView> applicationTypes);
        public ApplicationView ConvertApplicationIntoApplicationView(Application application, UserView sender, UserView reciever, ApplicationTypeView applicationType);
    }
}
