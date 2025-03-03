using BO.Models;
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
