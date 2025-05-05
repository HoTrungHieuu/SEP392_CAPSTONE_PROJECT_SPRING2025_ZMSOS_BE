using BO.Models;
using DAO.AddModel;
using DAO.OtherModel;
using DAO.UpdateModel;
using DAO.ViewModel;
using Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class IncidentHistoryRepository : GenericRepository<IncidentHistory>, IIncidentHistoryRepository
    {
        public IncidentHistoryRepository()
        {
        }
        public async Task<List<IncidentHistory>?> GetListIncidentHistory()
        {
            try
            {
                var incidentHistorys = await GetAllAsync();
                incidentHistorys = incidentHistorys.OrderByDescending(l => l.Id).ToList();
                return incidentHistorys;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<IncidentHistory>?> GetListIncidentHistoryByAnimalId(int animalId)
        {
            try
            {
                var incidentHistorys = (await GetListIncidentHistory()).FindAll(l=>l.AnimalId == animalId);
                return incidentHistorys;
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public async Task<IncidentHistory> AddIncidentHistory(IncidentHistoryAdd key)
        {
            try
            {
                IncidentHistory incidentHistory = new()
                {
                    AnimalId = key.AnimalId,
                    Name = key.Name,
                    Description = key.Description,
                    DateStart = key.DateStart,
                    DateEnd = key.DateEnd,
                    ControlMeasures = key.ControlMeasures,
                };
                await CreateAsync(incidentHistory);
                return incidentHistory;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IncidentHistory?> UpdateIncidentHistory(IncidentHistoryUpdate key)
        {
            try
            {
                var incidentHistory = GetById(key.Id);
                if (incidentHistory == null) return null;

                incidentHistory.Name = key.Name;
                incidentHistory.Description = key.Description;
                incidentHistory.ControlMeasures = key.ControlMeasures;
                incidentHistory.DateStart = key.DateStart;
                incidentHistory.DateEnd = key.DateEnd;
                await UpdateAsync(incidentHistory);
                return incidentHistory;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<int> DisableIncidentHistory(int id)
        {
            try
            {
                var IncidentHistory = GetById(id);
                if (IncidentHistory == null) return 0;
                IncidentHistory.Status = "Deleted";
                var row = await UpdateAsync(IncidentHistory);
                return row;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IncidentHistoryView ConvertIncidentHistoryIntoIncidentHistoryView(IncidentHistory incidentHistory, AnimalView animal)
        {
            try
            {
                IncidentHistoryView result = new IncidentHistoryView();
                result.ConvertIncidentHistoryIntoIncidentHistoryView(incidentHistory, animal);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
