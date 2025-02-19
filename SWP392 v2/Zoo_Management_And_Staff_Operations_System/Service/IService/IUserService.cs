using DAO.UpdateModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.IService
{
    public interface IUserService
    {
        public Task<ServiceResult> GetUserByAccountId(int accountId);
        public Task<ServiceResult> UpdateUser(UserUpdate key);
    }
}
