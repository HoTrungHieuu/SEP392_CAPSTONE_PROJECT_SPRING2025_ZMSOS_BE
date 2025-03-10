using BO.Models;
using DAO.AddModel;
using DAO.UpdateModel;
using DAO.ViewModel;
using Repository.IRepository;
using Repository.IRepositoyr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    public class StatusRepository : GenericRepository<Status>, IStatusRepository
    {
        public StatusRepository()
        {
        }
        public async Task<Status> AddStatus(StatusAdd key)
        {
            try
            {
                Status status = new()
                {
                    Name = key.Name,
                    Description = key.Description,
                };
                await CreateAsync(status);
                return status;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<Status?> UpdateStatus(StatusUpdate key)
        {
            try
            {
                var status = GetById(key.Id);
                if (status == null) return null;
                status.Name = key.Name;
                status.Description = key.Description;
                await UpdateAsync(status);
                return status;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public StatusView ConvertStatusIntoStatusView(Status status)
        {
            try
            {
                StatusView result = new StatusView();
                result.ConvertStatusIntoStatusView(status);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
