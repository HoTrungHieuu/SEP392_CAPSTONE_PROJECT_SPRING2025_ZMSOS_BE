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
        public IObjectViewService objectViewService;
        public ReportService(IReportRepository repo, IReportAttachmentRepository attachmentRepo, IUserRepository userRepo, IObjectViewService objectViewService)
        {
            this.repo = repo;
            this.attachmentRepo = attachmentRepo;
            this.userRepo = userRepo;
            this.objectViewService = objectViewService;
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
                
                var result = await objectViewService.GetListReportView(reports);
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

                var result = await objectViewService.GetListReportView(reports);
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

                var result = await objectViewService.GetReportView(report);
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
                        Status = null
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
