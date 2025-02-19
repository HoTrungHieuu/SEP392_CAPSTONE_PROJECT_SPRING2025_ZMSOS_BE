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
    public class ReportService : IReportService
    {
        public IReportRepository repo;
        public IReportAttachmentRepository attachmentRepo;
        public IUserRepository userRepo;
        public ReportService(IReportRepository repo, IReportAttachmentRepository attachmentRepo, IUserRepository userRepo)
        {
            this.repo = repo;
            this.attachmentRepo = attachmentRepo;
            this.userRepo = userRepo;
        }
        public async Task<ServiceResult> GetListReportBySenderId(int senderId)
        {
            try
            {
                var reports = await repo.GetListReportBySenderId(senderId);
                if (reports == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                List<UserView> senders = new List<UserView>();
                List<UserView> recievers = new List<UserView>();
                List<List<string>> fileUrls = new(); 
                for(int i = 0; i < reports.Count; i++)
                {
                    UserView sender = new();
                    sender.ConvertUserIntoUserView(await userRepo.GetUserByAccountId((int)reports[i].SenderId));
                    senders.Add(sender);
                    UserView reciever = new();
                    reciever.ConvertUserIntoUserView(await userRepo.GetUserByAccountId((int)reports[i].ReceiverId));
                    recievers.Add(reciever);
                    var attachs = await attachmentRepo.GetListReportAttachmentByReportId(reports[i].Id);
                    List<string> fileUrl = attachmentRepo.ConvertListReportAttachmentIntoListString(attachs);
                    fileUrls.Add(fileUrl);
                }
                
                var result = repo.ConvertListReportIntoListReportView(reports,senders,recievers,fileUrls);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Reports",
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
        public async Task<ServiceResult> GetListReportByRecieverId(int recieverId)
        {
            try
            {
                var reports = await repo.GetListReportBySenderId(recieverId);
                if (reports == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                List<UserView> senders = new List<UserView>();
                List<UserView> recievers = new List<UserView>();
                List<List<string>> fileUrls = new();
                for (int i = 0; i < reports.Count; i++)
                {
                    UserView sender = new();
                    sender.ConvertUserIntoUserView(await userRepo.GetUserByAccountId((int)reports[i].SenderId));
                    senders.Add(sender);
                    UserView reciever = new();
                    reciever.ConvertUserIntoUserView(await userRepo.GetUserByAccountId((int)reports[i].ReceiverId));
                    recievers.Add(reciever);
                    var attachs = await attachmentRepo.GetListReportAttachmentByReportId(reports[i].Id);
                    List<string> fileUrl = attachmentRepo.ConvertListReportAttachmentIntoListString(attachs);
                    fileUrls.Add(fileUrl);
                }

                var result = repo.ConvertListReportIntoListReportView(reports, senders, recievers, fileUrls);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Reports",
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
        public async Task<ServiceResult> GetReportById(int id)
        {
            try
            {
                var report = repo.GetById(id);
                if (report == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                UserView sender = new();
                sender.ConvertUserIntoUserView(await userRepo.GetUserByAccountId((int)report.SenderId));
                UserView reciever = new();
                reciever.ConvertUserIntoUserView(await userRepo.GetUserByAccountId((int)report.ReceiverId));
                var attachs = await attachmentRepo.GetListReportAttachmentByReportId(report.Id);
                List<string> fileUrl = attachmentRepo.ConvertListReportAttachmentIntoListString(attachs);

                var result = repo.ConvertReportIntoReportView(report, sender, reciever, fileUrl);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Report",
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
        public async Task<ServiceResult> AddReport(ReportAdd key)
        {
            try
            {
                var report = await repo.AddReport(key);
                foreach(var item in key.UrlFile)
                {
                    await attachmentRepo.CreateAsync(new()
                    {
                        ReportId = report.Id,
                        FileUrl = item,
                        Status = ""
                    });
                }
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
