using BO.Models;
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
        public StatusView ConvertStatusIntoStatusView(Status status);
    }
}
