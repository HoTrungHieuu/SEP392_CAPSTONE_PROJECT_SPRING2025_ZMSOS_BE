using BO.Models;
using DAO.AddModel;
using DAO.OtherModel;
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
        public async Task<List<Application>?> GetListApplcation()
        {
            try
            {
                var applications = await GetAllAsync();
                applications = applications.OrderByDescending(l => l.Date).ToList();
                return applications;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<Application>?> GetListApplcationBySenderId(int senderId)
        {
            try
            {
                var applications = (await GetListApplcation()).FindAll(l => l.SenderId == senderId);
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
                var applications = (await GetListApplcation()).FindAll(l => l.RecieverId == recieverId);
                return applications;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Application> AddApplication(ApplicationAdd key, int recieverId)
        {
            try
            {
                Application application = new()
                {
                    ApplicationTypeId = key.ApplicationTypeId,
                    RecieverId = recieverId,
                    SenderId = key.SenderId,
                    Title = key.Title,
                    Details = key.Detail,
                    Date = VietNamTime.GetVietNamTime(),
                    Status = "Pending"
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
                application.Status = key.Status;
                await UpdateAsync(application);
                return application;
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
