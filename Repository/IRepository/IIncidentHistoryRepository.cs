using BO.Models;
using DAO.AddModel;
using DAO.UpdateModel;
using DAO.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepository
{
    public interface IIncidentHistoryRepository : IGenericRepository<IncidentHistory>
    {
        public Task<List<IncidentHistory>?> GetListIncidentHistory();
        public Task<List<IncidentHistory>?> GetListIncidentHistoryByAnimalId(int animalId);
        public Task<IncidentHistory> AddIncidentHistory(IncidentHistoryAdd key);
        public Task<IncidentHistory?> UpdateIncidentHistory(IncidentHistoryUpdate key);
        public Task<int> DisableIncidentHistory(int id);
        public IncidentHistoryView ConvertIncidentHistoryIntoIncidentHistoryView(IncidentHistory incidentHistory, AnimalView animal);
    }
}
