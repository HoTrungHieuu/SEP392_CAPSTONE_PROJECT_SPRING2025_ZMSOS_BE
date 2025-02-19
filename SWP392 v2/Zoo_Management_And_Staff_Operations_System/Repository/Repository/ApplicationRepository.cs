using BO.Models;
using DAO.AddModel;
using DAO.UpdateModel;
using DAO.ViewModel;
using Repository.IRepositoyr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class ApplicationRepository : GenericRepository<Application>, IApplicationRepository
    {
        public ApplicationRepository()
        {
        }
        public async Task<List<Application>?> GetListApplcationBySenderId(int senderId)
        {
            try
            {
                var applications = (await GetAllAsync()).FindAll(l => l.SenderId == senderId).OrderByDescending(l => l.Id).ToList();
                return applications;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<Application>?> GetListApplcationByRecieverId(int recieverId)
        {
            try
            {
                var applications = (await GetAllAsync()).FindAll(l => l.RecieverId == recieverId).OrderByDescending(l => l.Id).ToList();
                return applications;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Application> AddApplication(ApplicationAdd key)
        {
            try
            {
                Application application = new()
                {
                    RecieverId = key.RecieverId,
                    SenderId = key.SenderId,
                    Title = key.Title,
                    Details = key.Detail,
                    //Date = DateTime.Now,
                    Status = ""
                };
                await CreateAsync(application);
                return application;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Application?> UpdateApplication(ApplicationUpdate key)
        {
            try
            {
                var application = GetById(key.Id);
                if(application == null)
                {
                    return null;
                }
                application.Reply = key.Reply;  
                await UpdateAsync(application);
                return application;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<ApplicationView> ConvertListApplicationIntoListApplicationView(List<Application> applications, List<UserView> senders, List<UserView> recievers, List<ApplicationTypeView> applicationTypes)
        {
            try
            {
                List<ApplicationView> result = new();
                for (int i = 0; i < applications.Count; i++)
                {
                    result.Add(ConvertApplicationIntoApplicationView(applications[i], senders[i], recievers[i], applicationTypes[i]));
                }
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ApplicationView ConvertApplicationIntoApplicationView(Application application, UserView sender, UserView reciever, ApplicationTypeView applicationType)
        {
            try
            {
                ApplicationView result = new ApplicationView();
                result.ConvertApplicationIntoApplicationView(application, sender, reciever, applicationType);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
