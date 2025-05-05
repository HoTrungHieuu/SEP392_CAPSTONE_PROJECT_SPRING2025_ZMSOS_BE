using BO.Models;
using DAO.AddModel;
using DAO.OtherModel;
using DAO.SearchModel;
using DAO.UpdateModel;
using DAO.ViewModel;
using Repository.IRepository;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class IncidentHistoryService : IIncidentHistoryService
    {
        public IIncidentHistoryRepository repo;
        public IObjectViewService objectViewService;
        public IncidentHistoryService(IIncidentHistoryRepository repo, IObjectViewService objectViewService)
        {
            this.repo = repo;
            this.objectViewService = objectViewService;
        }
        public async Task<ServiceResult> GetListIncidentHistory()
        {
            try
            {
                var incidentHistorys = await repo.GetListIncidentHistory();
                if (incidentHistorys == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                var result = await objectViewService.GetListIncidentHistoryView(incidentHistorys);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Incident Historys",
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
        public async Task<ServiceResult> GetListIncidentHistoryByAnimalId(int animalId)
        {
            try
            {
                var incidentHistorys = await repo.GetListIncidentHistoryByAnimalId(animalId);
                if (incidentHistorys == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                var result = await objectViewService.GetListIncidentHistoryView(incidentHistorys);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Incident Historys",
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
        public async Task<ServiceResult> GetIncidentHistoryById(int id)
        {
            try
            {
                var incidentHistory = repo.GetById(id);
                if (incidentHistory == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found!",
                    };
                }

                var result = await objectViewService.GetIncidentHistoryView(incidentHistory);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Incident History",
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
        public async Task<ServiceResult> AddIncidentHistory(IncidentHistoryAdd key)
        {
            try
            {
                var incidentHistory = await repo.AddIncidentHistory(key);
                var result = await objectViewService.GetIncidentHistoryView(incidentHistory);
                return new ServiceResult
                {
                    Status = 200,
                    Message = "Add Success",
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
        public async Task<ServiceResult> UpdateIncidentHistory(IncidentHistoryUpdate key)
        {
            try
            {
                var incidentHistory = await repo.UpdateIncidentHistory(key);
                if (incidentHistory == null)
                {
                    return new ServiceResult
                    {
                        Status = 404,
                        Message = "Not Found"
                    };
                }
                var result = await objectViewService.GetIncidentHistoryView(incidentHistory);

                return new ServiceResult
                {
                    Status = 200,
                    Message = "Update Success",
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
        public async Task<ServiceResult> DisableIncidentHistory(List<int> incidentHistoryIds)
        {
            try
            {
                incidentHistoryIds = incidentHistoryIds.Distinct().ToList();
                List<int> unsucessIds = new List<int>();
                foreach (int incidentHistoryId in incidentHistoryIds)
                {
                    if ((await repo.DisableIncidentHistory(incidentHistoryId)) == 0)
                        unsucessIds.Add(incidentHistoryId);
                }

                return new ServiceResult
                {
                    Status = 200,
                    Message = $"Disable Success with id unsuccess {unsucessIds}",
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
