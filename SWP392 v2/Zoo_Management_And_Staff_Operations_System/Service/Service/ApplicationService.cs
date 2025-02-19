using BO.Models;
using DAO.AddModel;
using DAO.ViewModel;
using Repository.IRepositoyr;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class ApplicationService : IApplicationService
    {
        public IApplicationRepository repo;
        public IApplicationTypeRepository applicationTypeRepo;
        public IUserRepository userRepo;
        public ApplicationService(IApplicationRepository repo, IApplicationTypeRepository applicationTypeRepo, IUserRepository userRepo)
        {
            this.repo = repo;
            this.applicationTypeRepo = applicationTypeRepo;
            this.userRepo = userRepo;
        }
        public async Task<ServiceResult> GetListApplicationBySenderId(int senderId)
        {
            try
            {
                var applications = await repo.GetListApplcationBySenderId(senderId);
                if (applications == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                List<UserView> senders = new List<UserView>();
                List<UserView> recievers = new List<UserView>();
                List<ApplicationTypeView> applicationTypes = new();
                for (int i = 0; i < applications.Count; i++)
                {
                    UserView sender = new();
                    sender.ConvertUserIntoUserView(await userRepo.GetUserByAccountId((int)applications[i].SenderId));
                    senders.Add(sender);
                    UserView reciever = new();
                    reciever.ConvertUserIntoUserView(await userRepo.GetUserByAccountId((int)applications[i].RecieverId));
                    recievers.Add(reciever);
                    var applicationType = applicationTypeRepo.GetById(applications[i].Id);
                    ApplicationTypeView applicationTypeView = applicationTypeRepo.ConvertApplicationTypeIntoApplicationTypeView(applicationType);
                    applicationTypes.Add(applicationTypeView);
                }

                var result = repo.ConvertListApplicationIntoListApplicationView(applications, senders, recievers, applicationTypes);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Applications",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }
        public async Task<ServiceResult> GetListApplicationByRecieverId(int recieverId)
        {
            try
            {
                var applications = await repo.GetListApplcationByRecieverId(recieverId);
                if (applications == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                List<UserView> senders = new List<UserView>();
                List<UserView> recievers = new List<UserView>();
                List<ApplicationTypeView> applicationTypes = new();
                for (int i = 0; i < applications.Count; i++)
                {
                    UserView sender = new();
                    sender.ConvertUserIntoUserView(await userRepo.GetUserByAccountId((int)applications[i].SenderId));
                    senders.Add(sender);
                    UserView reciever = new();
                    reciever.ConvertUserIntoUserView(await userRepo.GetUserByAccountId((int)applications[i].RecieverId));
                    recievers.Add(reciever);
                    var applicationType = applicationTypeRepo.GetById(applications[i].Id);
                    ApplicationTypeView applicationTypeView = applicationTypeRepo.ConvertApplicationTypeIntoApplicationTypeView(applicationType);
                    applicationTypes.Add(applicationTypeView);
                }

                var result = repo.ConvertListApplicationIntoListApplicationView(applications, senders, recievers, applicationTypes);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Applications",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }
        public async Task<ServiceResult> GetApplicationById(int id)
        {
            try
            {
                var application = repo.GetById(id);
                if (application == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                UserView sender = new();
                sender.ConvertUserIntoUserView(await userRepo.GetUserByAccountId((int)application.SenderId));
                UserView reciever = new();
                reciever.ConvertUserIntoUserView(await userRepo.GetUserByAccountId((int)application.RecieverId));
                var applicationType = applicationTypeRepo.GetById(application.Id);
                ApplicationTypeView applicationTypeView = applicationTypeRepo.ConvertApplicationTypeIntoApplicationTypeView(applicationType);




                var result = repo.ConvertApplicationIntoApplicationView(application, sender, reciever, applicationTypeView);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Application",
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }
        public async Task<ServiceResult> AddApplication(ApplicationAdd key)
        {
            try
            {
                var application = await repo.AddApplication(key);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Add Success",
                };
            }
            catch (Exception ex)
            {
                return new ServiceResult
                {
                    Status = 501,
                    Message = ex.ToString(),
                };
            }
        }
    }
}
