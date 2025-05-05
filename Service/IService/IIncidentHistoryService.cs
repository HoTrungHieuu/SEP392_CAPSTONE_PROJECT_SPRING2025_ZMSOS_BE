using DAO.AddModel;
using DAO.UpdateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IIncidentHistoryService
    {
        public Task<ServiceResult> GetListIncidentHistory();
        public Task<ServiceResult> GetListIncidentHistoryByAnimalId(int animalId);
        public Task<ServiceResult> GetIncidentHistoryById(int id);
        public Task<ServiceResult> AddIncidentHistory(IncidentHistoryAdd key);
        public Task<ServiceResult> UpdateIncidentHistory(IncidentHistoryUpdate key);
        public Task<ServiceResult> DisableIncidentHistory(List<int> incidentHistoryIds);
    }
}
