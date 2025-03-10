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
    public interface IStatusRepository : IGenericRepository<Status>
    {
        public Task<Status> AddStatus(StatusAdd key);
        public Task<Status?> UpdateStatus(StatusUpdate key);
        public StatusView ConvertStatusIntoStatusView(Status status);
    }
}
